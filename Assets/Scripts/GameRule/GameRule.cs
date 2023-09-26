using UnityEngine;

public abstract class GameRule : MonoBehaviour
{
    [Header("Score:")]
    [SerializeField] protected int _neededScore = 15;

    protected int _curentScore;
    protected bool _isPlaying;

    protected EnemyInvoker _enemyInvoker;
    protected Mediator _mediator;

    public virtual void Init(EnemyInvoker invoker, Mediator mediator)
    {
        _enemyInvoker = invoker;
        _mediator = mediator;

        SubscribeOnEnemyInvoker();
    }

    private void OnDestroy() => UnsubscribeOnEnemyInvoker();

    protected virtual void Start()
    {
        _mediator.ShowHUD(true);
        _isPlaying = true;

    }

    protected virtual void AddScore(int addScore)
    {
        _curentScore += addScore;
        _mediator.SetScore(_curentScore);
    }

    protected virtual void CheckScore()
    {
        if (_isPlaying == false)
            return;

        if (_curentScore >= _neededScore)
            Win();
        else
            Lose();

        _enemyInvoker.StopInvoke();
        _enemyInvoker.ClearEnemies();

        _isPlaying = false;
    }
    protected virtual void Win() => _mediator.ShowWinWindow();

    protected virtual void Lose() => _mediator.ShowLosetWindow();

    protected virtual void SubscribeOnEnemy(Enemy enemy) => enemy.Died += OnEnemyDie;

    protected virtual void UnsubscribeOnEnemy(Enemy enemy) => enemy.Died -= OnEnemyDie;

    private void OnEnemyInvoked(Enemy enemy) => SubscribeOnEnemy(enemy);

    private void OnEnemyDie(Enemy enemy)
    {
        AddScore(enemy.MaxHelath);
        UnsubscribeOnEnemy(enemy);
    }

    private void SubscribeOnEnemyInvoker() => _enemyInvoker.EnemyInvoked += OnEnemyInvoked;

    private void UnsubscribeOnEnemyInvoker() => _enemyInvoker.EnemyInvoked -= OnEnemyInvoked;
}