using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int ink;
    [SerializeField] private GameObject InkTextObject;

    public int selectedChampionID = 0;
    [SerializeField] private GameObject[] availableShops;
    [SerializeField] private GameObject shopParentObject;

    void Awake()
    {
        // Ignore collision between allied mobs
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(7, 7);

        ShopSetup();
    }

    private void Update()
    {
        // Update ink value visible on the screen
        InkTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Ink owned: " + ink;
    }

    public void ShopSetup()
    {
        GameObject currentShop = Instantiate(availableShops[selectedChampionID], shopParentObject.transform);
        currentShop.transform.SetParent(shopParentObject.transform);
    }

}
