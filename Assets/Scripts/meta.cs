using UnityEngine;
using UnityEngine.Events;

public class Meta : MonoBehaviour
{
    public UnityEvent OnWin;

    private void Start()
    {
        if (OnWin == null)
        {
            OnWin = new UnityEvent();
        }
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
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.playerController.canMove = false;
            ScoreManager.Instance.EndLevel();
            ScoreManager.Instance.managerUI.winScreen();
        }
    }
}