using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Buffs.Debuffs
{
	public class Powertheft : ModBuff
	{
        public override string Texture => "WeaponAugs/Content/Buffs/Debuffs/Debuff";

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<GlobalAugNPC>().powerStolen = true;
		}
	}

	// Handles Powertheft lowering projectile damage
	public class PowertheftGlobalProj : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		private bool powerStolen = false;

		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return !entity.friendly && entity.hostile;
		}

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (source is EntitySource_Parent parent && parent.Entity is NPC npc)
			{
				if (npc.TryGetGlobalNPC(out GlobalAugNPC globalNPC))
				{
                    if (globalNPC.powerStolen)
                    {
						powerStolen = true;
					}
				}
			}
		}

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            if (powerStolen)
            {
				modifiers.FinalDamage *= 0.9f;
            }
        }
    }
}