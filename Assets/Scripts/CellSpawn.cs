using System;
using UnityEngine;

public class CellSpawn : MonoBehaviour, ISpawner
{
    [SerializeField] private IEnemyFactory _enemyFactory;
    [SerializeField] private Transform _spawnPosition;
    private bool _isEmpty = true;
    private Enemy _enemy;

    public event Action<ISpawner> Cleared;

    public void Init(IEnemyFactory factory)
    {
        _enemyFactory = factory;
    }

    public Enemy Spawn(EnemyType enemyType)
    {
        if (!_isEmpty)
            throw new Exception($"{this} Spawn not empty");

        _enemy = _enemyFactory.Create(enemyType, _spawnPosition);
        _isEmpty = false;
        _enemy.Destroyed += OnEnemyDestroyed;

        return _enemy;
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        enemy.Destroyed -= OnEnemyDestroyed;
        ClearUp();
    }

    private void ClearUp()
    {
        _enemy = null;
        _isEmpty = true;

        Cleared?.Invoke(this);
    }



}
