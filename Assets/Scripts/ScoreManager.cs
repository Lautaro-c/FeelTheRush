using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public UIManager managerUI;
    public static ScoreManager Instance;
    public PlayerController playerController;
    public GameObject wallInvisibity;
    private float levelStartTime;
    private int enemiesKilled;
    public TMP_Text ScoreText;

    public float TimeElapsed => Time.time - levelStartTime;
    public int EnemiesKilled => enemiesKilled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        levelStartTime = Time.time;
        enemiesKilled = 0;
    }

    public void RegisterKill()
    {
        enemiesKilled++;
    }

    public void EndLevel()
    {
        float finalTime = TimeElapsed;
        int totalKills = enemiesKilled;
        int score = (totalKills * 100) + Mathf.Max(0, (int)(1000 - finalTime * 10));
        ScoreText.text = "Tiempo: " + finalTime.ToString("F2") + "s" + "\nEnemigos derrotados: " + totalKills + "\nPuntaje Final: " + score; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.canMove = false;
            EndLevel();
            managerUI.winScreen();
        }
    }
}