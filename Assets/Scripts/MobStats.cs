using UnityEngine;

public class MobStats : MonoBehaviour
{
    public int health;
    public int armor;
    public int damage;
    public float CooldownBetweenAttacks;

    private WaveSpawner spawnerScript;

    private void Start()
    {
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
            spawnerScript.waves[spawnerScript.currentWave].enemiesLeft--;
    }
}
