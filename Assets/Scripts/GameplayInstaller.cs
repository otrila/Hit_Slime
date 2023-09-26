using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private Grid _grid;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private CellFactory _cellFactory;
    [SerializeField] private RulesFactory _ruleFactory;
    [SerializeField] private EnemyInvoker _enemyInvoker;
    [SerializeField] private Mediator _mediator;

    public override void InstallBindings()
    {
        BindCellFactory();
        BindEnemyFactory();
        BindRulesFactory();
        BindGrid();
        BindInput();
        BindEnemyInvoker();
        BindMediator();
    }

    private void BindMediator()
    {
        Container.Bind<Mediator>()
                 .ToSelf()
                 .FromInstance(_mediator)
                 .AsSingle();
    }

    private void BindEnemyInvoker()
    {
        Container.Bind<EnemyInvoker>()
                 .ToSelf()
                 .FromInstance(_enemyInvoker)
                 .AsSingle();
    }

    private void BindInput()
    {
        Container.Bind(typeof(IInputService), typeof(ITickable))
                 .To<MouseInput>()
                 .AsSingle()
                 .NonLazy();
    }

    private void BindEnemyFactory()
    {
        Container.Bind<IEnemyFactory>()
                 .To<EnemyFactory>()
                 .FromInstance(_enemyFactory)
                 .AsSingle();
    }

    private void BindRulesFactory()
    {
        Container.Bind<IRulesFactory>()
                 .To<RulesFactory>()
                 .FromInstance(_ruleFactory)
                 .AsSingle();
    }

    private void BindCellFactory()
    {
        Container.Bind<ICellFactory>()
                 .To<CellFactory>()
                 .FromInstance(_cellFactory)
                 .AsSingle();
    }

    private void BindGrid()
    {
        Container.Bind<IGrid>()
                 .To<Grid>()
                 .FromInstance(_grid)
                 .AsSingle();
    }
}