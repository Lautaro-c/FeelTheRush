using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    private bool fullScreen;
    [SerializeField] private Toggle screenToggle;

    private void Start()
    {
        if (PlayerPrefs.HasKey("fullScreen"))
        {
            if(PlayerPrefs.GetInt("fullScreen") == 1)
            {
                Screen.fullScreen = true;
                screenToggle.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                screenToggle.isOn = false;
            }
        }
        else
        {
            screenToggle.isOn = true;
            SetFullScreen();
        }
    }

    public void SetFullScreen()
    { 
        if (!screenToggle.isOn)
        {
            PlayerPrefs.SetInt("fullScreen", 0);
            fullScreen = false;
        }
        else
        {
            PlayerPrefs.SetInt("fullScreen", 1);
            fullScreen = true;
        }
        Screen.fullScreen = fullScreen;
    }
}
