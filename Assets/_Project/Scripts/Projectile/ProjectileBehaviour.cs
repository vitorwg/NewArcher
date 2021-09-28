using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _centerOfMass;

    private PoolerObject _arrowPooler;
    private Rigidbody _arrowRb;
    private float _forcePower = 50.0f;

    private void Awake()
    {
        _arrowRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _arrowPooler = GetComponentInParent<PoolerObject>();
    }

    private void OnEnable()
    {
        _arrowRb.velocity = Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Poller
        // MecanicsSystem
    }

    public void AddForce()
    {
        _arrowRb.AddForce(transform.forward * _forcePower, ForceMode.Impulse);
    }


    private void OnDisable()
    {
        _arrowRb.velocity = Vector3.zero;
    }
}
