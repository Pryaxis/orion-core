using System;

namespace Orion.Items
{
	/// <summary>
	/// Specifies the prefix of an <see cref="IItem"/> instance.
	/// </summary>
	public struct ItemPrefix : IEquatable<ItemPrefix>
	{
#pragma warning disable 1591
		public static ItemPrefix Adept => (ItemPrefix)27;
		public static ItemPrefix Agile => (ItemPrefix)44;
		public static ItemPrefix Angry => (ItemPrefix)71;
		public static ItemPrefix Annoying => (ItemPrefix)50;
		public static ItemPrefix Arcane => (ItemPrefix)66;
		public static ItemPrefix Armored => (ItemPrefix)64;
		public static ItemPrefix Awful => (ItemPrefix)22;
		public static ItemPrefix Awkward => (ItemPrefix)24;
		public static ItemPrefix Brisk => (ItemPrefix)73;
		public static ItemPrefix Broken => (ItemPrefix)39;
		public static ItemPrefix Bulky => (ItemPrefix)12;
		public static ItemPrefix Celestial => (ItemPrefix)34;
		public static ItemPrefix Damaged => (ItemPrefix)40;
		public static ItemPrefix Dangerous => (ItemPrefix)3;
		public static ItemPrefix Deadly => (ItemPrefix)43;
		public static ItemPrefix DeadlyRanged => (ItemPrefix)20;
		public static ItemPrefix Demonic => (ItemPrefix)60;
		public static ItemPrefix Deranged => (ItemPrefix)31;
		public static ItemPrefix Dull => (ItemPrefix)10;
		public static ItemPrefix Fleeting => (ItemPrefix)74;
		public static ItemPrefix Forceful => (ItemPrefix)38;
		public static ItemPrefix Frenzying => (ItemPrefix)58;
		public static ItemPrefix Furious => (ItemPrefix)35;
		public static ItemPrefix Godly => (ItemPrefix)59;
		public static ItemPrefix Guarding => (ItemPrefix)63;
		public static ItemPrefix Hard => (ItemPrefix)62;
		public static ItemPrefix Hasty => (ItemPrefix)18;
		public static ItemPrefix HastyAccessory => (ItemPrefix)75;
		public static ItemPrefix Heavy => (ItemPrefix)14;
		public static ItemPrefix Hurtful => (ItemPrefix)53;
		public static ItemPrefix Ignorant => (ItemPrefix)30;
		public static ItemPrefix Inept => (ItemPrefix)29;
		public static ItemPrefix Intense => (ItemPrefix)32;
		public static ItemPrefix Intimidating => (ItemPrefix)19;
		public static ItemPrefix Intrepid => (ItemPrefix)79;
		public static ItemPrefix Jagged => (ItemPrefix)69;
		public static ItemPrefix Keen => (ItemPrefix)36;
		public static ItemPrefix Large => (ItemPrefix)1;
		public static ItemPrefix Lazy => (ItemPrefix)49;
		public static ItemPrefix Legendary => (ItemPrefix)81;
		public static ItemPrefix Lethargic => (ItemPrefix)23;
		public static ItemPrefix Light => (ItemPrefix)15;
		public static ItemPrefix Lucky => (ItemPrefix)68;
		public static ItemPrefix Manic => (ItemPrefix)52;
		public static ItemPrefix Massive => (ItemPrefix)2;
		public static ItemPrefix Masterful => (ItemPrefix)28;
		public static ItemPrefix Menacing => (ItemPrefix)72;
		public static ItemPrefix Murderous => (ItemPrefix)46;
		public static ItemPrefix Mystic => (ItemPrefix)26;
		public static ItemPrefix Mythical => (ItemPrefix)83;
		public static ItemPrefix Nasty => (ItemPrefix)51;
		public static ItemPrefix Nimble => (ItemPrefix)45;
		public static ItemPrefix None => (ItemPrefix)0;
		public static ItemPrefix Pointy => (ItemPrefix)6;
		public static ItemPrefix Powerful => (ItemPrefix)25;
		public static ItemPrefix Precise => (ItemPrefix)67;
		public static ItemPrefix Quick => (ItemPrefix)42;
		public static ItemPrefix QuickAccessory => (ItemPrefix)76;
		public static ItemPrefix Random => (ItemPrefix)(-1);
		public static ItemPrefix Rapid => (ItemPrefix)17;
		public static ItemPrefix Rash => (ItemPrefix)78;
		public static ItemPrefix Reforge => (ItemPrefix)(-2);
		public static ItemPrefix Ruthless => (ItemPrefix)57;
		public static ItemPrefix Savage => (ItemPrefix)4;
		public static ItemPrefix Shameful => (ItemPrefix)13;
		public static ItemPrefix Sharp => (ItemPrefix)5;
		public static ItemPrefix Shoddy => (ItemPrefix)41;
		public static ItemPrefix Sighted => (ItemPrefix)16;
		public static ItemPrefix Slow => (ItemPrefix)47;
		public static ItemPrefix Sluggish => (ItemPrefix)48;
		public static ItemPrefix Small => (ItemPrefix)9;
		public static ItemPrefix Spiked => (ItemPrefix)70;
		public static ItemPrefix Staunch => (ItemPrefix)21;
		public static ItemPrefix Strong => (ItemPrefix)54;
		public static ItemPrefix Superior => (ItemPrefix)37;
		public static ItemPrefix Taboo => (ItemPrefix)33;
		public static ItemPrefix Terrible => (ItemPrefix)8;
		public static ItemPrefix Tiny => (ItemPrefix)7;
		public static ItemPrefix Unhappy => (ItemPrefix)11;
		public static ItemPrefix Unpleasant => (ItemPrefix)55;
		public static ItemPrefix Unreal => (ItemPrefix)82;
		public static ItemPrefix Violent => (ItemPrefix)80;
		public static ItemPrefix Warding => (ItemPrefix)65;
		public static ItemPrefix Weak => (ItemPrefix)56;
		public static ItemPrefix Wild => (ItemPrefix)77;
		public static ItemPrefix Zealous => (ItemPrefix)61;
#pragma warning restore 1591

		/// <summary>
		/// Determines if two <see cref="ItemPrefix"/> instances are equal.
		/// </summary>
		/// <param name="type1">The first <see cref="ItemPrefix"/> instance.</param>
		/// <param name="type2">The second <see cref="ItemPrefix"/> instance.</param>
		/// <returns>true if <paramref name="type1"/> equals <paramref name="type2"/>, false otherwise.</returns>
		public static bool operator ==(ItemPrefix type1, ItemPrefix type2) => type1.Equals(type2);

		/// <summary>
		/// Converts a <see cref="ItemPrefix"/> instance into its integer representation.
		/// </summary>
		/// <param name="type">The <see cref="ItemPrefix"/> instance.</param>
		public static explicit operator int(ItemPrefix type) => type._prefix;

		/// <summary>
		/// Converts an integer into its <see cref="ItemPrefix"/> representation.
		/// </summary>
		/// <param name="i">The integer.</param>
		public static explicit operator ItemPrefix(int i) => new ItemPrefix(i);

		/// <summary>
		/// Determines if two <see cref="ItemPrefix"/> instances are not equal.
		/// </summary>
		/// <param name="type1">The first <see cref="ItemPrefix"/> instance.</param>
		/// <param name="type2">The second <see cref="ItemPrefix"/> instance.</param>
		/// <returns>
		/// true if <paramref name="type1"/> does not equal <paramref name="type2"/>, false otherwise.
		/// </returns>
		public static bool operator !=(ItemPrefix type1, ItemPrefix type2) => !type1.Equals(type2);

		private readonly int _prefix;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemPrefix"/> structure with the specified item prefix.
		/// </summary>
		/// <param name="prefix">The item prefix.</param>
		public ItemPrefix(int prefix)
		{
			_prefix = prefix;
		}

		/// <summary>
		/// Determines if this instance equals another <see cref="ItemPrefix"/> instance.
		/// </summary>
		/// <param name="other">The other <see cref="ItemPrefix"/> instance.</param>
		/// <returns>true if this instance equals <paramref name="other"/>, false otherwise.</returns>
		public bool Equals(ItemPrefix other) => _prefix == other._prefix;

		/// <summary>
		/// Determines if this instance equals another object instance.
		/// </summary>
		/// <param name="obj">The other object instance.</param>
		/// <returns>true if this instance equals <paramref name="obj"/>, false otherwise.</returns>
		public override bool Equals(object obj) => obj is ItemPrefix && Equals((ItemPrefix)obj);

		/// <summary>
		/// Gets the hash code for this instance.
		/// </summary>
		/// <returns>The hash code for this instance.</returns>
		public override int GetHashCode() => _prefix;

		/// <summary>
		/// Returns the string representation of this instance.
		/// </summary>
		/// <returns>The string representation of this instance.</returns>
		public override string ToString() => $"{_prefix}";
	}
}
