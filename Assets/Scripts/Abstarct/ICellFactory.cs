using UnityEngine;

public interface ICellFactory
{
    CellSpawn Create(Transform parent, IEnemyFactory enemyFactory);
}
