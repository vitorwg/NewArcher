using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _target;
    private PlayerStats _targetStats;
    private Animator _animator;
    private Rigidbody _enemyRb;

    private float _distance;
    private float _lerpRotation = 5f;

    private void Awake()
    {
        _target = GameManager.instance.GetTarget();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemyRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _targetStats = _target.GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        _animator.SetBool("IsMoving", true);
    }

    private void OnDisable()
    {
        _animator.SetBool("IsMoving", false);
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.localPosition, _target.transform.localPosition);

        if (_distance <= (_agent.stoppingDistance + 0.3f))
        {
            _animator.SetTrigger("Attack");
            FaceTarget();
        }
        else
        {
            _agent.SetDestination(_target.transform.localPosition);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.transform.localPosition - transform.localPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
        transform.rotation = Quaternion.Slerp(lookRotation, transform.rotation, Time.deltaTime * _lerpRotation);
    }

}
