using System.IO;
using Orion.Framework.Events;
using Terraria;
using Microsoft.Xna.Framework;

namespace Orion.NetData.Packets
{
	public class PlayerUpdate : TerrariaPacketBase
	{
		public byte PlayerID { get; set; }
		public BitsByte Control = 0;
		public BitsByte Pulley = 0;
		public byte SelectedItem { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float VelocityX { get; set; }
		public float VelocityY { get; set; }

		internal PlayerUpdate(byte playerId) : base(PacketTypes.PlayerUpdate)
		{
			PlayerID = playerId;
			Player player = Main.player[PlayerID];
			Control[0] = player.controlUp;
			Control[1] = player.controlDown;
			Control[2] = player.controlLeft;
			Control[3] = player.controlRight;
			Control[4] = player.controlJump;
			Control[5] = player.controlUseItem;
			Control[6] = (player.direction == 1);

			Pulley[0] = player.pulley;
			Pulley[1] = (player.pulley && player.pulleyDir == 2);
			Pulley[2] = (player.velocity != Vector2.Zero);
			Pulley[3] = player.vortexStealthActive;
			Pulley[4] = (player.gravDir == 1f);

			SelectedItem = (byte)player.selectedItem;
			PositionX = player.position.X;
			PositionY = player.position.Y;

			if (Pulley[2])
			{
				VelocityX = player.velocity.X;
				VelocityY = player.velocity.Y;
			}
		}

		internal PlayerUpdate(BinaryReader reader) : base(reader)
		{
			PlayerID = reader.ReadByte();
			Control = reader.ReadByte();
			Pulley = reader.ReadByte();
			SelectedItem = reader.ReadByte();
			PositionX = reader.ReadSingle();
			PositionY = reader.ReadSingle();
			if (Pulley[2])
			{
				VelocityX = reader.ReadSingle();
				VelocityY = reader.ReadSingle();
			}
		}

		internal override void SetNewData(ref NetSendDataEventArgs e)
		{
			e.Number = PlayerID;
			Player player = Main.player[PlayerID];
			player.controlUp = Control[0];
			player.controlDown = Control[1];
			player.controlLeft = Control[2];
			player.controlRight = Control[3];
			player.controlJump = Control[4];
			player.controlUseItem = Control[5];
			player.direction = Control[6] ? 1 : -1;

			player.pulley = Pulley[0];
			if (Pulley[1])
			{
				player.pulley = true;
				player.pulleyDir = 2;
			}
			if (!Pulley[2])
			{
				player.velocity = Vector2.Zero;
			}
			else
			{
				player.velocity.X = VelocityX;
				player.velocity.Y = VelocityY;
			}

			player.vortexStealthActive = Pulley[3];

			if (Pulley[4])
			{
				player.gravDir = 1f;
			}

			player.selectedItem = SelectedItem;
			player.position.X = PositionX;
			player.position.Y = PositionY;
		}
	}
}