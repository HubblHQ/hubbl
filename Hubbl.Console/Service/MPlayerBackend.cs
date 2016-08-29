using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using Hubbl.Core.Model;
using Hubbl.Core.Service;

namespace Hubbl.Console.Service
{
	public class MPlayerBackend : IMusicPlayerBackend
	{
		private const string MPLAYER_COMMAND = "C:\\Program Files\\mplayer\\mplayer.exe";

		public MPlayerBackend ()
		{
			_currentTrack = null;
			_currentTask = null;
			_taskCancellationTokenSource = new CancellationTokenSource ();
		    _mplayer = null;
		}

		private Track _currentTrack;
		private Task _currentTask;
		private CancellationTokenSource _taskCancellationTokenSource;
		// private CancellationToken _currentTaskCancellationToken;
		private TimeSpan _currentTrackStartTime;
	    private Process _mplayer;
	    private int _volume = 100;

        #region IMusicPlayerBackend implementation

        void IMusicPlayerBackend.PauseCurrentTrack()
        {
            if (_mplayer != null && !_mplayer.HasExited)
            {
                _mplayer.StandardInput.Write("pause\n");
            }
        }

	    void IMusicPlayerBackend.ResumeCurrentTrack()
	    {
            if (_mplayer != null && !_mplayer.HasExited)
            {
                _mplayer.StandardInput.Write("pause\n");
            }
        }

        public Track GetTrackInfo (string path)
		{
			Process mplayer = new Process ();
			mplayer.StartInfo.CreateNoWindow = true;
			mplayer.StartInfo.UseShellExecute = false;
			mplayer.StartInfo.ErrorDialog = false;
			mplayer.StartInfo.RedirectStandardOutput = true;
			mplayer.StartInfo.RedirectStandardInput = true;
			mplayer.StartInfo.RedirectStandardError = true;

			mplayer.StartInfo.FileName = MPLAYER_COMMAND;
			mplayer.StartInfo.Arguments = "-vo null -ao null -frames 0 -identify " + path; 
			// mplayer.StartInfo.Arguments = "-identify " + filename; 

			mplayer.Start ();
            
			// DANGER! WOLVES AHEAD!
			//TODO: may take REALLY long time or never exit if the file is unreacheable through network for some reason
			mplayer.WaitForExit ();

			var result = new Track();
			result.Source = path;

			while (!mplayer.StandardOutput.EndOfStream) {
				var line = mplayer.StandardOutput.ReadLine ();
				var tagLinePrefix = "ID_CLIP_INFO_NAME";
				if (line.StartsWith (tagLinePrefix)) {
					int separatorPos = line.IndexOf ('=');
					// var tagId = line.Substring (tagLinePrefix.Length, separatorPos - tagLinePrefix.Length);
					var tagName = line.Substring (separatorPos + 1);

					line = mplayer.StandardOutput.ReadLine ();
					separatorPos = line.IndexOf ('=');

					var tagValue = line.Substring (separatorPos + 1);
					switch (tagName) {
					case "Title":
						result.Name = tagValue;
						break;
					case "Artist":
						result.Artist = tagValue;
						break;
					}
				}

				if (line.StartsWith ("ID_LENGTH")) {
					int separatorPos = line.IndexOf ('=');
					var seconds = Double.Parse (line.Substring (separatorPos + 1), System.Globalization.CultureInfo.InvariantCulture);
					result.Duration = TimeSpan.FromSeconds (seconds);
				}
			}

			if (!(result.Duration.TotalSeconds > 0))
				result = null;

			return result;
		}

		void IMusicPlayerBackend.PlayTrack (Track track)
		{
		    if (_mplayer != null)
		    {
                if (!_mplayer.HasExited)
                	_mplayer.Kill ();
		        _mplayer = null;
		    }
            if (_currentTrack != null)
				_currentTrack = null;
			if (_currentTask != null) {
				_taskCancellationTokenSource.Cancel ();
				_currentTask = null;
			}

			if (track == null)
				return;
			var cancellationToken = _taskCancellationTokenSource.Token;
			_currentTrack = track;
			_currentTrackStartTime = new TimeSpan ();
			_currentTrack.Current = new TimeSpan(0);
			_currentTask = Task.Run (() => {
				Process mplayer = new Process ();
				mplayer.StartInfo.UseShellExecute = false;
				mplayer.StartInfo.ErrorDialog = false;
				mplayer.StartInfo.RedirectStandardOutput = true;
				mplayer.StartInfo.RedirectStandardInput = true;
				mplayer.StartInfo.RedirectStandardError = true;

				mplayer.StartInfo.FileName = MPLAYER_COMMAND;
				mplayer.StartInfo.Arguments = "-slave -volume " + _volume + " " + track.Source; 

				// cancellationToken.ThrowIfCancellationRequested ();
			    try
			    {
			        mplayer.Start();
			    }
			    catch (System.ComponentModel.Win32Exception ex)
			    {
			        System.Console.WriteLine(ex);
			    }

                _mplayer = mplayer;
                mplayer.WaitForExit();

                // while (!(cancellationToken.IsCancellationRequested || mplayer.HasExited)) {
                /*while (!mplayer.HasExited) {
					Thread.Sleep (100);
				    if (_doPauseAction)
				    {
                        mplayer.StandardInput.Write("pause\n");
				        _doPauseAction = false;
				    }
                    //MEGAZALEPA
                    System.Console.WriteLine (mplayer.StandardOutput.ReadToEnd ()); 
				}*/
                
			    //if (!mplayer.HasExited)
			    //	mplayer.Kill ();
			});
		}

	    void IMusicPlayerBackend.ChangeVolume(int volume)
	    {
	        _volume = volume;
            if (_mplayer != null && !_mplayer.HasExited)
            {
                _mplayer.StandardInput.Write("volume " + volume + " 1\n");
            }
        }


        Track IMusicPlayerBackend.CurrentPlayedTrack { get {
				if (_currentTrack != null && _currentTask.IsCompleted)
					_currentTrack = null;
				if (_currentTrack != null)
					_currentTrack.Current = new TimeSpan () - _currentTrackStartTime;
				return _currentTrack;
		} }

		#endregion
	}
}

