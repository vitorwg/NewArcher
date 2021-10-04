using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerObject : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _objPrefab;

    [Header("Settings")] 
    [SerializeField] private int _poolStartSize = 5;

    [SerializeField] private Queue<GameObject> _objPool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _poolStartSize; i++)
        {
            GameObject newObj = Instantiate(_objPrefab);
            newObj.transform.parent = transform;
            _objPool.Enqueue(newObj);
            newObj.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        if (_objPool.Count > 0)
        {
            GameObject newObj = _objPool.Dequeue();
            newObj.SetActive(true);
            return newObj;
        }
        else
        {
            GameObject newObj = Instantiate(_objPrefab);
            newObj.transform.parent = transform;
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        _objPool.Enqueue(obj);
        obj.SetActive(false);
    }
}