using UnityEngine;
using UnityEngine.UI;

public class STOPBUTTON : MonoBehaviour
{
    public Button pauseButton;  
    private bool isPaused = true; 

    void Start()
    {
        Time.timeScale = 0f;

        pauseButton.onClick.AddListener(TogglePause);
    }
    void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseButton.gameObject.SetActive(false);  // إخفاء الزر بعد الضغط
        }
    }
}
