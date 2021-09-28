using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private GameObject _playerTarget;
    [SerializeField] private SpawnEnemyManager _spawnEnemyManager;
    [SerializeField] private TextMeshProUGUI _enemyCount;
    [SerializeField] private TextMeshProUGUI _waveCount;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ScoreManager found!", gameObject);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        EnemyManager();
    }

    public GameObject GetTarget()
    {
        return _playerTarget;
    }

    public void EnemyManager()
    {
        string enemyCountString = _spawnEnemyManager.GetEnemyCount().ToString();
        string waveCountString = _spawnEnemyManager.GetWaveCount().ToString();

        _enemyCount.text = "Enemy Remaing: " + enemyCountString;
        _waveCount.text = "Current Wave: " + waveCountString;
    }
}
