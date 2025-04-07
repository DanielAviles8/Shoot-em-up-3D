using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float _countdown;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] public static bool Win = false;
    [HideInInspector] public int EnemiesLeft;


    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool _readyToCountDown;

    private void Start()
    {
        _readyToCountDown = true;
        EnemiesLeft = waves[currentWaveIndex].enemies.Length;
        
    }
    private void Update()
    {
        
        Debug.Log(currentWaveIndex + "index");
        Debug.Log(waves.Length + "Oleadas");
        if (_readyToCountDown)
        {
            _countdown -= Time.deltaTime;
        }

        if (_countdown <= 0)
        {
            _readyToCountDown = false;
            _countdown = waves[currentWaveIndex].TimeToNextWave;
            StartCoroutine(SpawnWave());
        }

    }
    private IEnumerator SpawnWave()
    {
        
        if(currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], _spawnPoint.transform);
                enemy.waveSpawner = this;
                enemy.transform.SetParent(null);
                yield return new WaitForSeconds(waves[currentWaveIndex].TimeToNextEnemy);
            }
            _readyToCountDown = false;
        }
    }
    public void DieEnemy()
    {
        if(Enemy.death)
        {
            EnemiesLeft--;
            Enemy.death = false;
        }
    }
    public void CountDeathEnemies()
    {
        EnemiesLeft--;
        if (EnemiesLeft == 0)
        {
            currentWaveIndex++;

            if (currentWaveIndex >= waves.Length)
            {
                Win = true;
                GamePanel.SetActive(false);
                Debug.Log("Ganaste");
                return;
            }
            else
            {
                Debug.Log(waves[currentWaveIndex].enemies.Length);
                EnemiesLeft = waves[currentWaveIndex].enemies.Length;
                _readyToCountDown = true;
            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float TimeToNextEnemy;
    public float TimeToNextWave;

}
