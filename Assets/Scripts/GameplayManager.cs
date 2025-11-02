using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(7, 7);
    }


}
