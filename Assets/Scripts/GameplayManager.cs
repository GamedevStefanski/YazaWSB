using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int ink;
    [SerializeField] private GameObject InkTextObject;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(7, 7);
    }

    private void Update()
    {
        InkTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Ink owned: " + ink;
    }
}
