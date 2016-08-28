using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using Hubbl.Core.Model;
using Hubbl.Core.Service;

namespace Hubbl.Console.Tests
{
    internal class ConsoleTest
    {

        private const string MPLAYER_COMMAND = "C:\\Program Files\\mplayer\\mplayer.exe";
        private const string path = "https://psv4.vk.me/c4505/u29750645/audios/4fe7793cfe8f.mp3";
        public ConsoleTest()
        {
            
        }

        public void Run()
        {
            Process mplayer = new Process();
            mplayer.StartInfo.CreateNoWindow = true;
            mplayer.StartInfo.UseShellExecute = false;
            mplayer.StartInfo.ErrorDialog = false;
            mplayer.StartInfo.RedirectStandardOutput = true;
            mplayer.StartInfo.RedirectStandardInput = true;
            mplayer.StartInfo.RedirectStandardError = true;

            mplayer.StartInfo.FileName = MPLAYER_COMMAND;
            mplayer.StartInfo.Arguments = "";//"-vo null -ao null -frames 0 -identify " + path;
            //mplayer.StartInfo.Arguments = "-identify " + filename; 

            mplayer.Start();
            // DANGER! WOLVES AHEAD!
            //TODO: may take REALLY long time or never exit if the file is unreacheable through network for some reason
            mplayer.WaitForExit();

            var result = new Track();
            result.Source = path;

            while (!mplayer.StandardOutput.EndOfStream)
            {
                var line = mplayer.StandardOutput.ReadLine();
                var tagLinePrefix = "ID_CLIP_INFO_NAME";
                if (line.StartsWith(tagLinePrefix))
                {
                    int separatorPos = line.IndexOf('=');
                    // var tagId = line.Substring (tagLinePrefix.Length, separatorPos - tagLinePrefix.Length);
                    var tagName = line.Substring(separatorPos + 1);

                    line = mplayer.StandardOutput.ReadLine();
                    separatorPos = line.IndexOf('=');

                    var tagValue = line.Substring(separatorPos + 1);
                    switch (tagName)
                    {
                        case "Title":
                            result.Name = tagValue;
                            break;
                        case "Artist":
                            result.Artist = tagValue;
                            break;
                    }
                }

                if (line.StartsWith("ID_LENGTH"))
                {
                    int separatorPos = line.IndexOf('=');
                    var seconds = Double.Parse(line.Substring(separatorPos + 1), System.Globalization.CultureInfo.InvariantCulture);
                    result.Duration = TimeSpan.FromSeconds(seconds);
                }
            }

            if (!(result.Duration.TotalSeconds > 0))
                result = null;
        }
    }
}