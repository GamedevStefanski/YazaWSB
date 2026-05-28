using UnityEngine;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isOpen = !isOpen;

        mainMenu.SetActive(isOpen);

        
        Time.timeScale = isOpen ? 0f : 1f;

       
    }
}