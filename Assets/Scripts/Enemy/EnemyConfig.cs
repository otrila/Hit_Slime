using UnityEngine;

[CreateAssetMenu(fileName = ("EnemyConfig"), menuName = ("Configs/EnemyConfig"))]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private float _timeLive;
    [SerializeField] private Slime _model;

    [SerializeField] private GameObject _dieParticle;
    [SerializeField] private GameObject _escapeParticle;

    public int Health => _health;
    public float TimeLive => _timeLive;
    public Slime Model => _model;

    public GameObject DieParticle => _dieParticle;
    public GameObject EscapeParticle => _escapeParticle;
}
