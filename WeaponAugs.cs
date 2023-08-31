using System.IO;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs
{
	public class WeaponAugs : Mod
	{
		// The below code is adapted from Example Mod and handles netcode, namely Deathshroud's dodge effect
		internal enum MessageType : byte
		{
			DeathshroudDodge
		}
		public override void HandlePacket(BinaryReader reader, int whoAmI)
		{
			MessageType msgType = (MessageType)reader.ReadByte();

			switch (msgType)
			{
				case MessageType.DeathshroudDodge:
					GlobalAugPlayer.HandleDeathshroudNetcode(reader, whoAmI);
					break;
				default:
					Logger.WarnFormat("WeaponAugs: Unknown Message type: {0}", msgType);
					break;
			}
		}

		// Used for cross-mod content
		public class BWLCrossMod : ModSystem
		{
			public static bool bwlLoaded = false;

			public override void PostSetupContent()
			{
				bwlLoaded = ModLoader.HasMod("BSWLmod");
			}
		}

	}
}