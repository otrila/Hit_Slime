using UnityEngine;

[CreateAssetMenu(fileName = "CellSpawnFactory", menuName = "Factory/CellSpawnFactory")]
public class CellFactory : ScriptableObject, ICellFactory
{
    [SerializeField] private CellSpawn _cellPrefab;

    public CellSpawn Create(Transform parent, IEnemyFactory enemyFactory)
    {
        CellSpawn cell;
        cell = Instantiate(_cellPrefab, parent);
        cell.Init(enemyFactory);
        return cell;
    }
}
