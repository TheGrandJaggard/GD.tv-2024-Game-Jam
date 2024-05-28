using UnityEngine;

public static class ScoreKeeper
{
    #region Public Setters
    public static void LevelReached(int level)
    {
        AddToRunLevelsCompleted();
        AddToTotalLevelsCompleted();
        SetHighestLevel(level);
    }

    public static void DamgeDealt(float damage)
    {
        AddToRunDamageDealt(damage);
        AddToTotalDamageDealt(damage);
    }

    public static void EnemyKilled()
    {
        AddToTotalEnemiesKilled();
        AddToRunEnemiesKilled();
    }

    public static void PlayerDodged()
    {
        AddToRunPlayerDodges();
        AddToTotalPlayerDodges();
    }
    #endregion

    #region Private Run-level Setters
    private static void AddToRunLevelsCompleted()
    {
        PlayerPrefs.SetFloat("RunLevelsCompleted", PlayerPrefs.GetFloat("RunLevelsCompleted") + 1);
    }

    private static void AddToRunDamageDealt(float damage)
    {
        PlayerPrefs.SetFloat("RunDamageDealt", PlayerPrefs.GetFloat("RunDamageDealt") + damage);
    }

    private static void AddToRunEnemiesKilled()
    {
        PlayerPrefs.SetFloat("RunEnemiesKilled", PlayerPrefs.GetFloat("RunEnemiesKilled") + 1);
    }
    
    private static void AddToRunPlayerDodges()
    {
        PlayerPrefs.SetFloat("RunPlayerDodges", PlayerPrefs.GetFloat("RunPlayerDodges") + 1);
    }
    #endregion

    #region Private Game-wide Setters
    private static void SetHighestLevel(int level)
    {
        if (level > PlayerPrefs.GetInt("HighestLevel"))
        {
            PlayerPrefs.SetInt("HighestLevel", level);
        }
    }

    private static void AddToTotalLevelsCompleted()
    {
        PlayerPrefs.SetFloat("TotalLevelsCompleted", PlayerPrefs.GetFloat("TotalLevelsCompleted") + 1);
    }

    private static void AddToTotalDamageDealt(float damage)
    {
        PlayerPrefs.SetFloat("TotalDamageDealt", PlayerPrefs.GetFloat("TotalDamageDealt") + damage);
    }

    private static void AddToTotalEnemiesKilled()
    {
        PlayerPrefs.SetFloat("TotalEnemiesKilled", PlayerPrefs.GetFloat("TotalEnemiesKilled") + 1);
    }
    
    private static void AddToTotalPlayerDodges()
    {
        PlayerPrefs.SetFloat("TotalPlayerDodges", PlayerPrefs.GetFloat("TotalPlayerDodges") + 1);
    }
    #endregion
}