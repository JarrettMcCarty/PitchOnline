using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net.Http.Json;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace PitchOnline.Core
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

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
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
        }

        #endregion

        public async void SetUserSettingsOnLogin(HttpResponseMessage postResponse, User postData)
        {
            User res;
            // Set Users Settings
            res = await postResponse.Content.ReadFromJsonAsync<User>();
            IoC.Get<SettingsViewModel>().DeckFolder = res.Deck;
            IoC.Get<SettingsViewModel>().UserBackground = res.Background;
            // Image pathing to be updated when allowing user to upload an image for their profile
            var path = @"C:\dev\PitchOnline\PitchOnline\Images\Profile\default.png";
            File.WriteAllBytes(path, System.Convert.FromBase64String(res.Avatar));
            IoC.Get<SettingsViewModel>().ImagePath = path;
            IoC.Get<SettingsViewModel>().Username = res.Username;
            IoC.Get<SettingsViewModel>().Password = new TextEntryViewModel() { Label = "Password", OriginalText = postData.Password };
        }

        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            ErrorMessage = string.Empty;
            await RunCommandAsync(() => LoginIsRunning, async () =>
            {

                HttpClient httpClient = new HttpClient();
                var postData = new User
                {
                    Username = Username,
                    Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                };
                var postResponse = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["NonProdBaseURL"]+"/login", postData);
                JsonResponse error;
                if (postResponse.IsSuccessStatusCode)
                {
                    // Set Users Settings
                    SetUserSettingsOnLogin(postResponse, postData);
                    // Go to home page
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Home);
                }
                else
                {
                    error = await postResponse.Content.ReadFromJsonAsync<JsonResponse>();
                    ErrorMessage = error.Details;
                }


                //var postRequest = new HttpRequestMessage(HttpMethod.Post, /*ConfigurationManager.AppSettings["BaseURL"] + "/login"*/)
                //{
                //    Content = JsonContent.Create(JsonConvert.SerializeObject(
                //        new User { Username = Username, Password = (parameter as IHavePassword).SecurePassword.Unsecure() }))
                //};

                    //await Task.Delay(1000);
                    //var postResponse = await httpClient.SendAsync(postRequest);

                //postResponse.EnsureSuccessStatusCode();
               

                //var email = Email;

                //// IMPORTANT: Never store unsecure password in variable like this
            });
        }

        /// <summary>
        /// Takes the user to the register page
        /// </summary>
        /// <returns></returns>
        public async Task RegisterAsync()
        {
            // Go to register page?
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Register);

            await Task.Delay(1);
        }
    }
}
