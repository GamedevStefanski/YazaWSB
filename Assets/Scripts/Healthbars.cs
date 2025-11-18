using UnityEngine;
using UnityEngine.UI;

public class Healthbars : MonoBehaviour
{
    [SerializeField] private Slider armorSlider;
    [SerializeField] private Slider healthBarSlider;

    private float maxHealth;
    private float currentHealth;
    private float maxArmor;
    private float currentArmor;

    public void UpdateHealthBars()
    {
        healthBarSlider.value = GetComponent<MobStats>().currentHealth / GetComponent<MobStats>().maxHealth; ;
        armorSlider.value = GetComponent<MobStats>().currentArmor / GetComponent<MobStats>().maxArmor;
    }
}
