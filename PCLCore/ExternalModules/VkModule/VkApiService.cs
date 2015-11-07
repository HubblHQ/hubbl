using System;
using System.Threading.Tasks;
using System.Net.Http;
using Hubl.Core.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCLCore
{
	public class VkApiService
	{
		//Sample is:  https://api.vk.com/method/METHOD_NAME?PARAMETERS&access_token=ACCESS_TOKEN
		private const string _baseUrl = "https://api.vk.com/method/";
		private readonly User _user;

		public VkApiService (User u)
		{
			this._user = u;
		}

		private async Task<IEnumerable<Track>> GetUserAudioList (string url)
		{
			// Create an HTTP web request using the URL:
			var request = new HttpClient();
			var content = new FormUrlEncodedContent (
				new Dictionary<string, string>()
				{
					{"owner_id", _user.VkUserInfo.VkUserId},
					{"method", "audio.get"}
				});
			var responceRaw = await request.PostAsync ("http://vk.com/dev/audio.get",content);
			var responceString = await responceRaw.Content.ReadAsStringAsync ();
			JArray VkAudioListJSon = (JArray)JObject.Parse (responceString) ["responce"] ["items"];

			var responce = new List<Track>();


			foreach (var i in VkAudioListJSon) {
				var track = new Track ();
				track.Id = (string)i ["id"];
				track.Artist = (string)i ["artist"];
				track.Name = (string)i ["title"];
				track.Duration = TimeSpan.FromSeconds((double)(int)i ["duration"]);
				track.Source = (string)i ["url"];
				track.SourceType = SourceType.VK;
				track.Genre = (string)i ["genre_id"];

				responce.Add (track); 
			}

			return responce;
			}
		}

	}

