using Terraria;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Buffs.Debuffs
{
	public class RendUnc : ModBuff
	{
        public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
            if (npc.GetGlobalNPC<GlobalAugNPC>().rendTier > 1)
            {
				npc.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			npc.GetGlobalNPC<GlobalAugNPC>().rendTier = 1;
		}
	}

	public class RendRar : ModBuff
	{
		public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			if (npc.GetGlobalNPC<GlobalAugNPC>().rendTier > 2)
			{
				npc.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			npc.GetGlobalNPC<GlobalAugNPC>().rendTier = 2;
		}
	}

	public class RendEpi : ModBuff
	{
		public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			if (npc.GetGlobalNPC<GlobalAugNPC>().rendTier > 3)
			{
				npc.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			npc.GetGlobalNPC<GlobalAugNPC>().rendTier = 3;
		}
	}

	public class RendUlt : ModBuff
	{
		public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalAugNPC>().rendTier = 4;
		}
	}
}