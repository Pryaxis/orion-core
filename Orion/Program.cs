namespace Orion
{
	internal static class Program
	{
		internal static void Main(string[] args)
		{
			using (var orion = new Orion())
			{
				orion.Start(args);
			}
		}
	}
}
