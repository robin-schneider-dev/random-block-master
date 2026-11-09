using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using Unity.Services.Economy;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class CloudSaveManager : MonoBehaviour
{
    private bool _debug;
    private Dictionary<string, object> _dataToUploadDictionary;
    
    
    private void Awake()
    {
        _dataToUploadDictionary = new Dictionary<string, object>();
        _debug = !AuthenticationService.Instance.IsSignedIn;
    }

    public void FillUpdateData(string key, object data)
    {
        if(_debug) return;
        _dataToUploadDictionary.Add(key, data);
    }

    // public void ClearUpdatedDate()
    // {
    //     _dataToUploadDictionary.Clear();
    // }
    
    /// <summary>
    /// Called from: Game Over Manager on Game Over setting block gems, boss blocks and highscore!
    /// Called from: Loot Box Manager on add loot to player data
    /// Called from: Block Manager on Player Block Deck changed
    /// Called from: Menu Manager on player name change
    /// Called from: Top Bar Manager on loot box bought
    /// </summary>
    public async Task SaveMultipleUpdatedDataToCloud()
    {
        if(_debug) return;
        await SaveUpdatedData(_dataToUploadDictionary);
    }

    /// <summary>
    /// Called from: Currency Manager -> Todo: Check!
    /// Called from: Game Over Manager on Game Over
    /// </summary>
    public async Task SaveSingleUpdatedDataToCloud(string key, object data)
    {
        if(_debug) return;
        _dataToUploadDictionary.Clear();
        _dataToUploadDictionary.Add(key, data);
        // Check if access token is there?
        await SaveUpdatedData(_dataToUploadDictionary);
    }

    /// <summary>
    /// Updata data to cloud
    /// </summary>
    private async Task SaveUpdatedData(Dictionary<string, object> data)
    {
        if(_debug) return;
        try
        {
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            _dataToUploadDictionary.Clear(); // we clear after updating the saved data
            Debug.Log("Saving successfully!");
        }
        catch (Exception e)
        {
            Debug.Log("Could not save data!");
            // Debug.LogException(e);
        }
    }

    /// <summary>
    /// Updata data to economy
    /// </summary>
    // private async Task SaveUpdatedEconomyData(Dictionary<string, object> data)
    // {
    //     try
    //     {
    //         await EconomyService.Instance.(data);
    //         Debug.Log("Saving successfully!");
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogException(e);
    //     }
    // }

    /// <summary>
    /// Update data to leaderboards
    /// </summary>
    public async Task AddScoreToLeaderboard(string leaderboardKey, int score) 
    {
        if(_debug) return;
        // Score maybe as double
        try {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardKey, score);
            Debug.Log("Score submitted!");
        }
        catch (Exception e) {
            Debug.Log("Failed to submit score!");
            // throw;
        }
    }

}
