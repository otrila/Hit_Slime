using System;

public interface IInputService
{
    event Action<Enemy> OnEnemyClick;
}
