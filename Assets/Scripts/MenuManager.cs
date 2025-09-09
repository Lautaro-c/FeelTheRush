using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header ("Canvas")]
    [SerializeField] private Canvas principalCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas creditsCanvas;
    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI playTxt;
    [SerializeField] private TextMeshProUGUI optionsTxt;
    [SerializeField] private TextMeshProUGUI creditsTxt;
    [SerializeField] private TextMeshProUGUI backTxt;
    [SerializeField] private TextMeshProUGUI back2Txt;
    [SerializeField] private TextMeshProUGUI exitTxt;
    [SerializeField] private TextMeshProUGUI languajeTxt;
    [SerializeField] private TextMeshProUGUI creditsTo;   
    public bool english;
    private int lastLvlPlayed = 0;

    private void Awake()
    {
        principalCanvas = GameObject.Find("PrincipalCanvas").GetComponent<Canvas>();
        optionsCanvas = GameObject.Find("OptionsCanvas").GetComponent<Canvas>();
        creditsCanvas = GameObject.Find("CreditsCanvas").GetComponent<Canvas>();
        playTxt = GameObject.Find("PlayTxt").GetComponent<TextMeshProUGUI>();
        optionsTxt = GameObject.Find("OptionsTxt").GetComponent<TextMeshProUGUI>();
        creditsTxt = GameObject.Find("CreditsTxt").GetComponent<TextMeshProUGUI>();
        creditsTo = GameObject.Find("CreditsTo").GetComponent<TextMeshProUGUI>();
        backTxt = GameObject.Find("BackTxt").GetComponent<TextMeshProUGUI>();
        back2Txt = GameObject.Find("BackTxt2").GetComponent<TextMeshProUGUI>();
        exitTxt = GameObject.Find("ExitTxt").GetComponent<TextMeshProUGUI>();
        languajeTxt = GameObject.Find("LanguajeTxt").GetComponent<TextMeshProUGUI>();
        principalCanvas.enabled = true;
        optionsCanvas.enabled = false;
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
    

    public void optionsButton()
    {
        principalCanvas.enabled=false;
        optionsCanvas.enabled=true;
        creditsCanvas.enabled=false;
    }

    public void creditsButton()
    {
        principalCanvas.enabled = false;
        optionsCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void backButton()
    {   
        principalCanvas.enabled = true;
        optionsCanvas.enabled = false;
        creditsCanvas.enabled = false;
    }

    public void languajeButton()
    {
        if (english)
        {
            english = false;
            playTxt.text = "Play";
            optionsTxt.text = "Options";
            creditsTxt.text = "Credits";
            backTxt.text = "Main Menu";
            back2Txt.text="Main Menu";
            exitTxt.text = "Exit";
            languajeTxt.text = "Languaje";
        }
        else
        {
            english = true;
            playTxt.text = "Jugar";
            optionsTxt.text = "Opciones";
            creditsTxt.text = "Creditos";
            backTxt.text = "Menú";
            back2Txt.text = "Menú";
            exitTxt.text = "Salir";
            languajeTxt.text = "Lenguaje";
        }
    }
}
