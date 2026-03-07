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
        HidePanel(7);
        ShowPanel(3);
    }

    public void OnPlayerSpeedMultiplier()
    {
        HidePanel(3);
        ShowPanel(4);
    }

    public void OnPlayerSlide()
    {
        HidePanel(4);
        ShowPanel(5);
    }

    public void OnPlayerParry()
    {
        HidePanel(5);
        ShowPanel(6);
    }

    public void OnRKey()
    {
        HidePanel(1);
        ShowPanel(7);
    }

    public void OnJumpKill()
    {
        ShowPanel(0);
    }

    public void Desactivate()
    {
        HidePanel(0);
        HidePanel(1);
        HidePanel(2);
        HidePanel(3);
        HidePanel(4);
        HidePanel(5);
        HidePanel(6);
    }

    void ShowPanel(int index)
    {
        if (index < tutorialPanels.Length)
        {
            if (tutorialPanels[index] != null)
            {
                tutorialPanels[index].SetActive(true);
            }
        }
    }

    void HidePanel(int index)
    {
        if (index < tutorialPanels.Length)
        {
            {
                if (tutorialPanels[index] != null)
                {
                    tutorialPanels[index].SetActive(false);
                }
            }
        }
    }
}