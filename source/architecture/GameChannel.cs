using System;
using System.Collections.Generic;
using Data;
using PrefabCores;
using UnityEngine;

namespace Channel
{
    [CreateAssetMenu (menuName = "ScriptableObject/Channel/GameChannel")]
    public class GameChannel : ScriptableObject
    {
        public Action<ShapeCore> OnShapeDropped;
        public Action<ShapeCore> OnShapePlacementIsValid;
        public Action<ShapeCore> OnShapePlacementIsNotValid;
        public Action<ShapeCore> OnShapeRolled;
        public Action<int> OnScoreChanged;
        public Action<int> OnCurrencyChanged;
        public Action<int> OnCurrencyCollected;
        public Action<int> OnSquareCleared;
        public Action<int> OnRowCleared;
        public Action<int> OnColumnCleared;
        public Action<int> OnBlockPowerChanged;
        public Action<ProjectileData, Vector2, int> OnFireZenProjectile;
        public Action<ProjectileData, Vector2, int> OnFireProjectile;
        public Action<int> OnComboChanged;
        public Action<int> OnWaveChanged;
        public Action<int> OnBasicDamageDone;
        public Action<int> OnAbilityDamageDone;
        public Action<int> OnFinalCalculatedDamageDone;
        public Action<Vector3, int, ShapeCore> OnShapePickUp;
        public Action OnGameOver;
        public Action OnRestart;
        public Action OnShapeSet;
        public Action<BossData> OnBossDied;
        public Action<LootBoxData> OnLootBoxBought;
        public Action OnUseBlockForDeck;
        public Action<BlockData> OnSpawnRandomBlock;
        public Action<Vector2[], BlockData> OnUltimateBossAttack;
        public Action<BlockData, int> OnLootedBlock;
        public Action<BlockData> OnBlockUpgraded;
        public Action<bool> OnToggleSwipe;
        public Action<bool> OnSetDragZones;
        public Action OnSwiped;
        public Action OnSpawnUniqueBlock;
        public Action OnTutorialSectionFinished;
        public Action OnTutorialEnd;
        public Action<LootBoxData> OnLootboxInfoClicked;
        public Action<LootBoxData> OnLootboxBuyInfoClicked;
        // public Action<LootData> OnOpenLootBox;
        public Action<BlockData> OnBlockInfoClicked;
        public Action<GameData.GameModeID> OnLeaderboardInfoClicked;
        public Action OnSetupGameOver;
        public Action OnUpdateTopBar;
        public Action<int, int> OnBossGetsDamageAffected;
        public Action<int, int> OnBossGetsAbilityAffected;
    
        public void AnnounceOnShapeDropped(ShapeCore shapeCore){OnShapeDropped?.Invoke(shapeCore);}
        public void AnnounceOnShapePlacementIsValid(ShapeCore shapeCore){OnShapePlacementIsValid?.Invoke(shapeCore);}
        public void AnnounceOnShapePlacementIsNotValid(ShapeCore shapeCore){OnShapePlacementIsNotValid?.Invoke(shapeCore);}
        public void AnnounceOnShapeRolled(ShapeCore shapeCore){OnShapeRolled?.Invoke(shapeCore);}
        public void AnnounceOnScoreChanged(int score){OnScoreChanged?.Invoke(score);}
        public void AnnounceOnCurrencyChanged(int currency){OnCurrencyChanged?.Invoke(currency);}
        public void AnnounceOnCurrencyCollected(int currency){OnCurrencyCollected?.Invoke(currency);}
        public void AnnounceOnSquareCleared(int amount){OnSquareCleared?.Invoke(amount);}
        public void AnnounceOnRowCleared(int amount){OnRowCleared?.Invoke(amount);}
        public void AnnounceOnColumnCleared(int amount){OnColumnCleared?.Invoke(amount);}
        public void AnnounceOnBlockPowerChanged(int amount){OnBlockPowerChanged?.Invoke(amount);}
        public void AnnounceOnFireZenProjectile(ProjectileData projectileData, Vector2 spawnPos, int amount){OnFireZenProjectile?.Invoke(projectileData, spawnPos, amount);}
        public void AnnounceOnFireProjectile(ProjectileData projectileData, Vector2 spawnPos, int amount){OnFireProjectile?.Invoke(projectileData, spawnPos, amount);}
        public void AnnounceOnComboChanged(int combo){OnComboChanged?.Invoke(combo);}
        public void AnnounceOnWaveChanged(int combo){OnWaveChanged?.Invoke(combo);}
        public void AnnounceOnBasicDamageDone(int amount){OnBasicDamageDone?.Invoke(amount);}
        public void AnnounceOnAbilityDamageDone(int amount){OnAbilityDamageDone?.Invoke(amount);}
        public void AnnounceOnFinalCalculatedDamageDone(int amount){OnFinalCalculatedDamageDone?.Invoke(amount);}
        public void AnnounceOnShapePickUp(Vector3 pos, int position, ShapeCore shapeCore){OnShapePickUp?.Invoke(pos, position, shapeCore);}
        public void AnnounceOnGameOver(){OnGameOver?.Invoke();}
        public void AnnounceOnRestart(){OnRestart?.Invoke();}
        public void AnnounceOnShapeSet(){OnShapeSet?.Invoke();}
        public void AnnounceOnBossDied(BossData bossData){OnBossDied?.Invoke(bossData);}
        public void AnnounceOnLootBoxBought(LootBoxData lootBoxData){OnLootBoxBought?.Invoke(lootBoxData);}
        public void AnnounceOnSpawnBlock(BlockData blockData){OnSpawnRandomBlock?.Invoke(blockData);}
        public void AnnounceOnUltimateBossAttack(Vector2[] gridSlotKeys, BlockData blockData){OnUltimateBossAttack?.Invoke(gridSlotKeys, blockData);}
        public void AnnounceOnLootedBlock(BlockData blockData, int count){OnLootedBlock?.Invoke(blockData, count);}
        public void AnnounceOnBlockUpgraded(BlockData blockData){OnBlockUpgraded?.Invoke(blockData);}
        public void AnnounceOnToggleSwipe(bool canSwipe){OnToggleSwipe?.Invoke(canSwipe);}
        public void AnnounceOnSetDragZones(bool value){OnSetDragZones?.Invoke(value);}
        public void AnnounceOnUseBlockForDeck(){OnUseBlockForDeck?.Invoke();}
        public void AnnounceOnSwiped(){OnSwiped?.Invoke();}
        public void AnnounceOnSpawnUniqueBlock(){OnSpawnUniqueBlock?.Invoke();}
        public void AnnounceOnTutorialSectionFinished(){OnTutorialSectionFinished?.Invoke();}
        public void AnnounceOnTutorialEnd(){OnTutorialEnd?.Invoke();}
        public void AnnounceOnLootboxInfoClicked(LootBoxData lootBoxData){OnLootboxInfoClicked?.Invoke(lootBoxData);}
        public void AnnounceOnLootboxBuyInfoClicked(LootBoxData lootBoxData){OnLootboxBuyInfoClicked?.Invoke(lootBoxData);}
        // public void AnnounceOnOpenLootBox(LootData lootData){OnOpenLootBox?.Invoke(lootData);}
        public void AnnounceOnBlockInfoClicked(BlockData blockData){OnBlockInfoClicked?.Invoke(blockData);}
        public void AnnounceOnLeaderboardInfoClicked(GameData.GameModeID gameModeID){OnLeaderboardInfoClicked?.Invoke(gameModeID);}
        public void AnnounceOnSetupGameOver(){OnSetupGameOver?.Invoke();}
        public void AnnounceOnUpdateTopBar(){OnUpdateTopBar?.Invoke();}
        public void AnnounceOnBossGetsDamageAffected(int rounds, int amount){OnBossGetsDamageAffected?.Invoke(rounds, amount);}
        public void AnnounceOnBossGetsAbilityAffected(int rounds, int amount){OnBossGetsAbilityAffected?.Invoke(rounds, amount);}
    }
}
