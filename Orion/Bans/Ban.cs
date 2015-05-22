namespace Orion.Bans
{
	public class Ban
	{
		public string Username { get; set; }
		public string UUID { get; set; }
		public string IP { get; set; }
		public long Expiration { get; set; }

		public Ban(string username, string uuid, string ip, long expiration)
		{
			Username = username;
			UUID = uuid;
			IP = ip;
			Expiration = expiration;
		}
	}
}