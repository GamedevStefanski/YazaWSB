using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class ShopButtons : MonoBehaviour
{

    [SerializeField] GameObject mob;
    private GameplayManager gameplayManager;
    private GameObject friendlySpawner;
    private static int spawnOrderCounter = 1;

    private void Awake()
    {
        gameplayManager = GameObject.Find("GameplayManager").GetComponent<GameplayManager>();
        // ZnajdŸ wszystkie obiekty z tagiem "Friendly"
        GameObject[] friendlyObjects = GameObject.FindGameObjectsWithTag("Friendly");

        // Przeszukaj je, aby znaleŸæ ten, którego nazwa koñczy siê na "Fortress"
        foreach (GameObject obj in friendlyObjects)
        {
            if (obj.name.EndsWith("Fortress")) // Jeœli nazwa mo¿e mieæ coœ jeszcze po s³owie Fortress, u¿yj .Contains("Fortress")
            {
                friendlySpawner = obj;
                break; // ZnaleŸliœmy odpowiedni obiekt, wiêc zatrzymujemy pêtlê
            }
        }

        // Opcjonalne zabezpieczenie przed b³êdami (dobra praktyka)
        if (friendlySpawner == null)
        {
            Debug.LogWarning("Nie znaleziono obiektu z tagiem Friendly i nazw¹ koñcz¹c¹ siê na Fortress!");
        }
    }

    private void Start()
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = mob.name + " " + mob.GetComponent<MobStats>().InkCost;
    }
    public void PurchaseMob()
    {
        if (mob.GetComponent<MobStats>().InkCost <= gameplayManager.ink)
        {
            gameplayManager.ink -= mob.GetComponent<MobStats>().InkCost;

            // Twoje instancjonowanie jednostki
            GameObject friendlyMob = Instantiate(mob, friendlySpawner.transform);

            friendlyMob.GetComponent<MeshRenderer>().sortingOrder = spawnOrderCounter;

            // Zwiêksz licznik, ¿eby nastêpna jednostka by³a jeszcze wy¿ej
            spawnOrderCounter++;

            // --- ANIMACJA SPAWNERA ---
            SkeletonAnimation spawnerAnim = friendlySpawner.GetComponent<SkeletonAnimation>();

            if (spawnerAnim != null)
            {
                // 1. Odtwórz animacjê otwierania (false = odtwórz tylko raz)
                spawnerAnim.AnimationState.SetAnimation(0, "Open", false);

                // 2. Dodaj do kolejki animacjê zamykania. 
                // Jeœli jednak nadal chcesz, ¿eby namiot posta³ otwarty przez np. 1 sekundê,
                // zmieñ ostatni parametr z 0f na 1f.
                spawnerAnim.AnimationState.AddAnimation(0, "Closed", false, 0f);

                // 3. Gdy namiot ju¿ siê zamknie, niech p³ynnie przejdzie do pêtli Idle
                spawnerAnim.AnimationState.AddAnimation(0, "Idle", true, 0f);
            }
        }
    }
}
