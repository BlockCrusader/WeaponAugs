using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Content.Items.Others;

namespace WeaponAugs.Common
{
	public class ItemDrops : GlobalNPC
	{
		public class IsHardmodeNoUI : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return Main.hardMode;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class IsExpertHardmode : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return Main.expertMode && Main.hardMode;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class IsExpertPreHardmode : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return Main.expertMode && !Main.hardMode;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		// The below rule also have an associated config
		public class BonusShardsHardmode : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return Main.hardMode && ModContent.GetInstance<ConfigServer>().MoreShards;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class BonusShardsPreHardmode : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return !Main.hardMode && ModContent.GetInstance<ConfigServer>().MoreShards;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class IsPostPlantera : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return NPC.downedPlantBoss && ModContent.GetInstance<ConfigServer>().ProgressShards;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class IsPostGame : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return NPC.downedMoonlord && ModContent.GetInstance<ConfigServer>().ProgressShards;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class IsMasterHardmode : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return Main.masterMode && Main.hardMode && ModContent.GetInstance<ConfigServer>().MasterShards;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public class IsMasterPreHardmode : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info)
			{
				if (!info.IsInSimulation)
				{
					return Main.masterMode && !Main.hardMode && ModContent.GetInstance<ConfigServer>().MasterShards;
				}
				return false;
			}

			public bool CanShowItemDropInUI() => false;
			public string GetConditionDescription() => null;
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// Many replicas of exsisting rules are used to disable the Bestiary's ability to display these drops, which would otherwise be very cluttering
			LeadingConditionRule isHardmode = new(new IsHardmodeNoUI());
			LeadingConditionRule isExpertH = new(new IsExpertHardmode());
			LeadingConditionRule isExpertPH = new(new IsExpertPreHardmode());
			LeadingConditionRule bonusH = new(new BonusShardsHardmode());
			LeadingConditionRule bonusPH = new(new BonusShardsPreHardmode());
			LeadingConditionRule isMasterH = new(new IsMasterHardmode());
			LeadingConditionRule isMasterPH = new(new IsMasterPreHardmode());
			LeadingConditionRule postPlant = new(new IsPostPlantera());
			LeadingConditionRule postGame = new(new IsPostGame());

			// Basic: 54%, Uncommon: 29%, Rare: 17%
			IItemDropRule[] lootPoolPreHardmode = new IItemDropRule[] {
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardBas>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUnc>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardRar>(), 2),
				ItemDropRule.OneFromOptions(1, ModContent.ItemType<PowerShardBas>(), ModContent.ItemType<PowerShardUnc>(),
											   ModContent.ItemType<PowerShardRar>())
			};
			// Basic: 50%, Uncommon: 26%, Rare: 13%, Epic: 7%, Ultimate: 4%
			IItemDropRule[] lootPoolHardmode = new IItemDropRule[] {
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardBas>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUnc>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardRar>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardEpi>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUlt>(), 2),
				ItemDropRule.OneFromOptions(1, ModContent.ItemType<PowerShardBas>(), ModContent.ItemType<PowerShardUnc>(),
											   ModContent.ItemType<PowerShardRar>(), ModContent.ItemType<PowerShardEpi>(),
											   ModContent.ItemType<PowerShardUlt>())
			};

			// The rest of the loot pools are restricted to config toggled drop rolls

			// Uncommon: 75%, Rare: 25%
			IItemDropRule[] masterLootPH = new IItemDropRule[] {
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUnc>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardRar>(), 4),
				ItemDropRule.OneFromOptions(1, ModContent.ItemType<PowerShardUnc>(), ModContent.ItemType<PowerShardUnc>(),
											   ModContent.ItemType<PowerShardRar>())
			};
			// Uncommon: 52%, Rare: 27%, Epic: 15%, Ultimate: 6%
			IItemDropRule[] bountifulLootHardmode = new IItemDropRule[] {
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUnc>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardRar>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardEpi>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUlt>(), 3),
				ItemDropRule.OneFromOptions(1, ModContent.ItemType<PowerShardUnc>(), ModContent.ItemType<PowerShardRar>(), 
											   ModContent.ItemType<PowerShardEpi>(), ModContent.ItemType<PowerShardUlt>())
			};
			// Rare: 63%, Epic: 24%, Ultimate: 13%
			IItemDropRule[] bountifulLootPostGame = new IItemDropRule[] {
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardRar>(), 2),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardEpi>(), 3),
				ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PowerShardUlt>(), 5),
				ItemDropRule.OneFromOptions(1, ModContent.ItemType<PowerShardRar>(), ModContent.ItemType<PowerShardRar>(),
											   ModContent.ItemType<PowerShardEpi>(), ModContent.ItemType<PowerShardUlt>())
			};


			// Shards have some inital drop conditions which are very similar to Soul of Light/Night's
			if ((!npc.SpawnedFromStatue && npc.value >= 1f && npc.lifeMax > 1f && !npc.friendly && !NPCID.Sets.CannotDropSouls[npc.type])
				|| LunarEnemyOverride(npc.type))
            {
                if (!npc.boss && npc.type != NPCID.EaterofWorldsHead && npc.type != NPCID.EaterofWorldsBody && npc.type != NPCID.EaterofWorldsTail
					&& npc.type != NPCID.LunarTowerSolar && npc.type != NPCID.LunarTowerVortex && npc.type != NPCID.LunarTowerNebula && npc.type != NPCID.LunarTowerStardust)
                {
					npcLoot.Add(isHardmode);
					npcLoot.Add(isExpertPH); 
					npcLoot.Add(isExpertH);

					isHardmode.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(30, lootPoolHardmode)); 
					isHardmode.OnFailedConditions(ItemDropRule.SequentialRulesNotScalingWithLuck(40, lootPoolPreHardmode));

					// In expert mode, enemies get an extra drop attempt, alebit with slightly reduced odds
					isExpertH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(40, lootPoolHardmode));
					isExpertPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(50, lootPoolPreHardmode));

					// Toggleable bouns drop attempts
					npcLoot.Add(bonusPH);
					npcLoot.Add(bonusH);
					npcLoot.Add(isMasterPH);
					npcLoot.Add(isMasterH);
					npcLoot.Add(postPlant);
					npcLoot.Add(postGame);

					bonusH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(50, lootPoolHardmode));
					bonusPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(60, lootPoolPreHardmode));

					isMasterH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(50, bountifulLootHardmode));
					isMasterPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(60, masterLootPH));

					postPlant.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(50, bountifulLootHardmode));
					postGame.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(90, bountifulLootPostGame));
				}
                else if (npc.type != NPCID.Spazmatism && npc.type != NPCID.Retinazer 
					&& npc.type != NPCID.EaterofWorldsHead && npc.type != NPCID.EaterofWorldsBody && npc.type != NPCID.EaterofWorldsTail) // These have special, extra drop logic
                {
					// Bosses; these get 5 drop attempts in a row with vastly increased drop chances. Plus an extra two with even better RNG in expert
					// Loot bags in expert+ don't inherit these rolls; a single set of rolls is made by boss itself, just like before

					// x5 rolls
					npcLoot.Add(isHardmode);
					npcLoot.Add(isHardmode);
					npcLoot.Add(isHardmode);
					npcLoot.Add(isHardmode);
					npcLoot.Add(isHardmode);

					// x2 rolls
					npcLoot.Add(isExpertPH); 
					npcLoot.Add(isExpertH);
					npcLoot.Add(isExpertPH);
					npcLoot.Add(isExpertH);

					isHardmode.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(6, lootPoolHardmode));
					isHardmode.OnFailedConditions(ItemDropRule.SequentialRulesNotScalingWithLuck(8, lootPoolPreHardmode));

					isExpertH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(4, lootPoolHardmode));
					isExpertPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(6, lootPoolPreHardmode));

					// Toggleable bouns drop attempts

					// x2 rolls
					npcLoot.Add(bonusPH);
					npcLoot.Add(bonusH);
					npcLoot.Add(bonusPH);
					npcLoot.Add(bonusH);

					// Normal x1
					npcLoot.Add(isMasterPH);
					npcLoot.Add(isMasterH);
					npcLoot.Add(postPlant);
					npcLoot.Add(postGame);

					bonusH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(8, lootPoolHardmode));
					bonusPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(10, lootPoolPreHardmode));

					isMasterH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(6, bountifulLootHardmode));
					isMasterPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(8, masterLootPH));

					postPlant.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(10, bountifulLootHardmode));
					postGame.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(18, bountifulLootPostGame));
				}
			}
			// The EoW is a special exception, having decreased drop rates due to having a very large amount of parts
			if (System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
			{
				npcLoot.Add(isHardmode);
				npcLoot.Add(isExpertPH);
				npcLoot.Add(isExpertH);

				isHardmode.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(60, lootPoolHardmode));
				isHardmode.OnFailedConditions(ItemDropRule.SequentialRulesNotScalingWithLuck(80, lootPoolPreHardmode));

				isExpertH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(80, lootPoolHardmode));
				isExpertPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(100, lootPoolPreHardmode));

				// Toggleable bouns drop attempts
				npcLoot.Add(bonusPH);
				npcLoot.Add(bonusH);
				npcLoot.Add(isMasterPH);
				npcLoot.Add(isMasterH);
				npcLoot.Add(postPlant);
				npcLoot.Add(postGame);

				bonusH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(100, lootPoolHardmode));
				bonusPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(120, lootPoolPreHardmode));

				isMasterH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(100, bountifulLootHardmode));
				isMasterPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(120, masterLootPH));

				postPlant.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(100, bountifulLootHardmode));
				postGame.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(180, bountifulLootPostGame));
			}
			// Special condition for Twins. Limiting the encounter to a single set of drop attempts at the end
			if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
			{
				LeadingConditionRule twinsRule = new LeadingConditionRule(new Conditions.MissingTwin());
				twinsRule.OnSuccess(npcLoot.Add(isHardmode));
				twinsRule.OnSuccess(npcLoot.Add(isHardmode));
				twinsRule.OnSuccess(npcLoot.Add(isHardmode));
				twinsRule.OnSuccess(npcLoot.Add(isHardmode));
				twinsRule.OnSuccess(npcLoot.Add(isHardmode));

				twinsRule.OnSuccess(npcLoot.Add(isExpertPH));
				twinsRule.OnSuccess(npcLoot.Add(isExpertH));
				twinsRule.OnSuccess(npcLoot.Add(isExpertPH));
				twinsRule.OnSuccess(npcLoot.Add(isExpertH));

				isHardmode.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(6, lootPoolHardmode));
				isHardmode.OnFailedConditions(ItemDropRule.SequentialRulesNotScalingWithLuck(8, lootPoolPreHardmode));

				isExpertH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(4, lootPoolHardmode));
				isExpertPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(6, lootPoolPreHardmode));

				// Toggleable bonus drop attempts
				npcLoot.Add(bonusPH);
				npcLoot.Add(bonusH);
				npcLoot.Add(bonusPH);
				npcLoot.Add(bonusH);
				npcLoot.Add(isMasterPH);
				npcLoot.Add(isMasterH);
				npcLoot.Add(postPlant);
				npcLoot.Add(postGame);

				bonusH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(8, lootPoolHardmode));
				bonusPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(10, lootPoolPreHardmode));

				isMasterH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(6, bountifulLootHardmode));
				isMasterPH.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(8, masterLootPH));

				postPlant.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(10, bountifulLootHardmode));
				postGame.OnSuccess(ItemDropRule.SequentialRulesNotScalingWithLuck(18, bountifulLootPostGame));
			}
			// Void shards may rarely drop from ANY enemy
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidShard>(), 1500));
		}

        /// <summary>
        /// Returns if the given enemy type corresponds to an enemy from the Lunar Events. Used to allow these enemies to drop Shards of Power
        /// </summary>
        private static bool LunarEnemyOverride(int type)
        {
			int[] lunarEnemies = new int[]
			{
				NPCID.SolarDrakomire,
				NPCID.SolarDrakomireRider,
				NPCID.SolarSolenian,
				NPCID.SolarSpearman,
				NPCID.SolarSroller,
				NPCID.SolarCorite,
				NPCID.SolarCrawltipedeHead,
				NPCID.VortexLarva,
				NPCID.VortexHornet,
				NPCID.VortexHornetQueen,
				NPCID.VortexSoldier,
				NPCID.VortexRifleman,
				NPCID.NebulaBeast,
				NPCID.NebulaBrain,
				NPCID.NebulaHeadcrab,
				NPCID.NebulaSoldier,
				NPCID.StardustJellyfishBig,
				NPCID.StardustSpiderBig,
				NPCID.StardustSoldier,
				NPCID.StardustWormHead,
				NPCID.LunarTowerSolar,
				NPCID.LunarTowerVortex,
				NPCID.LunarTowerNebula,
				NPCID.LunarTowerStardust
			};
			foreach (int enemyType in lunarEnemies)
			{
				if (enemyType == type)
				{
					return true;
				}
			}
			return false;
		}
	}
}