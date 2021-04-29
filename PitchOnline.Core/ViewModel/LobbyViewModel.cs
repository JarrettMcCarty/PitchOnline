using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PitchOnline.Core
{
    public class LobbyViewModel : BaseViewModel
    {
        public bool IsPrivate { get; set; } = false;
        public bool IsLeader { get; set; }
        public string LobbyCode { get; set; }
        public string LobbyStatus { get; set; }
        public List<string> Team1 { get; set; }
        public List<string> Team2 { get; set; }
        public List<string> LobbyUsers { get; set; }

        public int NumberOfMembers { get; set; }

        public bool IsReady { get; set; } = false;
        public bool CanSwitch { get; set; } = true;

        public ICommand SwitchTeamCommand { get; set; }
        public ICommand ReadyCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PrivateToggleCommand { get; set; }
        public bool RemoveUserFromLobbyIsRunning { get; private set; }
        public string ErrorMessage { get; private set; }

        public LobbyViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            SwitchTeamCommand = new RelayCommand(SwitchTeam);
            ReadyCommand = new RelayCommand(Ready);
            PrivateToggleCommand = new RelayCommand(PrivateToggle);
        }

        public void Close()
        {
            IsReady = false;
            RemoveUserFromLobby();
            // TODO: Remove user from the lobby/teams in the database
            //IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LobbyList);
        }
        public async void RemoveUserFromLobby()
        {
            await RunCommandAsync(() => RemoveUserFromLobbyIsRunning, async () =>
            {
                HttpClient httpClient = new HttpClient();
                var postData = new User
                {
                    Username = IoC.Get<SettingsViewModel>().Username
                };
                var postResponse = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["NonProdBaseURL"] + "/lobby/removeuser", postData);
                JsonResponse error;
                if (postResponse.IsSuccessStatusCode)
                {
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LobbyList);
                }
                else
                {
                    error = await postResponse.Content.ReadFromJsonAsync<JsonResponse>();
                    ErrorMessage = error.Details;
                }
            });
        }
        public void SetCanSwitch()
        {
            //if ()
        }
        public async void SwitchTeam()
        {
            if (CanSwitch)
            { }
        }
        public void Ready()
        {
            IsReady = !IsReady;
        }

        public void PrivateToggle()
        {
            IsPrivate = true;
            // update db with private lobby
        }
    }
}
