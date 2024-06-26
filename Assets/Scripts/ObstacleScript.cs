using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    GameManager gameManagerScript;
    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameManagerScript.TogglePause();
        gameManagerScript.fail.SetActive(true);
        Destroy(gameObject);
    }
}
