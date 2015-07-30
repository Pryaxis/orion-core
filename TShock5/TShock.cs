using Orion;
using System;
using System.Reflection;

namespace TShock5
{
	[OrionVersion(1, 0)]
	public class TShock : OrionPlugin
	{
		public override string Author
		{
			get
			{
				return "Nyx Studios";
			}
		}

		public override string Name
		{
			get
			{
				return "TShock 5";
			}
		}

		public override int Order
		{
			get
			{
				return 1;
			}
		}

		public override Version Version
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version;
			}
		}
		protected TShock(Orion.Orion instance) :
			base(instance)
		{
		}

		public override void Initialize()
		{
		}

		protected override void Dispose(bool disposing)
		{
			Dispose();
		}
	}
}
