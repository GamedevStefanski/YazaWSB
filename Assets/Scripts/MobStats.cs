using UnityEngine;

public class MobStats : MonoBehaviour
{
    [Header("Customazible Parameters")]
    public float maxHealth;
    public float maxArmor;
    public float damage;
    public float CooldownBetweenAttacks;

    [Header("Change cost for friendly, granted for enemy mobs")]
    public int InkCost;
    public int inkGranted;

    [Header("Statistics to read only - leave values at 0")]
    public float currentHealth;
    public float currentArmor;

    private WaveSpawner spawnerScript;
    private GameplayManager gameplayManager;

    private void Awake()
    {
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        spawnerScript = GetComponentInParent<WaveSpawner>();
        currentHealth = maxHealth;
        currentArmor = maxArmor;
    }
    private void Update()
    {
        if (currentHealth <= 0)
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
