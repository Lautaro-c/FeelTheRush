using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text SpeedText;
    [SerializeField] private PlayerController playerController;
    void Start()
    {
        
    }

    void Update()
    {
        SpeedText.text = "X" + playerController.SpeedMultiplier.ToString();
    }
}
