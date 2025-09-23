using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject speedCounter;
    [SerializeField] private Sprite[] speedNum;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject emptySpeed;

    [SerializeField] private Image multiplierBar;
    

    public GameObject panelUI;

    private int multiplier;

    void Update()
    {
        //Se iguala el 'multiplier' al 'SpeedMultiplier'.
        //El 'FloorToInt' no solo convierte el float en int, necesario para el array,...
        //Sino que también para redondear el número para abajo.
        multiplier = Mathf.FloorToInt(playerController.SpeedMultiplier);

        //Se asegura que el valor esté entre 1 y el largo del array, 5 en este caso.
        multiplier = Mathf.Clamp(multiplier, 1, speedNum.Length);

        //Elige el sprite
        speedCounter.GetComponent<Image>().sprite = speedNum[multiplier - 1];

        if (playerController.SpeedMultiplier > 1)
        {
            float t = playerController.timeSinceLastDecrement;
            float interval = playerController.decrementInterval;

            float fill = Mathf.Clamp01(1f - (t / interval));

            multiplierBar.fillAmount = fill;
        }
        else
        {
            multiplierBar.fillAmount = 0f;
        }
    }

    public void winScreen()
    {
        emptySpeed.SetActive(false);
        panelUI.SetActive(true);
    }
}