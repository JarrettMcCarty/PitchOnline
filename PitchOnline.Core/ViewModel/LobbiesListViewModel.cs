using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Net.Http.Json;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;

namespace PitchOnline.Core
{
    public class LobbiesListViewModel : BaseViewModel
    {
        public ICommand LobbyListCommand { get; set; }
        public ICommand JoinLobbyCommand { get; set; }
        public ICommand CreateLobbyCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        //private ObservableCollection<LobbyListInfo> rooms;
        public ObservableCollection<LobbyListInfo> Rooms { get; set; }
        public bool CreateLobbyIsRunning { get; private set; } = false;
        public bool JoinLobbyIsRunning { get; private set; } = false;
        // for testing purposes
        public bool IsPrivate { get; set; } = true;
        public string ErrorMessage { get; private set; }
        public Dispatcher dispatcher { get; set; }
        public string LobbyDisplay()
        {
            return string.Empty;
        }
        
        public LobbiesListViewModel()
        {
            LobbyListCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
            CreateLobbyCommand = new RelayCommand(CreateLobbyAsync);
            JoinLobbyCommand = new RelayParameterizedCommand(async (parameter) => await JoinLobbyAsync(parameter)); // param will be short id passed in from the itemscontrol button
            Rooms = new ObservableCollection<LobbyListInfo>();
            GetLobbies();
            /*
            Rooms = new ObservableCollection<LobbyListInfo>
            {
                new LobbyListInfo
                {
                    LobbyStatus = "testroom",
                    Players = 4,
                    Avatars = new List<byte[]>() { File.ReadAllBytes("/dev/PitchOnline/PitchOnline/Images/Profile/default.png") },
                    IsJoinable = true
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom1",
                    Players = 1,
                    Avatars = { },
                    IsJoinable = false
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom2",
                    Players = 2,
                    Avatars = { },
                    IsJoinable = false
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom3",
                    Players = 4,
                    Avatars = { },
                    IsJoinable = false
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom4",
                    Players = 4,
                    Avatars = { },
                    IsJoinable = false
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom5",
                    Players = 3,
                    Avatars = { },
                    IsJoinable = false
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom5",
                    Players = 3,
                    Avatars = { },
                    IsJoinable = false
                },
                new LobbyListInfo
                {
                    LobbyStatus = "testroom5",
                    Players = 3,
                    Avatars = { },
                    IsJoinable = false
                }
            };
            */
        }
       
        public async void GetLobbies()
        {
            //HttpClient httpClient = new HttpClient();
            //var response =  await httpClient.GetFromJsonAsync<LobbyListInfo>();
            var client = new HttpClient();
            var task = client.GetAsync(ConfigurationManager.AppSettings["NonProdBaseURL"] + "/lobby/all").ContinueWith((taskwithresponse) =>
            {
                var response = taskwithresponse.Result;
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                var data = (JObject)JsonConvert.DeserializeObject(jsonString.Result);
                var arr = JArray.Parse(data["LobbyList"].Value<string>());
                foreach (JObject o in arr.Children<JObject>())
                {
                    var x = new LobbyListInfo();
                    foreach (JProperty p in o.Properties())
                    {
                        if (p.Name == "ShortId")
                            x.ShortId = (string)p.Value;
                        if (p.Name == "LobbyName")
                            x.LobbyName = (string)p.Value;
                        if (p.Name == "LobbyStatus")
                            x.LobbyStatus = (string)p.Value;
                        if (p.Name == "IsPrivate")
                            x.IsPrivate = bool.Parse((string)p.Value);
                        if (p.Name == "Users")
                            x.Users = p.Value.ToObject<List<string>>();
                        if (p.Name == "Players")
                            x.Players = Int32.Parse((string)p.Value);
                    }
                    x.IsJoinable = (x.Players < 4 && x.IsPrivate == false);
                   
                    Rooms = new ObservableCollection<LobbyListInfo>(); 
                    Dispatcher.CurrentDispatcher.Invoke(() => { Rooms.Add(x); });
                }
            });
        }
        public async Task JoinLobbyAsync(object parameter)
        {
            await RunCommandAsync(() => JoinLobbyIsRunning, async () =>
            {
                HttpClient httpClient = new HttpClient();
                var postData = new LobbyListInfo() { ShortId = (string)parameter, JoiningUser = IoC.Get<SettingsViewModel>().Username };
                var postResponse = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["NonProdBaseURL"] + "/lobby/join", postData);
                JsonResponse error;
                if (postResponse.IsSuccessStatusCode)
                {
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Lobby);
                }
                else
                {
                    error = await postResponse.Content.ReadFromJsonAsync<JsonResponse>();
                    ErrorMessage = error.Details;
                }
            });
        }
        public async void CreateLobbyAsync()
        {
            
            await RunCommandAsync(() => CreateLobbyIsRunning, async () =>
            {
                HttpClient httpClient = new HttpClient();
                var postData = new User
                {
                    Username = IoC.Get<SettingsViewModel>().Username
                };
                var postResponse = await httpClient.PostAsJsonAsync(ConfigurationManager.AppSettings["NonProdBaseURL"] + "/lobby/create", postData);
                JsonResponse error;
                if (postResponse.IsSuccessStatusCode)
                {
                    IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Lobby);
                }
                else 
                {
                    error = await postResponse.Content.ReadFromJsonAsync<JsonResponse>();
                    ErrorMessage = error.Details;
                }
            });
            
        }

        public void Close()
        {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Home);
        }

        public void Open()
        { 
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LobbyList);
           
        }


    }
}
