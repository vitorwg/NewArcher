using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{

    [SerializeField] private float _radius = 5.0f;
    [SerializeField] private Transform[] _spawnPoint;

    [Header("Pooler Settings")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab2;
    

    
    private int _waveNumber = 1;
    private int _enemyCount = 0;

    private void Start()
    {
        SpawnEnemyWave(_waveNumber);
    }

    private void OnEnable()
    {
        SpiderStats.OnEnableCount += CharacterOnEnableCount;
        SpiderStats.IsDead += CharacterIsDead;
    }

    private void CharacterIsDead()
    {
        _enemyCount--;
    }

    private void CharacterOnEnableCount()
    {
        _enemyCount++;
    }

    private void Update()
    {
        if (_enemyCount == 0)
        {
            SpawnEnemyWave(_waveNumber);
        }
    }

    private void OnDisable()
    {
        SpiderStats.OnEnableCount -= CharacterOnEnableCount;
        SpiderStats.IsDead -= CharacterIsDead;
    }

    public int GetEnemyCount()
    {
        return _enemyCount;
    }

    public int GetWaveCount()
    {
        return _waveNumber - 1;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject newObj = PoolerObjectAdvanced.GetObject(_enemyPrefab);
            newObj.transform.position = GenerateRandomSpawn();
            newObj.SetActive(true);
        }
        _waveNumber++;
    }

    private Vector3 GenerateRandomSpawn()
    {
        int i = Random.Range(0, _spawnPoint.Length);
        Vector2 randomPointCircle = Random.insideUnitCircle * _radius;
        Vector3 point3D = new Vector3(randomPointCircle.x, _spawnPoint[i].position.y, randomPointCircle.y);

        return _spawnPoint[i].position + point3D;
    }
}
