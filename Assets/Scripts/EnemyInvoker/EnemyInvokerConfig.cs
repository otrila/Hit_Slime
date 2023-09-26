using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("EnemyInvokerConfig"), menuName = ("Configs/EnemyInvokerConfig"))]
public class EnemyInvokerConfig : ScriptableObject
{
    [SerializeField] private float _spawnTimeStep;
    [SerializeField] private List<EnemyType> _spawnEnemyTypes;

    public float SpawnTimeStep => _spawnTimeStep;
    public List<EnemyType> SpawnEnemyTypes => _spawnEnemyTypes;

    private void OnValidate()
    {
        if (_spawnEnemyTypes.Count == 0)
            throw new Exception($"Empty <Spawn Enemy Types>");
    }
}
