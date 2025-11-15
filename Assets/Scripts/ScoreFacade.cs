using UnityEngine;

public static class ScoreFacade
{
    // Devuelve tiempo transcurrido sin que el resto del juego toque el ScoreManager
    public static float GetTimeElapsed()
    {
        return ScoreManager.Instance.TimeElapsed;
    }

    // Registra una baja
    public static void AddKill()
    {
        ScoreManager.Instance.RegisterKill();
    }

    // Obtiene kills actuales
    public static int GetKills()
    {
        return ScoreManager.Instance.EnemiesKilled;
    }

    // Termina el nivel de forma segura
    public static void FinishLevel()
    {
        ScoreManager.Instance.EndLevel();
    }

    // Activa la pantalla de victoria
    public static void TriggerWinScreen()
    {
        ScoreManager.Instance.managerUI.winScreen();
    }

    // Para cuando el jugador entra al trigger de final
    public static void TriggerLevelEnd(PlayerController player)
    {
        player.canMove = false;
        FinishLevel();
        TriggerWinScreen();
    }
}

