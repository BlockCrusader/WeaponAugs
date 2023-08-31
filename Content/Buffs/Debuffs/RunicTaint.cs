using Terraria;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Buffs.Debuffs
{
	public class RunicTaint : ModBuff
	{
        public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalAugNPC>().runicTaint = true;
		}
	}
}