using UnityEngine;
using UnityEngine.Events;

public class meta : MonoBehaviour
{
    public UnityEvent OnWin;

    private void Start()
    {
        if (OnWin == null)
        {
            OnWin = new UnityEvent();
        }
          
        // Pod�s agregar listeners desde el inspector o por c�digo:
        OnWin.AddListener(TriggerWinSequence);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnWin.Invoke();
        }
    }


    private void TriggerWinSequence()
    {
        // Acced� al ScoreManager y ejecut� la l�gica de fin de nivel
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.playerController.canMove = false;
            ScoreManager.Instance.EndLevel();
            ScoreManager.Instance.managerUI.winScreen();
        }
    }

}
