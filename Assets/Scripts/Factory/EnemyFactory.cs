using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factory/EnemyFactory")]
public class EnemyFactory : ScriptableObject, IEnemyFactory
{
    [Header("Enemy configs")]
    [SerializeField] private EnemyConfig _enemyLite;
    [SerializeField] private EnemyConfig _enemyMedium;
    [SerializeField] private EnemyConfig _enemyHard;

    [Header("Prefab")]
    [SerializeField] private Enemy _enemyPrefab;

    public Enemy Create(EnemyType enemyType, Transform parent)
    {
        Enemy enemy = Instantiate(_enemyPrefab, parent);

        switch (enemyType)
        {
            case EnemyType.Lite:
                enemy.Init(_enemyLite);
                break;
            case EnemyType.Medium:
                enemy.Init(_enemyMedium);
                break;
            case EnemyType.Hard:
                enemy.Init(_enemyHard);
                break;
        }
        return enemy;
    }
}