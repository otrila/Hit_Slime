using UnityEngine;

public class HealthGameRule : GameRule
{
    [Header("Health:")]
    [SerializeField] private float _playerHealth = 80;

    [Tooltip("Enemy Life Time Multiplier (in seconds) time*damageMultiplayer")]
    [SerializeField] private float _damageMultiplayer = 1;

    protected override void Start()
    {
        base.Start();
        _mediator.SetRule($"Health:{_playerHealth}");
    }

    private void HealthEnd() => CheckScore();

    private void RemoveHealth(float health)
    {
        if (!_isPlaying)
            return;

        _playerHealth -= health;

        _mediator.SetRule($"Health:{_playerHealth}");

        if (_playerHealth <= 0)
            HealthEnd();
    }

    protected override void SubscribeOnEnemy(Enemy enemy)
    {
        base.SubscribeOnEnemy(enemy);
        enemy.Escaped += Enemy_OnTimerEnd;
    }

    protected override void UnsubscribeOnEnemy(Enemy enemy)
    {
        base.UnsubscribeOnEnemy(enemy);
        enemy.Escaped -= Enemy_OnTimerEnd;
    }

    private void Enemy_OnTimerEnd(Enemy enemy)
    {
        float damage = enemy.TimeLive * _damageMultiplayer;
        RemoveHealth(damage);

        UnsubscribeOnEnemy(enemy);
    }
}