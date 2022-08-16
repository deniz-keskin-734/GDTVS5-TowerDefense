using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    private void Start()
    {
        scoreText = FindObjectOfType<TMP_Text>();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
