using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int health;
    public TextMeshProUGUI scoreText;  // Reference to the Text component of the Canvas Text object
    public TextMeshProUGUI healthText;

    private void Start()
    {
        score = 0;
        health = 100;
        UpdateText();
    }

   
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateText();
    }

    public void TakeDamage(int damage) 
    {
        health = health - damage;
        UpdateText();

    }

    private void UpdateText()
    {
        scoreText.text = "Score: " + score;
        healthText.text = health + "%";
    }
}
