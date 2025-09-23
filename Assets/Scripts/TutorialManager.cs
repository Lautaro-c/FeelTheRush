using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPanels;

    public void OnPlayerMoved()
    {
        ShowPanel(0);
        HidePanel(1);
    }

    public void OnPlayerJump()
    {
        HidePanel(0);
        ShowPanel(1);
        HidePanel(2);
    }

    public void OnPlayerWallRun()
    {
        HidePanel(1);
        ShowPanel(2);
        HidePanel(3);
    }

    public void OnPlayerAttackEnemy()
    {
        HidePanel(2);
        ShowPanel(3);
    }

    public void Desactivate()
    {
        HidePanel(0);
        HidePanel(1);
        HidePanel(2);
        HidePanel(3);
    }

    void ShowPanel(int index)
    {
        tutorialPanels[index].SetActive(true);
    }

    void HidePanel(int index)
    {
        tutorialPanels[index].SetActive(false);
    }
}