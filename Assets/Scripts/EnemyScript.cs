using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    private WaveSpawner spawnerScript;
    private string ownTag;
    private void Start()
    {
        ownTag = gameObject.tag;
        spawnerScript = GetComponentInParent<WaveSpawner>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            Death();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != ownTag)
        {
            health -= 1;
        }
    }
    void Death()
    {
        Destroy(gameObject);
        spawnerScript.waves[spawnerScript.currentWave].enemiesLeft--;
    }
   
}
