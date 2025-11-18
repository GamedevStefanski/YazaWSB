using UnityEngine;
using System.Collections;

public class MobCombat : MonoBehaviour
{
    public bool fighting = false;
    public IEnumerator Fight(GameObject target)
    {
        fighting = true;
        if (target == null)
            yield break;

        MobStats enemyStats = target.GetComponent<MobStats>();
        MobStats thisStats = GetComponent<MobStats>();

        while (target != null && target.activeInHierarchy)
        {
            if (enemyStats.currentHealth <= 0)
                yield break;

            if (thisStats.currentHealth <= 0)
                yield break;

            float dmg = thisStats.damage;

            // armor first
            if (enemyStats.currentArmor > 0)
            {
                float armorDamage = Mathf.Min(enemyStats.currentArmor, dmg);
                enemyStats.currentArmor -= armorDamage;
                dmg -= armorDamage;
            }

            // health second
            if (dmg > 0)
            {
                enemyStats.currentHealth -= dmg;
            }

            target.GetComponent<Healthbars>().UpdateHealthBars();
            yield return new WaitForSeconds(thisStats.CooldownBetweenAttacks);
        }
    }
}
