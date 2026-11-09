using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("General")] 
    public string Name;
    public int Level;
    public int BlockGems; // Credits
    public int BossBlocks; // Credits
    public int CurrentBlockPower;
    public int CurrentScore;
    public int CollectedBlockGems;
    public int CurrentWave;
    public int CurrentCombo;
    public int CurrentRolls;
    public ShapeData CurrentShapeData;
    public BlockData CurrentSelectedBlockData;

    [Header("HighScores")]
    public int ZenHighscore;
    public int TakedownHighscore;

    [Header("Account Data")] 
    public string PlayerID;
    public DateTime? AccountCreationDate;

    [Header("Cloud Keys")] 
    public string NameKey;
    public string LevelKey;
    public string BlockGemsKey;
    public string BossBlocksKey;
    public string BlockDeckSlot1Key;
    public string BlockDeckSlot2Key;
    public string BlockDeckSlot3Key;
    public string BlockDeckSlot4Key;
    public string ZenHighscoreKey;
    public string TakedownHighscoreKey;

    [Header("Deck")] 
    public List<BlockData> PlayerBlockDeck;
    
    [Header("AllBlocks")]
    public List<BlockData> AllBlocks;
}   

