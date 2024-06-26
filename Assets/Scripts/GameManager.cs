using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused = false;
    public GameObject fail;

    private void Start()
    {
        fail = GameObject.Find("Fail");
        fail.SetActive(false);
    }

    void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()

    {
        // Check for pause/unpause input (the P key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        
        // Optionally, enable or disable other components or systems here
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
