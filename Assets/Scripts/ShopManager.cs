using UnityEngine;

public class ScribeManager : MonoBehaviour
{
    [SerializeField] GameObject[] shops;
    [SerializeField] GameObject shopParentObject;
    [SerializeField] GameObject[] availableShops;
    private void Start()
    {
        //Debug - delete later
        ShopSetup(1);
    }

    public void ShopSetup(int ScribeID)
    {
        GameObject shopUI = Instantiate(shops[ScribeID], shopParentObject.transform);
        shopUI.transform.SetParent(shopParentObject.transform);
    }
}
