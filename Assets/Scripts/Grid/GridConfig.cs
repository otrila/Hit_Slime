using UnityEngine;

[CreateAssetMenu(fileName = ("GridConfig"), menuName = ("Configs/GridConfig"))]
public class GridConfig : ScriptableObject
{
    [field: SerializeField] public int GridSize { get; private set; }
    [field: SerializeField] public float Spacing { get; private set; }
    [field: SerializeField] public GridOrientation Orientation { get; private set; }
}