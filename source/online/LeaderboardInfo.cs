using System;
using System.Threading.Tasks;
using Channel;
using TMPro;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PrefabCores
{
    public class LeaderboardInfo : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private GameObject _showHide;
        [SerializeField] private GameData _gameData;
        
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _leaderboardHeader;
        
        [Header("GameObjects")]
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private GameObject _leaderboardItemPrefab;

        [Header("Player Score UI")] 
        [SerializeField] private LeaderboardItemCore _playerLeaderboardItemPrefab;

        [Header("Visual")] 
        [SerializeField] private Color _firstPlaceColor;
        [SerializeField] private Color _secondPlaceColor;
        [SerializeField] private Color _thirdPlaceColor;
        [SerializeField] private Color _normalPlaceColor;
        [SerializeField] private Color _playerPlaceColor;

        private void OnEnable()
        {
            _gameChannel.OnLeaderboardInfoClicked += Setup;
        }

        private void Update()
        {
            // add score to leaderboard
            if (Input.GetKeyDown(KeyCode.S))
            {
                
            }
        }

        private async void Setup(GameData.GameModeID gameModeID)
        {
            foreach (Transform child in _itemContainer)
            {
                Destroy(child.gameObject);
            }
            
            switch (gameModeID)
            {
                case GameData.GameModeID.Zen:
                    _leaderboardHeader.text = "Zen-Leaderboard";
                    // Get data from unity leaderboards or from cloud saves..
                    await LoadLeaderboardScoresAsync(_gameData.ZenLeaderboardKey);
                    await LoadPlayerScoreAsync(_gameData.ZenLeaderboardKey);
                    // Get Names and Scores, maybe other stuff
                    // Setup max 100 _leaderboardItemPrefabs in _itemContainer
                    break;
                case GameData.GameModeID.Takedown:
                    _leaderboardHeader.text = "Takedown-Leaderboard";
                    await LoadLeaderboardScoresAsync(_gameData.TakedownLeaderboardKey);
                    await LoadPlayerScoreAsync(_gameData.TakedownLeaderboardKey);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameModeID), gameModeID, null);
            }
            _showHide.SetActive(true);
        }
        
        /// <summary>
        /// Setup leaderboard from given key
        /// </summary>
        private async Task LoadLeaderboardScoresAsync(string leaderboardKey) 
        {
            try
            {
                LeaderboardScoresPage scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardKey);
                for (int i = 0; i < scoresResponse.Results.Count; i++)
                {
                    LeaderboardEntry leaderboardEntry = scoresResponse.Results[i];
                    if (leaderboardEntry.Score == 0) continue;
                    GameObject entry = Instantiate(_leaderboardItemPrefab, _itemContainer);
                    LeaderboardItemCore leaderboardItemCore = entry.GetComponent<LeaderboardItemCore>();
                    switch (i)
                    {
                        case 0:
                            leaderboardItemCore.Setup(leaderboardEntry.Rank + 1, GetCleanPlayerName(leaderboardEntry.PlayerName), leaderboardEntry.Score, _firstPlaceColor);
                            break;
                        case 1:
                            leaderboardItemCore.Setup(leaderboardEntry.Rank + 1, GetCleanPlayerName(leaderboardEntry.PlayerName), leaderboardEntry.Score, _secondPlaceColor);
                            break;
                        case 2:
                            leaderboardItemCore.Setup(leaderboardEntry.Rank + 1, GetCleanPlayerName(leaderboardEntry.PlayerName), leaderboardEntry.Score, _thirdPlaceColor);
                            break;
                        default:
                            leaderboardItemCore.Setup(leaderboardEntry.Rank + 1, GetCleanPlayerName(leaderboardEntry.PlayerName), leaderboardEntry.Score, _normalPlaceColor);
                            break;
                    }
                }
            }
            catch (Exception e) {
                Debug.Log("Couldn't fetch scores: " + e);
                throw;
            }
        }

        /// <summary>
        /// Cut numbers from name id
        /// </summary>
        private static string GetCleanPlayerName(string playerName)
        {
            string[] splitArray = playerName.Split(char.Parse("#"));
            return splitArray[0];
        }

        /// <summary>
        /// Setup player score entry
        /// </summary>
        private async Task LoadPlayerScoreAsync(string leaderboardKey)
        {
            try
            {
                LeaderboardEntry entry = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardKey);
                if (entry.Score == 0) _playerLeaderboardItemPrefab.Setup(0, GetCleanPlayerName(entry.PlayerName), 0, _playerPlaceColor);
                _playerLeaderboardItemPrefab.Setup(entry.Rank + 1, GetCleanPlayerName(entry.PlayerName), entry.Score, _playerPlaceColor);
            }
            catch (Exception e) {
                Debug.Log("Couldn't fetch player score: " + e);
                throw;
            }
        }

        public void OnBackButtonPressed()
        {
            _showHide.SetActive(false);
        }

        private void OnDisable()
        {
            _gameChannel.OnLeaderboardInfoClicked -= Setup;
        }
    }
}
