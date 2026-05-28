using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance;

    private Spell selectedSpell;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectSpell(Spell spell)
    {
        selectedSpell = spell;
        Debug.Log("Wybrano spell: " + spell.spellName);
    }

    public void CastSelectedSpell(MobStats target)
    {
        if (selectedSpell == null)
        {
            Debug.Log("Nie wybrano spella.");
            return;
        }
    }
}