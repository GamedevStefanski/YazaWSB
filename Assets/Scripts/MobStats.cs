using UnityEngine;

public class MobStats : MonoBehaviour
{
    public int health;
    public int armor;
    public int damage;
    public float CooldownBetweenAttacks;
    public int inkGranted;

    private WaveSpawner spawnerScript;
    private GameplayManager gameplayManager;

    private void Awake()
    {
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        spawnerScript = GetComponentInParent<WaveSpawner>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        Destroy(gameObject);
        if(this.gameObject.tag == "Enemy")
        {
            spawnerScript.waves[spawnerScript.currentWave].enemiesLeft--;
            gameplayManager.ink += inkGranted;
        }
            
    }
}
