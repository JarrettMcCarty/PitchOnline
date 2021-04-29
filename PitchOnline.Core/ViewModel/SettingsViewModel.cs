using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PitchOnline.Core
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Private Properties
       
        private string username;
        
        private string backgroundPath = "/Images/Backgrounds/Background";
        private string DeckFolderProperty { get; set; }
        private string BackgroundProperty { get; set; }
        
        private List<string> deckFolders = new List<string>() { "Default" };
        
        private List<string> backgrounds = new List<string>() { "Green", "Blue", "Red" };
       
        private bool HasChanged { get; set; } = false;
        #endregion 

        #region Public Properties
       
        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(nameof(Username)); HasChanged = true; }
        }
        
        public string ErrorMessage { get; private set; }
        // Image pathing to be updated when allowing user to upload an image for their profile
        
        public string ImagePath { get; set; }
        public string DeckFolder
        {
            get { return DeckFolderProperty; }
            set
            {
                DeckFolderProperty = value; HasChanged = true;
            }
        }
        public string SelectedUserBackground => (backgroundPath + BackgroundProperty + ".jpg");
        
        public string UserBackground
        {
            get { return BackgroundProperty; }
            set { BackgroundProperty = value; HasChanged = true; }
        }
        
        public TextEntryViewModel Password { get; set; }
        
        public TextEntryViewModel Deck { get; set; }
       
        public TextEntryViewModel Background { get; set; }
        
        
        public List<string> DeckFolders => deckFolders;
       
        public List<string> UserBackgrounds => backgrounds;
       
        public bool SaveSettingsIsRunning { get; private set; }
        
        #endregion

        #region Commands 
        
        public ICommand OpenCommand { get; set; }

        
        public ICommand CloseCommand { get; set; }
        
        public ICommand SaveCommand { get; set; }
        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {
            // Create commands
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
            SaveCommand = new RelayCommand(async () => await SaveSettingsToDataBaseAsync());
            Deck = new TextEntryViewModel { Label = "Deck"};
            Background = new TextEntryViewModel { Label = "Background" };
        }

        #endregion

        #region Command Helpers
        public void Open()
        {
            // Close settings menu
            IoC.Get<ApplicationViewModel>().SettingsVisible = true;
        }

 
        public void Close()
        {
            // Close settings menu
            IoC.Get<ApplicationViewModel>().SettingsVisible = false;
        }
        #endregion

        #region Save & Load
        /*
        public void Save()
        {
            IsolatedStorageFile storageFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

            using (IsolatedStorageFileStream storageStream = new IsolatedStorageFileStream("Settings.xml", FileMode.Create, storageFile))
            {
                XmlSerializer homeSeri = new XmlSerializer(typeof(SettingsViewModel));
                homeSeri.Serialize(storageStream, this);
            }
        }

        public static SettingsViewModel Load()
        {
            IsolatedStorageFile storageFile = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);

            try
            {
                using (IsolatedStorageFileStream storageStream = new IsolatedStorageFileStream("Settings.xml", FileMode.Open, storageFile))
                {
                    XmlSerializer homeSeri = new XmlSerializer(typeof(SettingsViewModel));
                    return (SettingsViewModel)homeSeri.Deserialize(storageStream);
                }
            }
            catch
            {
                //throw new IOException("Error while loading save data");
            }
            return new SettingsViewModel();
        }
        */
        #endregion
     
        public async Task SaveSettingsToDataBaseAsync()
        {
            if (HasChanged || Password.WasChanged)
            {
                ErrorMessage = string.Empty;
                await RunCommandAsync(() => SaveSettingsIsRunning, async () =>
                {
                    var buffer = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(@"C:\dev\PitchOnline\PitchOnline\Images\Profile\default.png"));
                    HttpClient httpClient = new HttpClient();
                    var postData = new User
                    {
                        Username = Username,
                        Password = Password.OriginalText,
                        Avatar = buffer,
                        Background = UserBackground,
                        Deck = DeckFolder
                    };
                    var postResponse = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["NonProdBaseURL"] + "/user/settings", postData);
                    if (!postResponse.IsSuccessStatusCode)
                    {
                        JsonResponse res = await postResponse.Content.ReadFromJsonAsync<JsonResponse>();
                        ErrorMessage = res.Details;
                    }
                    
                });
                HasChanged = false;
            }
        }
    }
}
