using System.Collections;
using Attribute;
using Channel;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class ComboManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameChannel _gameChannel;
        [Expandable][SerializeField] private PlayerData _playerData;
        
        [Header("Combo UI")]
        [SerializeField] private CanvasGroup _comboUI;
        [SerializeField] private TextMeshProUGUI _comboText;

        private void OnEnable()
        {
            _gameChannel.OnComboChanged += OnComboChanged;
        }
        
        private void Start()
        {
            _playerData.CurrentCombo = 0;
            _comboText.text = "";
            _comboUI.alpha = 0;
            OnComboChanged(0);
        }
        
        private void OnComboChanged(int combo)
        {            
            _playerData.CurrentCombo = combo;
            if (combo <= 1)
            {
                _comboText.text = "";
                _playerData.CurrentCombo = 1;
            }
            else
            {
                StopAllCoroutines();
                _comboText.text = "x" + combo;
                StartCoroutine(ComboCoroutine());
            }

            
        }

        private IEnumerator ComboCoroutine()
        {
            _comboUI.alpha = 0f;
            _comboUI.transform.localScale = Vector3.zero;
            float elapsedTime = 0f;
            while (elapsedTime < 0.5f)
            {
                elapsedTime += Time.deltaTime;
                _comboUI.alpha = Mathf.Lerp(_comboUI.alpha, 1f, elapsedTime/0.5f);
                _comboUI.transform.localScale = Vector3.Lerp(_comboUI.transform.localScale, Vector3.one, elapsedTime/0.5f);
                yield return null;
            }

            _comboUI.alpha = 1f;
            _comboUI.transform.localScale = Vector3.one;

            yield return new WaitForSeconds(1f);
            elapsedTime = 0f;
            while (elapsedTime < 0.2f)
            {
                elapsedTime += Time.deltaTime;
                _comboUI.alpha = Mathf.Lerp(_comboUI.alpha, 0f, elapsedTime/0.2f);
                _comboUI.transform.localScale = Vector3.Lerp(_comboUI.transform.localScale, Vector3.zero, elapsedTime/0.5f);
                yield return null;
            }

            _comboUI.alpha = 0f;
            _comboUI.transform.localScale = Vector3.zero;
        }
        
        private void OnDisable()
        {
            _gameChannel.OnComboChanged -= OnComboChanged;
        }
    }
}
