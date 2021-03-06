using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PitchOnline.Core
{
    /// <summary>
    /// The View Model for a register screen
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// A flag indicating if the register command is running
        /// </summary>
        public bool RegisterIsRunning { get; set; }

        public string ErrorMessage { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// The command to register for a new account
        /// </summary>
        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterViewModel()
        {
            // Create commands
            RegisterCommand = new RelayParameterizedCommand(async (parameter) => await RegisterAsync(parameter));
            LoginCommand = new RelayCommand(async () => await LoginAsync());
        }

        #endregion

        /// <summary>
        /// Attempts to register a new user
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task RegisterAsync(object parameter)
        {
            ErrorMessage = string.Empty;
            await RunCommandAsync(() => RegisterIsRunning, async () =>
            {
                // Image pathing to be updated when allowing user to upload an image for their profile
                var buffer = System.Convert.ToBase64String(System.IO.File.ReadAllBytes(@"C:\dev\PitchOnline\PitchOnline\Images\Profile\default.png"));
                HttpClient httpClient = new HttpClient();
                var postData = new User
                {
                    Username = Username,
                    Password = (parameter as IHavePassword).SecurePassword.Unsecure(),
                    Avatar = buffer,
                    Background = "Green",
                    Deck = "Default"
                };
                var postResponse = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["NonProdBaseURL"] + "/register", postData);
                JsonResponse res;
                if (postResponse.IsSuccessStatusCode)
                {
                    IoC.Get<SettingsViewModel>().Username = postData.Username;
                    IoC.Get<SettingsViewModel>().Password = new TextEntryViewModel() { Label = "Password", OriginalText = postData.Password };
                    IoC.Get<SettingsViewModel>().DeckFolder = postData.Deck;
                    IoC.Get<SettingsViewModel>().UserBackground = postData.Background;
                    IoC.Get<SettingsViewModel>().ImagePath = @"C:\dev\PitchOnline\PitchOnline\Images\Profile\default.png";
                    // Go to home page
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Home);
                }
                else
                {
                    res = await postResponse.Content.ReadFromJsonAsync<JsonResponse>();
                    ErrorMessage = res.Details;
                }
            });
        }

        /// <summary>
        /// Takes the user to the login page
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Login);

            await Task.Delay(1);
        }
    }
}
