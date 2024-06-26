using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    GameManager gameManager;
    ScoreManager scoreManager;
    private void Awake()
    {
        scoreManager = ScoreManager.instance;
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.TogglePause();
        gameManager.fail.SetActive(true);
        gameObject.transform.DOScale(0, 0);
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), 0);
        other.transform.DOScale(0,0);
        scoreManager.ignoreScore();
    }
}
