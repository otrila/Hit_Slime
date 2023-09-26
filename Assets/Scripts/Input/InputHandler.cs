using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour
{
    IInputService _input;

    [Inject]
    public void Init(IInputService input)
    {
        _input = input;
        _input.OnEnemyClick += OnEnemyClick;
    }

    private void OnDestroy()
    {
        if (_input != null)
            _input.OnEnemyClick -= OnEnemyClick;
    }

    private void OnEnemyClick(Enemy enemy) => enemy.Hit();

}
