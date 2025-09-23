using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header ("Canvas")]
    [SerializeField] private Canvas principalCanvas;
    [SerializeField] private Canvas creditsCanvas;

    private int lastLvlPlayed = 0;

    private void Awake()
    {
        principalCanvas = GameObject.Find("PrincipalCanvas").GetComponent<Canvas>();
        creditsCanvas = GameObject.Find("CreditsCanvas").GetComponent<Canvas>();

        principalCanvas.enabled = true;
        creditsCanvas.enabled = false;
    }

    public void GoToLvl(int lvlIndex)
    {
        if(lastLvlPlayed==0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(lvlIndex);
        }
    }

    public void CreditsButton()
    {
        principalCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {   
        principalCanvas.enabled = true;
        creditsCanvas.enabled = false;
    }
}