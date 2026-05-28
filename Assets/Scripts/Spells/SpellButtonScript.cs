using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpellButton : MonoBehaviour
{
    [SerializeField] private Spell spell;

    [Header("UI")]
    [SerializeField] private Button button;

    private bool isOnCooldown = false;

    public void CastSpell()
    {
        if (isOnCooldown)
        {
            Debug.Log("Spell jest na cooldownie!");
            return;
        }

        spell.Cast();

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;

        button.interactable = false;

        yield return new WaitForSeconds(spell.cooldown);

        button.interactable = true;

        isOnCooldown = false;
    }
}