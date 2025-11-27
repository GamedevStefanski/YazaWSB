using UnityEngine;
using UnityEngine.UI;

public class ShopButtons : MonoBehaviour
{

    [SerializeField] GameObject mob;
    private GameplayManager gameplayManager;
    private GameObject friendlySpawner;

    private void Awake()
    {
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        friendlySpawner = GameObject.Find("FriendlySpawner");
    }

    private void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = mob.name + " " + mob.GetComponent<MobStats>().InkCost;
    }
    public void PurchaseMob()
    {
        if(mob.GetComponent<MobStats>().InkCost <= gameplayManager.ink)
        {
            gameplayManager.ink -= mob.GetComponent<MobStats>().InkCost;

            GameObject friendlyMob = Instantiate(mob, friendlySpawner.transform);
            friendlyMob.transform.SetParent(friendlySpawner.transform);
        }
    }
}
