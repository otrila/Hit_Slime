using System;
using UnityEngine;
using Zenject;

public class MouseInput : IInputService, ITickable
{
    public event Action<Enemy> OnEnemyClick;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;
            TryGetEnemy(clickedObject);
        }
    }

    private void TryGetEnemy(GameObject gameObject)
    {
        Enemy enemy = gameObject.GetComponentInParent<Enemy>();

        if (!enemy)
            return;

        OnEnemyClick?.Invoke(enemy);
    }
}
