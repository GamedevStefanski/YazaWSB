using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public SpellType spellType;
    public float value;
    [Header("Cooldown")]
    public float cooldown = 5f;

    public void Cast()
    {
        switch (spellType)
        {
            case SpellType.Damage:
                CastDamageToAllEnemies();
                break;

            case SpellType.Heal:
                CastHealToAllPlayers();
                break;

            case SpellType.Armor:
                CastArmorToAllPlayers();
                break;
        }
    }

    private void CastDamageToAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            MobStats stats = enemy.GetComponent<MobStats>();

            if (stats != null)
            {
                DealDamage(stats, value);
                UpdateBars(stats);
            }
        }
    }

    private void CastHealToAllPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Friendly");

        foreach (GameObject player in players)
        {
            MobStats stats = player.GetComponent<MobStats>();

            if (stats != null)
            {
                stats.currentHealth += value;
                stats.currentHealth = Mathf.Min(stats.currentHealth, stats.maxHealth);

                UpdateBars(stats);
            }
        }
    }

    private void CastArmorToAllPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Friendly");

        foreach (GameObject player in players)
        {
            MobStats stats = player.GetComponent<MobStats>();

            if (stats != null)
            {
                stats.currentArmor += value;
                stats.currentArmor = Mathf.Min(stats.currentArmor, stats.maxArmor);

                UpdateBars(stats);
            }
        }
    }
    private void DealDamage(MobStats target, float damage)
    {
        float remainingDamage = damage;

        if (target.currentArmor > 0)
        {
            float armorDamage = Mathf.Min(target.currentArmor, remainingDamage);
            target.currentArmor -= armorDamage;
            remainingDamage -= armorDamage;
        }

        if (remainingDamage > 0)
        {
            target.currentHealth -= remainingDamage;
        }
    }

    private void UpdateBars(MobStats stats)
    {
        Healthbars bars = stats.GetComponent<Healthbars>();

        if (bars != null)
        {
            bars.UpdateHealthBars();
        }
    }
}