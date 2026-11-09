using System.Collections;
using Attribute;
using Channel;
using PrefabCores;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class BossManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private GameData _gameData;
        [SerializeField] private PlayerData _playerData;

        [Header("Core")] 
        [SerializeField] private BossCore _bossCore;
        
        [Header("Boss Data")]
        [SerializeField] private int _specialBossRatio;
        [Expandable][SerializeField] private BossData _baseBossData;
        [SerializeField] private BossData[] _specialBossDatas; // in order
        private BossData _currentBossData;
    
        [Header("Boss UI")]
        [SerializeField] private GameObject _bossUI;
        [SerializeField] private TextMeshProUGUI _bossWaveCounterText;
        [SerializeField] private TextMeshProUGUI _bossAttackCounterText;
        
        [Header("Health Specs")]
        [SerializeField] private int _startHealth;
        [SerializeField] private int _additionHealthGrow;
        
        [Header("BlockPower Specs")]
        [SerializeField] private int _startBlockPowerDrop;
        [SerializeField] private int _additionBlockPowerDropGrow;
        
        private int _multiplier;
        private int _healthAddition;
        private int _blockPowerAddition;
        private int _attackCounter;

        // Specs
        private int _bossKillCounter;
        private int _specialBossKillCounter;
        private int _specialBossCounter;

        private bool _isDead;
        private bool _isSpecialBoss;

        
        /// <summary>
        /// Register events
        /// </summary>
        private void OnEnable()
        {
            // _gameChannel.OnBossDied += OnBossDied;
            if (_gameData.GameMode != GameData.GameModeID.Takedown) return;
            _gameChannel.OnShapePlacementIsValid += OnShapePlacementIsValid;
            _gameChannel.OnBasicDamageDone += OnBasicDamageDone;
            _gameChannel.OnAbilityDamageDone += OnAbilityDamageDone;
            _gameChannel.OnBossGetsDamageAffected += OnBossGetsDamageAffected;
            _gameChannel.OnBossGetsAbilityAffected += OnBossGetsAbilityAffected;
            _gameChannel.OnRestart += OnRestart;
        }

        /// <summary>
        /// Reset values of boss manager specs
        /// </summary>
        private void OnRestart()
        {
            _bossKillCounter = 0;
            _specialBossKillCounter = 0;
            _healthAddition = 0;
            _attackCounter = 0;
            _blockPowerAddition = 0;
            _multiplier = 1;
            _bossWaveCounterText.text = "0";
            StartCoroutine(DeathCoroutine());
        }
        
        /// <summary>
        /// Called from projectile manager on boss hit, contains projectile values
        /// </summary>
        private void OnBossGetsDamageAffected(int rounds, int amount)
        {
            _currentBossData.IsDamageAffected = true;
            _currentBossData.DamageAffectionRounds = rounds;
            _currentBossData.DamageAffectionValue = amount;
            _bossCore.SetupAffectionIcons();
            // _bossCore.SetBossDamageAffected(true, rounds, amount);
        }

        /// <summary>
        /// Called from projectile manager on boss hit, contains projectile values
        /// </summary>
        private void OnBossGetsAbilityAffected(int rounds, int amount)
        {
            _currentBossData.IsAbilityAffected = true;
            _currentBossData.AbilityAffectionRounds = rounds;
            _currentBossData.AbilityAffectionValue = amount;
            _bossCore.SetupAffectionIcons();
            // _bossCore.SetBossAbilityAffected(true, rounds, amount);
        }

        /// <summary>
        /// Set the done damage to current boss -> handle affection states
        /// </summary>
        private void OnBasicDamageDone(int amount)
        {
            // Debug.Log(amount);
            if (_currentBossData.IsDamageAffected && _currentBossData.DamageAffectionRounds > 0)
            {
                _currentBossData.Health -= amount * _currentBossData.DamageAffectionValue;
                _gameChannel.AnnounceOnFinalCalculatedDamageDone(amount * _currentBossData.DamageAffectionValue);
            }
            else
            {
                _currentBossData.Health -= amount;
                _gameChannel.AnnounceOnFinalCalculatedDamageDone(amount);
            }
            
            SetRest();
        }
        
        /// <summary>
        /// Set the done damage to current boss -> handle affection states
        /// </summary>
        private void OnAbilityDamageDone(int amount)
        {
            if (_currentBossData.IsAbilityAffected && _currentBossData.AbilityAffectionRounds > 0)
            {
                _currentBossData.Health -= amount * _currentBossData.AbilityAffectionValue;
                _gameChannel.AnnounceOnFinalCalculatedDamageDone(amount * _currentBossData.AbilityAffectionValue);
            }
            else
            {
                _currentBossData.Health -= amount;
                _gameChannel.AnnounceOnFinalCalculatedDamageDone(amount);
            }

            SetRest();
        }

        private void SetRest()
        {
            _bossCore.SetBossHealth();
            
            if (_currentBossData.Health <= 0 && !_isDead)
            {
                _isDead = true;
                _gameChannel.AnnounceOnBossDied(_currentBossData);
                
                if (_currentBossData.BossType != BossData.BossId.BaseBoss)
                {
                    _specialBossKillCounter++;
                }
                _bossKillCounter++;
                _playerData.CurrentWave = _bossKillCounter;
                _bossWaveCounterText.text = _bossKillCounter.ToString();
                _gameChannel.AnnounceOnBlockPowerChanged(_currentBossData.BlockPowerDrop);
                StartCoroutine(DeathCoroutine());
            }
        }

        private IEnumerator DeathCoroutine()
        {
            _bossCore.KillBoss();
            yield return new WaitForSeconds(1f);
            SetupBossToSpawn();
            _bossCore.SpawnBoss();
        }
        
        /// <summary>
        /// Check for game mode and enable or disable boss stuff
        /// </summary>
        private void Start()
        {
            if (_gameData.GameMode == GameData.GameModeID.Takedown)
            {
                // ResetBossSpecs();
                // _currentBossData = _baseBossData;
                // _bossCore.SetupBoss(_currentBossData);
                _multiplier = 1;
                SetupBossToSpawn();
                // Setup();
            }
            else
            {
                DisableBossStuff();
            }
        }

        /// <summary>
        /// Setups boss to spawn next
        /// </summary>
        private void SetupBossToSpawn()
        {
            if (_bossKillCounter % _specialBossRatio != 0 || _bossKillCounter == 0)
            {
                _isSpecialBoss = false;
                
                _baseBossData.Health = _startHealth + _healthAddition * _multiplier;
                _healthAddition += _additionHealthGrow;
                
                _baseBossData.BlockPowerDrop = _startBlockPowerDrop + _blockPowerAddition * _multiplier;
                _blockPowerAddition += _additionBlockPowerDropGrow;

                _baseBossData.AttackCounter = _baseBossData.AttackRatio;
                
                _currentBossData = _baseBossData;
            }
            else
            {
                _isSpecialBoss = true;
                // Get special boss wii special boss kill counter
                _specialBossDatas[_specialBossCounter].Health = _startHealth + _healthAddition * _multiplier;
                _specialBossDatas[_specialBossCounter].Health *= 2;
                _healthAddition += _additionHealthGrow;
                
                _specialBossDatas[_specialBossCounter].BlockPowerDrop = _startBlockPowerDrop + _blockPowerAddition * _multiplier;
                _blockPowerAddition += _additionBlockPowerDropGrow;

                _specialBossDatas[_specialBossCounter].AttackCounter = _specialBossDatas[_specialBossCounter].AttackRatio;

                
                _currentBossData = _specialBossDatas[_specialBossCounter];
                
                if (_specialBossCounter == _specialBossDatas.Length - 1)
                {
                    _specialBossCounter = 0;
                }
                else
                {
                    _specialBossCounter++;
                }
            }

            SetAttackCounter();
            ResetBossValues();
            _isDead = false;
            _bossCore.SetupBoss(_currentBossData);
            if (_isSpecialBoss)
            {
                _bossCore.DoUltimateBossAttack();
            }
        }

        /// <summary>
        /// Helper function
        /// </summary>
        private void DisableBossStuff()
        {
            _bossCore.gameObject.SetActive(false);
            _bossUI.SetActive(false);
        }
        
        /// <summary>
        /// Helper function
        /// </summary>
        public void ResetBossValues()
        {
            _currentBossData.IsDamageAffected = false;
            _currentBossData.IsAbilityAffected = false;
            _currentBossData.DamageAffectionValue = 1;
            _currentBossData.DamageAffectionRounds = 0;
            _currentBossData.AbilityAffectionValue = 1;
            _currentBossData.AbilityAffectionRounds = 0;
        }

        /// <summary>
        /// Helper function
        /// </summary>
        private void ResetBossSpecs()
        {
            _bossKillCounter = 0;
            _specialBossKillCounter = 0;
            _specialBossCounter = 0;
            _baseBossData.Health = _startHealth;
            _baseBossData.BlockPowerDrop = _startBlockPowerDrop;
        }
        
        /// <summary>
        /// Setup ui attack counter from current boss
        /// </summary>
        private void OnShapePlacementIsValid(ShapeCore shapeCore)
        {
            _currentBossData.AttackCounter -= 1;
            _currentBossData.DamageAffectionRounds -= 1;
            _currentBossData.AbilityAffectionRounds -= 1;
            if (_currentBossData.DamageAffectionRounds <= 0)
            {
                _currentBossData.IsDamageAffected = false;
            }
            
            if (_currentBossData.AbilityAffectionRounds <= 0)
            {
                _currentBossData.IsAbilityAffected = false;
            }
            _bossCore.SetupAffectionIcons();
            // _bossCore.SetBossDamageAffected(_currentBossData.DamageAffectionRounds > 0, _currentBossData.DamageAffectionRounds, _currentBossData.DamageAffectionValue);
            // _bossCore.SetBossAbilityAffected(_currentBossData.AbilityAffectionRounds > 0, _currentBossData.AbilityAffectionRounds, _currentBossData.AbilityAffectionValue);
            SetAttackCounter();
        }

        /// <summary>
        /// Set Attack Counter
        /// </summary>
        private void SetAttackCounter()
        {
            if (_currentBossData.AttackCounter == 0)
            {
                _currentBossData.AttackCounter = _currentBossData.AttackRatio;
                _bossAttackCounterText.text = _currentBossData.AttackRatio + " Shapes";
                _bossCore.DoNormalBossAttack();
            }
            else
            {
                _bossAttackCounterText.text = _currentBossData.AttackCounter + " Shapes";
            }
        }
        
        
        /// <summary>
        /// Unregister events
        /// </summary>
        private void OnDisable()
        {
            // _gameChannel.OnBossDied -= OnBossDied;
            _gameChannel.OnShapePlacementIsValid -= OnShapePlacementIsValid;
            _gameChannel.OnBossGetsDamageAffected -= OnBossGetsDamageAffected;
            _gameChannel.OnBossGetsAbilityAffected -= OnBossGetsAbilityAffected;
            _gameChannel.OnBasicDamageDone -= OnBasicDamageDone;
            _gameChannel.OnAbilityDamageDone -= OnAbilityDamageDone;
            _gameChannel.OnRestart -= OnRestart;
        }
        
    }
}
