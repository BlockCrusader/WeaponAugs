using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs.Debuffs
{
    public class ResurgenceCooldown : ModBuff
	{
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        // Most buff effects in this mod are handled by GlobalPlayer and/or GlobalNPC instances. Hence why it appears to do nothing
    }
}