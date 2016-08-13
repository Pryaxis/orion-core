namespace Orion.Console
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			using (var orion = new Orion())
			{
				orion.Start(args);
			}
		}
	}
}
