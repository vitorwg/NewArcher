using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour, ITakeDamage
{
    [SerializeField] private Stats _stats;

    public static event UnityAction IsDead;
    public static event UnityAction OnEnableCount;

    protected Animator _animator;
    private NavMeshAgent _agent;
    private float _currentHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _currentHealth = _stats.health;

        if (gameObject.activeSelf)
        {
            OnEnableCount?.Invoke();
        }
    }

    public virtual void TakeDamage(int enemyDamage)
    {
        _currentHealth -= enemyDamage;

        Debug.Log(transform.name + " takes " + enemyDamage + " damage.");

        if (_currentHealth <= 0)
        {
            OnDie();
        }
    }

    public Stats GetStats()
    {
        return _stats;
    }

    public virtual void OnDie()
    {
        _agent.speed = 0;
        _animator.SetTrigger("IsDead");
        IsDead?.Invoke();
    }
}
