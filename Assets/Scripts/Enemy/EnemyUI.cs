using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Enemy _enemy;

    private bool _isUpdate = false;

    private void Awake() => Subscribe();

    private void OnDestroy() => Unsubscribe();

    private void Update()
    {
        if (_isUpdate)
            UpdateTime();
    }

    private void Subscribe()
    {
        _enemy.Created += OnCreateEnemy;
        _enemy.Hited += OnHitEnemy;
    }

    private void Unsubscribe()
    {
        _enemy.Created -= OnCreateEnemy;
        _enemy.Hited -= OnHitEnemy;
    }

    private void OnCreateEnemy()
    {
        _healthSlider.maxValue = _enemy.MaxHelath;
        _healthSlider.value = _enemy.MaxHelath;
        _isUpdate = true;
    }

    private void OnHitEnemy() => UpdateHelth();

    public void UpdateHelth() => _healthSlider.value = _enemy.Helath;

    public void UpdateTime() => _timeText.text = _enemy.CurrentTimeLive.ToString("0");
}
