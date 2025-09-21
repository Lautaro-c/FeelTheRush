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
          
        // Podés agregar listeners desde el inspector o por código:
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
        // Accedé al ScoreManager y ejecutá la lógica de fin de nivel
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.playerController.canMove = false;
            ScoreManager.Instance.EndLevel();
            ScoreManager.Instance.managerUI.winScreen();
        }
    }

}
