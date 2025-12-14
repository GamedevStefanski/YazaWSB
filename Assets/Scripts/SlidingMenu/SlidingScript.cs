using UnityEngine;
using UnityEngine.UI; 

public class SlidingMenu : MonoBehaviour
{
    [Header("Ustawienia")]
    public RectTransform menuPanel; 
    public float slideSpeed = 3.5f;  

    [Header("Pozycje (X)")]
    public float hiddenPosX;   
    public float visiblePosX; 

    private bool isOpen = false; 

    void Update()
    {
       
        float targetX = isOpen ? visiblePosX : hiddenPosX;

        
        Vector2 currentPosition = menuPanel.anchoredPosition;

        
        float newX = Mathf.Lerp(currentPosition.x, targetX, Time.deltaTime * slideSpeed);

        
        menuPanel.anchoredPosition = new Vector2(newX, currentPosition.y);
    }

    
    public void ToggleMenu()
    {
        isOpen = !isOpen; 

        
    }
}