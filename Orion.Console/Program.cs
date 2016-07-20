namespace Orion.Console
{
	public class Program
	{
		static Orion orion;

		public static void Main(string[] args)
		{
			orion = new Orion();

			orion.StartServer();

			orion.Dispose();
		}
	}
}
