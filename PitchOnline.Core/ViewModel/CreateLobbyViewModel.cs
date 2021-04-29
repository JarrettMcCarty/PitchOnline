using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.IO;

namespace PitchOnline.Core
{
    public class CreateLobbyViewModel : BaseViewModel
    {
        public ICommand CreateLobbyCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public bool CreatingLobby { get; set; }
        public string Lobbyname { get; set; }
        public bool IsPrivate { get; set; } = false;
        public int Players { get; set; } = 1;
        public List<byte[]> Avatars { get; set; }

        public bool IsJoinable { get; set; } = true;

        public string LobbyKey => GenerateKey();
        public CreateLobbyViewModel()
        {
            CreateLobbyCommand = new RelayParameterizedCommand(async (parameter) => await CreatingLobbyAsync(parameter));
            CloseCommand = new RelayCommand(Close);
        }

        public void Close()
        {
            CreatingLobby = false;
            Lobbyname = string.Empty;
            IsPrivate = false;
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LobbyList);
        }

        public async Task CreatingLobbyAsync(object parameter)
        {
            await RunCommandAsync(() => CreatingLobby, async () =>
            {
                // Go to the Lobby, will need to figure out how to go from creating room to DB, 
                // to then pulling in info of freshly created lobby on creation

                // put new Lobby info into the DB
                /*
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://cardgameapi.azurewebsites.net");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(new LobbyListInfo()
                    {
                        LobbyKey = LobbyKey,
                        Lobbyname = Lobbyname,
                        IsJoinable = IsJoinable,
                        IsPrivate = IsPrivate,
                        Avatars = Avatars,
                        Players = Players
                    }));
                }
                */
                // Go to the new Lobby
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Lobby);
            });
        }

        private string GenerateKey()
        {
            // get all active private lobbies keys into a list to check againt generated one to be sure they dont match
            List<string> ActivePrivateLobbyKeys = new List<string>();
            var key = RandomString(10);
            while (ActivePrivateLobbyKeys.Contains(key))
                key = RandomString(10);
            return key;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
