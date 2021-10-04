using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderStats : CharacterStats
{
    public override void TakeDamage(int enemyDamage)
    {
        base.TakeDamage(enemyDamage);
        _animator.SetTrigger("TakeDamage");
    }

    public override void OnDie()
    {
        base.OnDie();
        StartCoroutine(ReturnObject());
    }

    IEnumerator ReturnObject()
    {
        yield return new WaitForSeconds(3f);
        PoolerObjectAdvanced.ReturnGameObject(gameObject);
    }
}
