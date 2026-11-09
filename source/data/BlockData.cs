using System.Collections.Generic;
using System.ComponentModel;
using Attribute;
using Data;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Data/BlockData")]
public class BlockData : ScriptableObject
{
    [Header("General")]
    public string Name;
    [TextArea(minLines: 2, maxLines: 4)]
    public string Description;
    public BlockID BlockType;
    // public int BasicDamage;
    // public int AbilityDamage;
    public Color MainColor;
    public Color BackgroundColor;
    public Color DesignColor;
    public AudioClip CounterSound;
    public int Class;
    public RarityID RarityType;
    public int Count;
    public bool IsDesigned;
    //public int UpgradeBlockGemCost; // these will always constant for every block!
    public List<int> BasicDamageValues;
    public List<int> AbilityDamageValues;
    public List<int> UpgradeBlockCountCost;

    // public int ThrustBlockDirection; // todo: change this or use for other ?

    [Header("Cloud")] 
    public string BlockCountKey;
    public string BlockClassKey;
    
    [Header("Visual")] 
    public Sprite Sprite;
    public Sprite BackgroundSprite;
    public Sprite DesignSprite;
    // public Sprite GridSlotAffectedSprite;
    public Material MainMaterial;
    public Material BackgroundMaterial;
    public Material DesignMaterial;
    [Expandable]public ProjectileData BaseProjectileData;
    [Expandable]public ProjectileData AbilityProjectileData;

    public readonly int[] UpgradeBlockGemCost = new int[8]
    {
        100,
        1000,
        2500,
        5000,
        10000,
        25000,
        50000,
        100000
    };



    public enum BlockID
    {
        // Common
        Base,
        Steady,
        Coin,
        Collector,
        Stone,
        Brick,
        // Rare
        Lonely,
        BasicBuff,
        Mid,
        Edge,
        Twin,
        Neighbour,
        // Epic
        Trans,
        Bomb,
        Cursed,
        Redemption,
        AbilityBuff,
        Drain,
        // Legendary
        Atom,
        Copy,
        Jade,
        Level,
        Soul,
        Random,
        
        // Other
        // Thrust,
        // Pull,
        // Boosted,
        // Halleluja,
        // Redemption,
    }

    public enum RarityID
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    
    /**
     * How to add a new Block:
     * 1. Add Block ID to Block Data
     * 2. In Script 'GridSlot'
     *      -> add block characteristic
     *      -> add block type in setup and use case for specific blocks
     *      -> may need to add a counter
     *      -> if counter is need add new helper count method
     *      -> if characteristic blocks grid slot return in shape set
     *      -> in 'Set Grid Slot' reset characteristic
     *      -> if block has affections make sure to set tag and check trigger functions
     * 3. In Script 'GridManager'
     *      -> Look in 'OnShapeDropped' and add condition for new block
     */
}

