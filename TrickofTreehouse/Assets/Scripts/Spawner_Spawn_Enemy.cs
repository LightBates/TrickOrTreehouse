using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class Spawner_Spawn_Enemy : MonoBehaviour {

    // Array of waypoints, the path the enemy will follow
    public GameObject[] waypoints;
    // The enemy that will be spawned
    public GameObject enemy;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    // Use this for initialization
    void Start () {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }
	
	// Update is called once per frame
	void Update () {
        // Get the current wave
        int currentWave = gameManager.Wave;
        // If we haven't reached the last wave
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;

            // If we haven't started spawning enemies, and it is time start a new wave OR it is time to spawn another wave
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies)
            {
                // update last spawntime, create a new enemy
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
                newEnemy.GetComponent<Enemy_Movement>().waypoints = waypoints;
                enemiesSpawned++;
            }
            // If we have spawned all the enemies of this wave...
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                //gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            
        }
        // If we have reached the last wave...
        else
        {
            //gameManager.gameOver = true;
            //GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            //gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}
