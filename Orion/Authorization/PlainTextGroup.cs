using System.Collections.Generic;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;
using Orion.Extensions;
using Orion.Players;

namespace Orion.Authorization
{
	/// <summary>
	/// Plain-text user group used by the <see cref="PlainTextAccountService"/>.
	/// </summary>
	public class PlainTextGroup : IGroup
	{
		private IniData _iniData;
		private PlainTextAccountService _service;

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public IEnumerable<IUserAccount> Members { get; }

		/// <inheritdoc/>
		public IEnumerable<IPermission> Permissions { get; }

		/// <summary>
		/// Gets the computed group file path based on the group name.
		/// </summary>
		protected string GroupPath => Path.Combine(PlainTextAccountService.GroupPathPrefix, $"{Name.Slugify()}.ini");

		/// <summary>
		/// Initializes a new instance of the <see cref="PlainTextGroup"/> class.
		/// </summary>
		/// <param name="service">
		/// A reference to the <see cref="PlainTextAccountService"/> which owns this user group.
		/// </param>
		public PlainTextGroup(PlainTextAccountService service)
		{
			this._service = service;
			this._iniData = new IniData();

			this._iniData.Sections.AddSection("Group");
			this._iniData.Sections.AddSection("Membership");
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlainTextGroup"/> class with the provided group name, which
		/// will load the group data from disk.
		/// </summary>
		/// <param name="service">
		/// A reference to the <see cref="PlainTextAccountService"/> which owns this user group.
		/// </param>
		/// <param name="groupName">A string containing the group name to load from disk.</param>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="PlainTextGroup"/> class from the specified I/O stream.
		/// </summary>
		/// <param name="service">
		/// A reference to the <see cref="PlainTextAccountService"/> which owns this user group.
		/// </param>
		/// <param name="stream">The I/O stream to load the <see cref="PlainTextGroup"/> data from.</param>
		public PlainTextGroup(PlainTextAccountService service, Stream stream)
			: this(service)
		{
			using (StreamReader sr = new StreamReader(stream))
			{
				_iniData = new StreamIniDataParser().ReadData(sr);
			}
		}

		/// <inheritdoc/>
		public IUserAccount AddMember(IUserAccount userAccount)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc/>
		public void RemoveMember(IUserAccount player)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc/>
		public bool HasMember(IUserAccount player)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Saves this plain text group data to file in the pre-computed location.
		/// </summary>
		public void Save()
		{
			using (FileStream fs = new FileStream(GroupPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			{
				ToStream(fs);
			}
		}

		/// <summary>
		/// Saves this plain text group data into the specified <paramref name="stream"/>.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to save the data to.</param>
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
