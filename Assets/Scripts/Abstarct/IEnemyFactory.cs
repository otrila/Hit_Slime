using UnityEngine;

public interface IEnemyFactory
{
    Enemy Create(EnemyType enemyType, Transform parent);
}
