using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemShootProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _viewPoint;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private PoolerObject poolerObject;

    private CharacterStats _characterStats;
    private float _attackCountdown = 0f;

    private void Awake()
    {
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        _attackCountdown -= Time.deltaTime;
    }

    public void OnShoot()
    {
        if (_attackCountdown <= 0)
        {
            GameObject newArrow = PoolerObjectAdvanced.GetObject(_projectilePrefab);
            //GameObject newArrow = poolerObject.GetObject();
            newArrow.transform.forward = _viewPoint.transform.forward;
            newArrow.transform.position = _shootPoint.transform.position;

            _attackCountdown = _characterStats.GetStats().countdownAttack;

            StartCoroutine(CountToShoot());

            IEnumerator CountToShoot()
            {
                yield return new WaitForSeconds(0.1f);
                newArrow.GetComponent<ProjectileBehaviour>().AddForce();
            }
        }
    }
}
