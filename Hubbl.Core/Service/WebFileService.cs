using System;
using System.Net.Http;
using System.Threading;
using PCLStorage;

namespace Hubbl.Core.Service
{
	public class WebFileService
	{
		public IDownloadTask Download(Uri uri, IFile file)
		{
			return new HttpDownloadFileTask(uri, file);
		}

		private class HttpDownloadFileTask : IDownloadTask
		{
			private readonly Uri _uri;
			private readonly IFile _file;
			private readonly CancellationTokenSource _cancellationTokenSource;

			public HttpDownloadFileTask(Uri uri, IFile file)
			{
				_uri = uri;
				_file = file;
				_cancellationTokenSource = new CancellationTokenSource();
			}

			public event EventHandler<DownloadFileProgressArgs> Report;
			public event EventHandler<DownloadFileComplite> Completed;
			public event EventHandler<DownloadFileErrorArgs> Error;
			public event EventHandler<DownloadFIleCancelArgs> Canceled;

			public async void Run()
			{
				var httpClient = new HttpClient();
				HttpResponseMessage response = null;
				try
				{
					response = await httpClient.GetAsync(_uri, _cancellationTokenSource.Token);
					var remoteStream = await response.Content.ReadAsStreamAsync();
					var localStream = await _file.OpenAsync(FileAccess.ReadAndWrite);
					//response.Content.Headers.ContentLength;
				}
				catch (Exception ex)
				{
					if (Error != null)
						Error(this, new DownloadFileErrorArgs(ex, _file));
				}
				finally
				{
					if (response != null)
						response.Dispose();
				}
			}

			public void Cancel()
			{
				_cancellationTokenSource.Cancel();
			}
		}
	}

	public class DownloadFileProgressArgs : EventArgs
	{
		public DownloadFileProgressArgs(long current, long count)
		{
			Current = current;
			Count = count;
		}

		public long Current { get; private set; }

		public long Count { get; private set; }
	}

	public class DownloadFileComplite : EventArgs
	{
		public DownloadFileComplite(IFile file)
		{
			File = file;
		}

		public IFile File { get; private set; }
	}

	public class DownloadFileErrorArgs : EventArgs
	{
		public DownloadFileErrorArgs(Exception exception, IFile file)
		{
			Exception = exception;
			File = file;
		}

		public Exception Exception { get; private set; }

		public IFile File { get; private set; }
	}

	public class DownloadFIleCancelArgs : EventArgs
	{
		public DownloadFIleCancelArgs(IFile file)
		{
			File = file;
		}

		public IFile File { get; private set; }
	}

	public interface IDownloadTask
	{
		event EventHandler<DownloadFileProgressArgs> Report;

		event EventHandler<DownloadFileComplite> Completed;

		event EventHandler<DownloadFileErrorArgs> Error;

		event EventHandler<DownloadFIleCancelArgs> Canceled;

		void Run();

		void Cancel();
	}
}
