using System;
using System.Collections.Generic;
using System.Dynamic;
using Terraria;

namespace Orion
{
	public class OrionPlayer : DynamicObject
	{
		[Temporary("Needs to return Main.player at a given index")]
		public Player Player { get { return null; } }

		private readonly Dictionary<string, object> _extensions = new Dictionary<string, object>();

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			return _extensions.TryGetValue(binder.Name, out result);
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			_extensions[binder.Name] = value;

			return true;
		}
	}
}