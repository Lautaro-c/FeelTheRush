using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text SpeedText;
    [SerializeField] private PlayerController playerController;
    

    public GameObject panelUI;
    void Start()
    {
        
    }

    void Update()
    {
        SpeedText.text = "X" + playerController.SpeedMultiplier.ToString();
    }

    public void winScreen()
    {
        panelUI.SetActive(true);

       
    }

    
}
