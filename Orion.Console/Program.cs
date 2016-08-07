namespace Orion.Console
{
	public class Program
	{
		private static Orion _orion;
		
		public static void Main(string[] args)
		{
			using (_orion = new Orion())
			{
				_orion.StartServer();
			}
		}
	}
}
