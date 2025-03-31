using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _countdown;
    [SerializeField] private GameObject _spawnPoint;

    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool _readyToCountDown;

    private void Start()
    {
        _readyToCountDown = true;
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].EnemiesLeft = waves[i].enemies.Length;
        }
    }
    private void Update()
    {
        Debug.Log("Enemigos restantes" +  waves[currentWaveIndex].EnemiesLeft);
        if (currentWaveIndex >= waves.Length - 1 && waves[currentWaveIndex].EnemiesLeft == 0)
        {
            Debug.Log("Ganaste");
            return;
        }
        if(_readyToCountDown)
        {
            _countdown -= Time.deltaTime;
        }

        if(_countdown <= 0)
        {
            _readyToCountDown = false;
            _countdown = waves[currentWaveIndex].TimeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].EnemiesLeft == 0)
        {
            _readyToCountDown = true;
            currentWaveIndex++;
        }
        DieEnemy();
    }
    private IEnumerator SpawnWave()
    {
        if(currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], _spawnPoint.transform);
                enemy.transform.SetParent(null);
                yield return new WaitForSeconds(waves[currentWaveIndex].TimeToNextEnemy);
            }
        }
    }
    public void DieEnemy()
    {
        if(Enemy.death)
        {
            Debug.Log("EnemyDead");
            waves[currentWaveIndex].EnemiesLeft--;
            Enemy.death = false;
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float TimeToNextEnemy;
    public float TimeToNextWave;

    [HideInInspector] public int EnemiesLeft;
}
