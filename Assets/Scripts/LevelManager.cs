using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // list of levels, each of which contains a list of enemies
    // spawns enemies and signals to ScoreKeeper every level
    [SerializeField] List<Level> levels;
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
    }
}