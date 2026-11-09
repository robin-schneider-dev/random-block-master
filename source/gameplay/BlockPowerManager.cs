using System;
using Attribute;
using Channel;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class BlockPowerManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameChannel _gameChannel;
        [Expandable][SerializeField] private PlayerData _playerData;
        [Expandable][SerializeField] private GameData _gameData;
        
        [Header("Settings")]
        [SerializeField] private int _startBlockPower;
        [SerializeField] private int _startSpawnCost;
        private int _blockSpawnCost;
        [SerializeField] private int _costStep;
        
        [Header("Block Power UI")]
        [SerializeField] private GameObject _blockPowerUI;
        [SerializeField] private TextMeshProUGUI _blockPowerText;
        [SerializeField] private TextMeshProUGUI _blockSpawnCostText;

        private void OnEnable()
        {
            _gameChannel.OnRowCleared += OnSomethingCleared;
            _gameChannel.OnColumnCleared += OnSomethingCleared;
            _gameChannel.OnSquareCleared += OnSomethingCleared;
            _gameChannel.OnBlockPowerChanged += OnBlockPowerChanged;
            _gameChannel.OnRestart += OnRestart;
        }

        private void Start()
        { 
            _playerData.CurrentBlockPower = _startBlockPower;
            _blockSpawnCost = _startSpawnCost;
            _blockPowerText.text = _playerData.CurrentBlockPower + " BP";
            _blockPowerUI.SetActive(_gameData.GameMode == GameData.GameModeID.Takedown);
        }
        
        private void OnBlockPowerChanged(int bp)
        {
            _playerData.CurrentBlockPower += bp;
            _blockPowerText.text = _playerData.CurrentBlockPower + " BP";
        }
        
        public void OnBlockPowerSpawnPressed()
        {
            if (_playerData.CurrentBlockPower < _blockSpawnCost)
            {
                return;
            }
            _playerData.CurrentBlockPower -= _blockSpawnCost;
            _blockSpawnCost += _costStep;
            _blockSpawnCostText.text = _blockSpawnCost.ToString();
            _blockPowerText.text = _playerData.CurrentBlockPower.ToString();
            _playerData.CurrentBlockPower = _playerData.CurrentBlockPower;
            _gameChannel.AnnounceOnSpawnUniqueBlock();
        }

        public bool CanSpawnUniqueBlock()
        {
            return _playerData.CurrentBlockPower >= _blockSpawnCost;
        }
        
        private void OnSomethingCleared(int amount)
        {
            if (_gameData.GameMode != GameData.GameModeID.Takedown) return;
            if (_playerData.CurrentCombo > 0)
            {
                _playerData.CurrentBlockPower += 10 * amount * _playerData.CurrentCombo;
            }
            else
            {
                _playerData.CurrentBlockPower += 10 * amount;
            }     
            _blockPowerText.text = _playerData.CurrentBlockPower.ToString();
            _playerData.CurrentBlockPower = _playerData.CurrentBlockPower;
        }
        
        private void OnRestart()
        {
            _blockSpawnCost = _startSpawnCost;
            _blockSpawnCostText.text = _blockSpawnCost.ToString();
            _playerData.CurrentBlockPower = _startBlockPower;
            _blockPowerText.text = _playerData.CurrentBlockPower + " BP";
        }
        
        private void OnDisable()
        {
            _gameChannel.OnRowCleared -= OnSomethingCleared;
            _gameChannel.OnColumnCleared -= OnSomethingCleared;
            _gameChannel.OnSquareCleared -= OnSomethingCleared;
            _gameChannel.OnBlockPowerChanged -= OnBlockPowerChanged;
            _gameChannel.OnRestart -= OnRestart;
        }
        
        // todo: level block , damage -> shooting projectile, scales of projectiles.
    }
}
