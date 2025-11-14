using UnityEngine;
using System.Collections;
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float roundStart;
    [SerializeField] private GameObject spawner;

    public Wave[] waves;
    public int currentWave = 0;
    [SerializeField] private bool readyToSpawn;
    private void Start()
    {

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
        readyToSpawn = true;
    }
    void Update()
    {
        if(waves.Length == currentWave)
        {
            // End of round
            return;
        }
        if (readyToSpawn)
        {
            roundStart -= Time.fixedDeltaTime;
        }

        if(roundStart <= 0 )
        {
            readyToSpawn = false;
            roundStart = waves[currentWave].timeBetweenWaves;
            StartCoroutine(SpawnWave());

        }

        if (waves[currentWave].enemiesLeft == 0)
        {
            readyToSpawn = true;
            currentWave++;
            
        }
    }

    private IEnumerator SpawnWave()
    {
        for( int i = 0; i < waves[currentWave].enemies.Length; i++)
        {
            GameObject enemy = Instantiate(waves[currentWave].enemies[i], spawner.transform);
            enemy.transform.SetParent(spawner.transform);

            yield return new WaitForSeconds(waves[currentWave].timeBetweenEnemies);
        }
    }
    [System.Serializable]
    public class Wave
    {
        public float timeBetweenEnemies;
        public float timeBetweenWaves;
        public GameObject[] enemies;

        [HideInInspector] public int enemiesLeft;
        
    }
}
