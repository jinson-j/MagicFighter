using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int beatScore;
    public GameObject[] enemyPrefabs;  // Array of enemy prefabs
    public ScoreManager scoreManager;
    public GameObject continueImage;  // Reference to the continue image game object

    public float spawnInterval = 2f;  // Interval between enemy spawns
    private float nextSpawnTime = 0f;  // Time of the next spawn

    public float[] spawnPos;

    public bool canContinue = false;  // Flag to track if the player can continue to the next level

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (scoreManager.score >= beatScore && !canContinue)
        {
            // Display the continue image
            continueImage.SetActive(true);
            canContinue = true;
        }

        // Check for input to proceed to the next level or reset the current level
        if (canContinue)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                // Go to the next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // Reset the current level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (scoreManager.health <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            Instantiate(enemyPrefab, new Vector3(spawnPos[0], spawnPos[1], spawnPos[2]), Quaternion.identity);
        }
    }
}