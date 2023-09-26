using UnityEngine;

public enum RuleType { Time, Health }

[CreateAssetMenu(fileName = "RulesFactory", menuName = "Factory/RulesFactory")]
public class RulesFactory : ScriptableObject, IRulesFactory
{
    [Header("Rules")]
    [SerializeField] private GameRule _timeRule;
    [SerializeField] private GameRule _healthRule;

    public void Create(RuleType ruleType, EnemyInvoker invoker, Mediator mediator)
    {
        GameRule rule;

        switch (ruleType)
        {
            case RuleType.Time:
                rule = Instantiate(_timeRule);
                rule.Init(invoker, mediator);
                break;

            case RuleType.Health:
                rule = Instantiate(_healthRule);
                rule.Init(invoker, mediator);
                break;
        }
    }
}
