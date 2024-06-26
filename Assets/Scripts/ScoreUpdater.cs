using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public int scoreCount = 0;

    public TextMeshProUGUI scoreText;
    
    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();    
    }

    private void Update()
    {
        scoreText.text = "Score: " + scoreCount;
    }
}
