using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private float levelStartTime;
    private int enemiesKilled;

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
        DontDestroyOnLoad(gameObject);
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

        Debug.Log($"Nivel terminado en {finalTime:F2} segundos. Enemigos eliminados: {totalKills}");

        // Aquí podés guardar los datos, mostrar UI, enviar a leaderboard, etc.
    }

}
