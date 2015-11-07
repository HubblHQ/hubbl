using System;
using System.Threading.Tasks;
using System.Net.Http;
using Hubl.Core.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
			var responceRaw = await request.PostAsync (_baseUrl + "audio.get",content);
			string responceString = "";
			try {
				var bytes = await responceRaw.Content.ReadAsByteArrayAsync();
				responceString = System.Text.Encoding.GetEncoding ("windows-1251").GetString (bytes, 0, bytes.Length);
			}
			catch (Exception e) {
				var a = 4;
			}

			JArray VkAudioListJSon = (JArray)JObject.Parse (responceString) ["response"] ;

			var responce = new List<Track>();


			foreach (var i in VkAudioListJSon.Skip (1)) {
				try {
				var track = new Track ();
				track.Id = (string)i ["aid"];
				track.Artist = (string)i ["artist"];
				track.Name = (string)i ["title"];
				track.Duration = TimeSpan.FromSeconds((double)(int)i ["duration"]);
				track.Source = (string)i ["url"];
				track.SourceType = SourceType.VK;
				track.Genre = (string)i ["genre"];

				responce.Add (track); 
				}
				catch (Exception e) {
					continue;
				}
			}

			return responce;
			}
		}

	}

