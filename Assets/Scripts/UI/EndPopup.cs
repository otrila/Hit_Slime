using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPopup : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _resultText;

    public void SetResultText(string text) => _resultText.text = text;

    private void OnEnable() => _restartButton.onClick.AddListener(() => Click());

    private void OnDisable() => _restartButton.onClick.RemoveAllListeners();

    private void Click() => SceneManager.LoadScene(0);

}
