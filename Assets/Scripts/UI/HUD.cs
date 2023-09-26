using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ruleText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Show() => this.gameObject.SetActive(true);

    public void Hide() => this.gameObject.SetActive(false);

    public void SetRuleText(string text) => _ruleText.text = text;

    public void SetScoreText(int score) => _scoreText.text = $"Score {score}";

}
