using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hubbl.Core.Model;
using Newtonsoft.Json.Linq;

namespace Hubbl.Core.ExternalModules.VkModule
{
	public class VkApiService
	{
		//Sample is:  https://api.vk.com/method/METHOD_NAME?PARAMETERS&access_token=ACCESS_TOKEN
		private const string _baseUrl = "https://api.vk.com/method/";
		private readonly HubblUser _user;

		public VkApiService (HubblUser u)
		{
			this._user = u;
		}

		public async Task<IEnumerable<Track>> GetUserAudioList (int offset, int count)
		{
			// Create an HTTP web request using the URL:
			var request = new HttpClient();
			var content = new FormUrlEncodedContent (
				new Dictionary<string, string>()
				{
					{"owner_id", _user.VkUserInfo.VkUserId},
					{"access_token", _user.VkUserInfo._token},
					{"count", count.ToString ()}
				});
			try {
				var responceRaw = await request.PostAsync (_baseUrl + "audio.get",content);
				var bytes = await responceRaw.Content.ReadAsByteArrayAsync();
				var responceString = System.Text.Encoding.GetEncoding ("windows-1251").GetString (bytes, 0, bytes.Length);
			

			JArray VkAudioListJSon = (JArray)JObject.Parse (responceString) ["response"] ;

			var responce = new List<Track>();
			
			foreach (var i in VkAudioListJSon.Skip (1)) {
				try {
				var track = new Track ();
				track.Id = (string)i ["aid"];
				track.Artist = (string)i ["artist"];
				track.Name = (string)i ["title"];
				track.Duration = TimeSpan.FromSeconds((int)i ["duration"]);
				track.Source = (string)i ["url"];
				track.SourceType = SourceType.VK;
				track.Genre = (string)i ["genre"];

				responce.Add (track); 
				}
				catch (Exception) {
					continue;
				}
			}
				return responce;
			}
			catch (Exception) {
				return new List<Track> ();
			}


			}
		}

	}

