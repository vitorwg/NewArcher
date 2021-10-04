using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _viewPoint;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private PoolerObject poolerObject;

    private InputReader _inputReader;
    private PlayerStats _playerStats;
    private float _attackCountdown = 0f;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        _inputReader.OnShootEvent += OnShoot;   
    }

    private void Update()
    {
        _attackCountdown -= Time.deltaTime;
    }

    private void OnShoot(bool shoot)
    {
        if (shoot & _attackCountdown <= 0)
        {
            GameObject newArrow = PoolerObjectAdvanced.GetObject(_arrowPrefab);
            //GameObject newArrow = poolerObject.GetObject();
            newArrow.transform.forward = _viewPoint.transform.forward;
            newArrow.transform.position = _shootPoint.transform.position;

            _attackCountdown = _playerStats.GetStats().countdownAttack;

            StartCoroutine(CountToShoot());

            IEnumerator CountToShoot()
            {
                yield return new WaitForSeconds(0.1f);
                newArrow.GetComponent<ProjectileBehaviour>().AddForce();
            }
        }
    }

    private void OnDisable()
    {
        _inputReader.OnShootEvent -= OnShoot;
    }
}
