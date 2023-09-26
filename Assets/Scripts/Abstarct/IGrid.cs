using System;
using System.Collections.Generic;

public interface IGrid
{
    event Action GridCreated;
    IReadOnlyList<ISpawner> CellsList { get; }

    void CreateGrid();

    void CreateGrid(GridConfig gridConfig, ICellFactory cellFactory);
}
