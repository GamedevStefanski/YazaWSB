using UnityEngine;

public class MobStats : MonoBehaviour
{
    [Header("Customazible Parameters")]
    public float maxHealth;
    public float maxArmor;
    public float damage;
    public float CooldownBetweenAttacks;
    private bool isDead = false;
    [SerializeField] private bool isTurret;

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
        CooldownBetweenAttacks += Random.Range(-0.15f, 0.15f);
    }
    private void Update()
    {
        // Sprawdzamy czy zdrowie spad³o i czy jeszcze nie uruchomiliœmy logiki œmierci
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;

            // 1. Wy³¹czamy fizykê, ¿eby Raycasty innych jednostek nas ignorowa³y
            GetComponent<Collider2D>().enabled = false;

            // 2. Wy³¹czamy skrypty zachowania, ¿eby trup nie chodzi³ ani nie walczy³
            GetComponent<MobMovement>().enabled = false;
            GetComponent<MobCombat>().enabled = false;

            Death();
        }
    }
    void Death()
    {
        if (this.gameObject.tag == "Enemy" && !isTurret)
        {
            spawnerScript.waves[spawnerScript.currentWave].enemiesLeft--;
            
        }
        gameplayManager.ink += inkGranted;
        Destroy(gameObject, 2f);
    }
}
