using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPanels; // Asigná los carteles en el inspector
    private int currentStep = 0;

    void Start()
    {
        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(i == 0); // Solo el primero activo
        }
    }

    public void OnPlayerMoved()
    {
        
        if (currentStep == 0)
        {
            HidePanel(0);
            ShowPanel(1); // Cartel de moverse con las teclas
            currentStep++;
        }
    }

    public void OnEnemySpawned()
    {
        if (currentStep == 1)
        {
            HidePanel(1);
            ShowPanel(2); // Cartel de generar enemigo
            currentStep++;
        }
    }

    public void OnEnemyHit()
    {
        if (currentStep == 2)
        {
            HidePanel(2);
            ShowPanel(3); // Cartel de golpear un enemigo
            currentStep++;
        }
    }

    void ShowPanel(int index)
    {
        tutorialPanels[index].SetActive(true);
        StartCoroutine(HideAfterSeconds(index, 5f));
    }

    IEnumerator HideAfterSeconds(int index, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        tutorialPanels[index].SetActive(false);

    }

    void HidePanel(int index)
    {
        tutorialPanels[index].SetActive(false);
    }

}
