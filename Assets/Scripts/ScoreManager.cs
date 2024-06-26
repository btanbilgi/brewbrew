using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private static int scoreCount = 0;
    private bool IsIgnoreScore = false;

    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();    
    }

    private void Update()
    {
        scoreText.text = "Score: " + scoreCount;
    }
    public void addScore()
    {
        if (!IsIgnoreScore)
        {
            scoreCount++;
        }
        else
        {
            IsIgnoreScore = false;
        }
    }

    public void ignoreScore()
    {
        IsIgnoreScore = true;
    }
}
