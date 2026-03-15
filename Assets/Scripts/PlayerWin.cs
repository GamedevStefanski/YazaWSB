using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    [SerializeField] private GameObject WinUI;
    private void OnDestroy()
    {
        Time.timeScale = 0;
        WinUI.SetActive(true);
    }
}
