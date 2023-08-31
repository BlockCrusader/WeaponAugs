using Terraria;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Buffs.Debuffs
{
	public class RallyRar : ModBuff
	{
		public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			if (npc.GetGlobalNPC<GlobalAugNPC>().rallyTier > 1)
			{
				npc.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			npc.GetGlobalNPC<GlobalAugNPC>().rallyTier = 1;
		}
	}

	public class RallyEpi : ModBuff
	{
		public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			if (npc.GetGlobalNPC<GlobalAugNPC>().rallyTier > 2)
			{
				npc.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			npc.GetGlobalNPC<GlobalAugNPC>().rallyTier = 2;
		}
	}

	public class RallyUlt : ModBuff
	{
		public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalAugNPC>().rallyTier = 3;
		}
	}
}