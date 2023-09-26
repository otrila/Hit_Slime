using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyConfig _enemyConfig;
    private Slime _slime;

    private int _health;
    private float _currentTimeLive;
    private bool _startTimer = false;

    public event Action Created;
    public event Action Hited;
    public event Action<Enemy> Died;
    public event Action<Enemy> Escaped;
    public event Action<Enemy> Destroyed;

    public int MaxHelath => _enemyConfig.Health;
    public int Helath => _health;
    public float TimeLive => _enemyConfig.TimeLive;
    public float CurrentTimeLive => _currentTimeLive;

    public void Init(EnemyConfig enemyConfig)
    {
        _enemyConfig = enemyConfig;

        _slime = Instantiate(enemyConfig.Model, this.transform);

        _health = MaxHelath;
        _currentTimeLive = TimeLive;
        _startTimer = true;

        Created?.Invoke();
    }

    private void Update() => Timer(Escape);

    public void Hit()
    {
        _slime.ChangeState(SlimeAnimationState.Damage);
        _health--;

        Hited?.Invoke();

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke(this);
        Instantiate(_enemyConfig.DieParticle, transform.position, Quaternion.identity);
        Destroy();
    }

    private void Escape()
    {
        Escaped?.Invoke(this);
        Instantiate(_enemyConfig.EscapeParticle, transform.position, Quaternion.identity);
        Destroy();
    }

    private void Destroy()
    {
        Destroyed?.Invoke(this);
        Destroy(this.gameObject);
    }

    private void Timer(Action timerEndAction)
    {
        if (!_startTimer)
            return;

        _currentTimeLive -= Time.deltaTime;

        if (_currentTimeLive <= 0)
        {
            timerEndAction.Invoke();
        }
    }
}
