using System;

public interface ISpawner
{
    event Action<ISpawner> Cleared;

    Enemy Spawn(EnemyType enemyType);
}
