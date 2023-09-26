using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Grid : MonoBehaviour, IGrid
{
    private GridConfig _gridConfig;

    private ICellFactory _cellFactory;
    private IEnemyFactory _enemyFactory;

    private List<ISpawner> _cellsList;

    public event Action GridCreated;

    public int GridSize => _gridConfig.GridSize;
    public IReadOnlyList<ISpawner> CellsList => _cellsList;

    [Inject]
    private void Init(ICellFactory cellFactory, IEnemyFactory enemyFactory)
    {
        _cellFactory = cellFactory;
        _enemyFactory = enemyFactory;

        _gridConfig = Resources.Load<GridConfig>("Configs/GridConfig");
    }

    private void Start()
    {
        _cellsList = new List<ISpawner>();
    }

    public void CreateGrid()
    {
        CreateGrid(_gridConfig, _cellFactory);
    }

    public void CreateGrid(GridConfig gridConfig, ICellFactory cellFactory)
    {
        int size = gridConfig.GridSize;
        float spacing = gridConfig.Spacing;

        float xOffset = (size - 1) * spacing / 2.0f;
        float zOffset = (size - 1) * spacing / 2.0f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Vector3 newPosition;

                if (gridConfig.Orientation == GridOrientation.Horisontal)
                    newPosition = new Vector3(x * spacing - xOffset, 0, y * spacing - zOffset);
                else
                    newPosition = new Vector3(x * spacing - xOffset, y * spacing - zOffset, 0);

                CellSpawn cell = cellFactory.Create(this.transform, _enemyFactory);
                cell.transform.position = newPosition;
                _cellsList.Add(cell);
            }
        }

        GridCreated?.Invoke();
    }
}