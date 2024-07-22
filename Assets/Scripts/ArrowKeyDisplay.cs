using UnityEngine;
using System.Collections.Generic;

public class ArrowKeyDisplay : MonoBehaviour
{
    public GameObject[] arrowSprites;  // Array of arrow key sprites
    public GameObject arrowSpritePrefab;  // Prefab for arrow key sprites

    private List<GameObject> spawnedSprites;  // List to store spawned arrow sprites
    private int currentIndex = 0;  // Current index in the spawnedSprites list



    private void Start()
    {
        spawnedSprites = new List<GameObject>();


    }

    private void Update()
    {
        // Check for arrow key inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SpawnArrowSprite(0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SpawnArrowSprite(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SpawnArrowSprite(2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SpawnArrowSprite(3);
        }
        // Check for delete key input
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            ClearArrowSprites();
        }
        
    }

    private void SpawnArrowSprite(int index)
    {
        // Check if the index is within the range of the arrowSprites array
        if (index >= 0 && index < arrowSprites.Length)
        {
            // Instantiate the arrow sprite prefab
            GameObject arrowSprite = Instantiate(arrowSpritePrefab, transform);

            switch (index)
            {
                case 0:
                    arrowSprite.tag = "left";
                    break;

                case 1:
                    arrowSprite.tag = "right";
                    break;


                case 2:
                    arrowSprite.tag = "up";
                    break;

                case 3:
                    arrowSprite.tag = "down";
                    break;

            }

            // Set the position of the arrow sprite in the line
            float xPos = currentIndex * 1.0f;  // Adjust the spacing between sprites
            arrowSprite.transform.localPosition = new Vector3(xPos - 1, 2f, 0f);

            // Set the arrow sprite's sprite based on the index
            SpriteRenderer spriteRenderer = arrowSprite.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = arrowSprites[index].GetComponent<SpriteRenderer>().sprite;

            // Store the spawned arrow sprite in the list
            spawnedSprites.Add(arrowSprite);

            currentIndex++;  // Increment the current index
        }
    }

    public void ClearArrowSprites()
    {
        // Destroy all spawned arrow sprites and clear the list
        foreach (GameObject arrowSprite in spawnedSprites)
        {
            if (arrowSprite != null)
            {


                Destroy(arrowSprite);
            }
        }

        spawnedSprites.Clear();
        currentIndex = 0;  // Reset the current index
    }


    public List<GameObject> ReturnSprites()
    {
        return spawnedSprites;
    }

}