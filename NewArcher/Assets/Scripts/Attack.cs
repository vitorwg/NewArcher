using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterStats damageable))
        {
            damageable.TakeDamage(_stats.damage);
        }
    }
}