using UnityEngine;

public class Mediator : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    [SerializeField] private GameObject _startWindow;
    [SerializeField] private EndPopup _endWindow;

    public void ShowHUD(bool isActive)
    {
        if (isActive)
            _hud.Show();
        else
            _hud.Hide();
    }

    public void SetScore(int score) => _hud.SetScoreText(score);

    public void SetRule(string text) => _hud.SetRuleText(text);

    public void ShowStartWindow(bool isActive) => _startWindow.SetActive(isActive);

    public void ShowWinWindow()
    {
        _endWindow.gameObject.SetActive(true);
        _endWindow.SetResultText("You Win");
    }

    public void ShowLosetWindow()
    {
        _endWindow.gameObject.SetActive(true);
        _endWindow.SetResultText("You Lose");
    }


}
