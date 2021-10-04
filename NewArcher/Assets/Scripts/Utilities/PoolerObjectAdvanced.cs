using System.Collections.Generic;
using UnityEngine;

public class PoolerObjectAdvanced : MonoBehaviour
{
    private static Dictionary<string, Queue<GameObject>> _objectPool;

    private void Awake()
    {
        _objectPool = new Dictionary<string, Queue<GameObject>>();
    }

    public static GameObject GetObject(GameObject gameObject)
    {
        if (_objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
            {
                return CreateNewObject(gameObject);
            }
            else
            {
                GameObject _object = objectList.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
        {
            return CreateNewObject(gameObject);
        }
    }
    public static void ReturnGameObject(GameObject gameObject)
    {
        if (_objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            _objectPool.Add(gameObject.name, newObjectQueue);
        }

        gameObject.SetActive(false);
    }

    public static void PoolerStartSize(GameObject gameObject, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject newObject = CreateNewObject(gameObject);
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(newObject);
            newObject.SetActive(false);
        }
    }

    private static GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newObject = Instantiate(gameObject);
        newObject.name = gameObject.name;
        return newObject;
    }
}
