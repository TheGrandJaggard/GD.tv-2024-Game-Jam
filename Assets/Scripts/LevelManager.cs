using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // list of levels, each of which contains a list of enemies
    // spawns enemies and signals to ScoreKeeper every level
    [SerializeField] List<Level> levels;
    [SerializeField] int startingLevel;
    [SerializeField] GameObject endGameScreen;
    private int currentLevel = 0;
    private int currentEnemiesDead = 0;

    [System.Serializable] struct Level
    {
        public float damageAndHealthMult;
        public List<GameObject> enemies;
    }

    public void EnemyDied()
    {
        currentEnemiesDead += 1;
        // Debug.Log($"{currentEnemiesDead} out of {levels[currentLevel].enemies.Count} enemies dead");

        if (currentEnemiesDead == levels[currentLevel].enemies.Count)
        {
            StartCoroutine(SpawnNextWave());
            currentEnemiesDead = 0;
        }
    }

    private void Start()
    {
        endGameScreen.gameObject.SetActive(false);
        currentLevel = startingLevel;
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave()
    {
        currentLevel += 1;
        // Debug.Log($"Spawning wave {currentLevel} with {levels[currentLevel].enemies.Count} enemies");

        foreach (var enemy in levels[currentLevel].enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy = Instantiate(enemy);
        enemy.transform.position = new Vector3(
            10f, Random.Range(-5.5f, 1)
        );
        enemy.GetComponent<Health>().death += EnemyDied;
        enemy.GetComponent<Health>().SetHealthMult(levels[currentLevel].damageAndHealthMult);
        enemy.GetComponent<Enemy>().SetDamageMult(levels[currentLevel].damageAndHealthMult);
    }

    public void GameOver()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetDead();
        endGameScreen.SetActive(true);
        StartCoroutine(GoBackToMenu());
    }

    private IEnumerator GoBackToMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MenuScene");
    }
}