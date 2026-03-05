using UnityEngine;
using UnityEngine.Events;

public class Meta : MonoBehaviour
{
    public UnityEvent OnWin;
    [SerializeField] private int allEnemies;

    private void Start()
    {
        if (OnWin == null)
        {
            OnWin = new UnityEvent();
        }
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ReceiveAllEnemies(allEnemies);
        }
        else
        {
            Debug.Log("No existe score manager");
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