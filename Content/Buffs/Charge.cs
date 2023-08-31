using Humanizer;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Buffs
{
	public class Charge : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
		}

		float stackTracker = -1;
		public override void Update(Player player, ref int buffIndex)
		{
			int stacks = player.GetModPlayer<GlobalAugPlayer>().superchargeStacks;
			stackTracker = (float)stacks / 110f;
		}

		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
			float displayValue = (float)Math.Round(stackTracker, 2);
			tip = Language.GetTextValue("Mods.WeaponAugs.CommonItemtooltip.SuperchargeBuffDesc").FormatWith(displayValue);
		}
    }
}