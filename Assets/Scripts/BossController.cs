using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRB;

    public GameObject[] arrowSprites;  // Array of arrow key sprites
    public GameObject arrowSpritePrefab;  // Prefab for arrow key sprites

    private List<GameObject> spawnedSprites;  // List to store spawned arrow sprites
    private int currentIndex = 0;  // Current index in the spawnedSprites list

    private ArrowKeyDisplay wiz;  // Prefab for arrow key sprites
    private ScoreManager scoreManager;
    public int score;
    public int damage;

    private bool isPlayerInContact;  // Flag to track if the player is in contact with the enemy
    private Coroutine damageCoroutine;  // Coroutine reference to damage the player repeatedly

    // Start is called before the first frame update
    void Start()
    {
        wiz = FindObjectOfType<ArrowKeyDisplay>();
        scoreManager = FindObjectOfType<ScoreManager>();
        spawnedSprites = new List<GameObject>();
        myRB = GetComponent<Rigidbody2D>();

        for (int i = 0; i < 10; i++)
        {
            int rand = UnityEngine.Random.Range(1, 5);
            SpawnArrowSprite(rand - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = new Vector3(-moveSpeed, myRB.velocity.y, 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckMatch(wiz.ReturnSprites()))
            {
                scoreManager.IncreaseScore(score);
                Destroy(gameObject);
                wiz.ClearArrowSprites();
            }
        }
    }

    private void SpawnArrowSprite(int index)
    {
        if (index >= 0 && index < arrowSprites.Length)
        {
            GameObject arrowSprite = Instantiate(arrowSpritePrefab, transform);

            switch (index)
            {
                case 0:
                    arrowSprite.tag = "right";
                    break;
                case 1:
                    arrowSprite.tag = "left";
                    break;
                case 2:
                    arrowSprite.tag = "up";
                    break;
                case 3:
                    arrowSprite.tag = "down";
                    break;
            }

            float xPos = currentIndex;
            arrowSprite.transform.localPosition = new Vector3(xPos - 1.5f, 2f, 0f);

            SpriteRenderer spriteRenderer = arrowSprite.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = arrowSprites[index].GetComponent<SpriteRenderer>().sprite;

            spawnedSprites.Add(arrowSprite);
            currentIndex++;
        }
    }

    public bool CheckMatch(List<GameObject> compareList)
    {
        if (spawnedSprites.Count != compareList.Count)
        {
            return false;
        }

        for (int i = 0; i < spawnedSprites.Count; i++)
        {
            if (spawnedSprites[i].tag != compareList[compareList.Count - 1 - i].tag)
            {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isPlayerInContact)  // Avoid starting multiple coroutines
            {
                isPlayerInContact = true;
                damageCoroutine = StartCoroutine(ApplyDamageRepeatedly());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInContact = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator ApplyDamageRepeatedly()
    {
        while (isPlayerInContact)
        {
            scoreManager.TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }
    }
}