using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour, IObserver
{
    [SerializeField] Enemy _enemyPrefab;

    int _enemiesAlive;

    void Start()
    {
        CreateNewWave();
    }

    void CreateNewWave()
    {
        _enemiesAlive = Random.Range(3, 11);

        for (int i = 0; i < _enemiesAlive; i++)
        {
            IObservable enemy = Instantiate(_enemyPrefab, transform.position + Vector3.right * i, Quaternion.identity);
            enemy.Subscribe(this);
        }
    }

    void DecreaseEnemiesAlive()
    {
        _enemiesAlive--;

        if (_enemiesAlive <= 0)
        {
            CreateNewWave();
        }
    }

    #region Observer

    public void Notify(string action)
    {
        if (action == "EnemyDead")
        {
            DecreaseEnemiesAlive();
        }
    }

    #endregion
}
