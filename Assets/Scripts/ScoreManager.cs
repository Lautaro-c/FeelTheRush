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
    public int enemiesKilled;
    public TMP_Text ScoreText;
    public TMP_Text TimeText;
    public TMP_Text EnemiesText;
    [SerializeField] private GameObject panel;
    private int score;
    [SerializeField] private int AllAliens;

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

    public void ReceiveAllEnemies(int allEnemies)
    {
        AllAliens = allEnemies;
    }

    public void EndLevel()
    {
        float finalTime = TimeElapsed;
        int totalKills = enemiesKilled;
        score = (totalKills * 100) + Mathf.Max(0, (int)(1000 - finalTime * 10));
        panel.SetActive(true);
        ScoreText.text = score.ToString();
        TimeText.text = finalTime.ToString("F2");
        EnemiesText.text = totalKills.ToString() + "/" + AllAliens.ToString();
        SaveData();
    }

    private void SaveData()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.GetInt($"lvl{sceneID}Score") <= score)
        {
            PlayerPrefs.SetInt($"lvl{sceneID}Kills", enemiesKilled);
            PlayerPrefs.SetInt($"lvl{sceneID}Aliens", AllAliens);
            PlayerPrefs.SetFloat($"lvl{sceneID}Time", TimeElapsed);
            PlayerPrefs.SetInt($"lvl{sceneID}Score", score);
        }
        PlayerPrefs.SetInt("lastLvlPlayed", sceneID);
    }
}