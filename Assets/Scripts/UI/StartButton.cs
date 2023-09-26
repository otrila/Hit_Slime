using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private RuleType _ruleType;

    private IRulesFactory _rulesFactory;
    private EnemyInvoker _invoker;
    private Mediator _mediator;
    private IGrid _grid;

    [Inject]
    public void Init(IRulesFactory rulesFactory, EnemyInvoker invoker, Mediator mediator, IGrid grid)
    {
        _rulesFactory = rulesFactory;
        _invoker = invoker;
        _mediator = mediator;
        _grid = grid;
    }

    private void Start() => _button.onClick.AddListener(() => Click());

    private void OnDestroy() => _button.onClick.RemoveAllListeners();

    private void Click()
    {
        _rulesFactory.Create(_ruleType, _invoker, _mediator);
        _mediator.ShowStartWindow(false);
        _grid.CreateGrid();
    }
}
