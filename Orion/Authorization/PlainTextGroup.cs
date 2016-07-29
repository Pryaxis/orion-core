using System.Collections.Generic;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;
using Orion.Entities.Player;
using Orion.Extensions;

namespace Orion.Authorization
{
	public class PlainTextGroup : IGroup
	{
		protected IniData _iniData;
		protected PlainTextAccountService _service;

		///<inheritdoc />
		public string Name
		{
			get
			{
				return _iniData.Sections["Group"]["Name"];
			}
			set
			{
				_iniData.Sections["Group"]["Name"] = value;
				Save();
			}
		}

		///<inheritdoc />
		public string Description
		{
			get
			{
				return _iniData.Sections["Group"]["Description"];
			}
			set
			{
				_iniData.Sections["Group"]["Description"] = value;
				Save();
			}
		}

		public IEnumerable<IUserAccount> Members { get; }
		public IEnumerable<IPermission> Permissions { get; }

		/// <summary>
		/// Gets the computed group file path based on the group name.
		/// </summary>
		protected string GroupPath =>
			 Path.Combine(PlainTextAccountService.UserGroupPrefix, $"{Name.Slugify()}.ini");

		public PlainTextGroup(PlainTextAccountService service)
		{
			this._service = service;
			this._iniData = new IniData();

			this._iniData.Sections.AddSection("Group");
			this._iniData.Sections.AddSection("Membership");
		}

		public PlainTextGroup(PlainTextAccountService service, string groupName)
			: this(service)
		{
			Name = groupName;

			using (FileStream fs = new FileStream(GroupPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
			using (StreamReader sr = new StreamReader(fs))
			{
				_iniData = new StreamIniDataParser().ReadData(sr);
			}
		}

		public PlainTextGroup(PlainTextAccountService service, Stream stream)
			: this(service)
		{
			using (StreamReader sr = new StreamReader(stream))
			{
				_iniData = new StreamIniDataParser().ReadData(sr);
			}
		}

		public bool HasMember(IPlayer player)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Saves this plain text group to file in the pre-computed location.
		/// </summary>
		public void Save()
		{
			using (FileStream fs = new FileStream(GroupPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			{
				ToStream(fs);
			}
		}

		/// <summary>
		/// Saves this plain text account into the specified stream.
		/// </summary>
		public void ToStream(Stream stream)
		{
			var parser = new StreamIniDataParser();

			using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
			{
				parser.WriteData(sw, _iniData);
			}
		}
	}
}