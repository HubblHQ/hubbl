namespace Hubl.Core.Model
{
    public class User
    {
        public int Port { get; set; }

        public string IpAddress { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }

		public bool IsHub {get; set; }

		public string Hub {get; set; }
    }
}
