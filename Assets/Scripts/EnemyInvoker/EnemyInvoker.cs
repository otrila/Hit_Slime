using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyInvoker : MonoBehaviour
{
    [SerializeField] private EnemyInvokerConfig _enemyInvokerConfig;
    private float _currentTimeTimer;
    private bool _invokeEnemy = false;
    private bool _startTimer = false;

    private IGrid _grid;

    private List<ISpawner> _emptySpawners;
    private List<Enemy> _enemies;

    public event Action<Enemy> EnemyInvoked;

    [Inject]
    private void Init(IGrid grid)
    {
        _grid = grid;
        _grid.GridCreated += OnGridCreated;
    }

    private void Update()
    {
        if (!_invokeEnemy)
            return;

        if (HasEmptySpawners())
            Timer(InvokeRandomEnemy);
        else
            ResetTimer();
    }

    private void OnDestroy() => Unsubscribe();

    public void StopInvoke()
    {
        _invokeEnemy = false;
        ResetTimer();
    }

    public void ClearEnemies()
    {
        foreach (var enemy in _enemies)
            Destroy(enemy.gameObject);

        _enemies.Clear();
    }

    private void OnGridCreated()
    {
        _emptySpawners = new List<ISpawner>(_grid.CellsList);
        _enemies = new List<Enemy>();

        SubscriptionOnCells();

        _startTimer = true;
        _invokeEnemy = true;
    }

    private void SubscriptionOnCells()
    {
        foreach (var spawner in _grid.CellsList)
        {
            spawner.Cleared += AddEmptySpawner;
        }
    }

    private void Unsubscribe()
    {
        _grid.GridCreated -= OnGridCreated;

        foreach (var spawner in _grid.CellsList)
        {
            spawner.Cleared -= AddEmptySpawner;
        }
    }

    private ISpawner GetRandomCell()
    {
        int rand = Random.Range(0, _emptySpawners.Count);
        var cell = _emptySpawners[rand];
        _emptySpawners.Remove(cell);
        return cell;
    }

    private EnemyType GetRandomEnemyType()
    {
        int randType = Random.Range(0, _enemyInvokerConfig.SpawnEnemyTypes.Count);
        return _enemyInvokerConfig.SpawnEnemyTypes[randType];
    }

    private void InvokeRandomEnemy()
    {
        Enemy enemy = GetRandomCell().Spawn(GetRandomEnemyType());
        _enemies.Add(enemy);
        enemy.Destroyed += OnEnemyDestroyed;
        EnemyInvoked?.Invoke(enemy);
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        enemy.Destroyed -= OnEnemyDestroyed;
        _enemies.Remove(enemy);
    }

    private bool HasEmptySpawners() => _emptySpawners.Count > 0 ? true : false;

    private void AddEmptySpawner(ISpawner spawner) => _emptySpawners.Add(spawner);

    private void ResetTimer() => _currentTimeTimer = _enemyInvokerConfig.SpawnTimeStep;

    private void Timer(Action timerEndAction)
    {
        if (!_startTimer)
            return;

        _currentTimeTimer -= Time.deltaTime;

        if (_currentTimeTimer <= 0)
        {
            timerEndAction.Invoke();
            ResetTimer();
        }
    }
}
