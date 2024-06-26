using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] bool isPaused = false;
    public GameObject fail;

    ScoreManager keepScore;

    private Camera mainCamera;
    private bool canClick = true;

    private GameObject targetSphere;
    private Rigidbody playerRb;
    private MovementController moveScript;
    [SerializeField] float moveSpeed = 1.5f;

    private void Start()
    {
        keepScore = ScoreManager.instance;
        mainCamera = Camera.main; 

        //UNUTMA BURAYI DEĞİŞTİR
        fail = GameObject.Find("Fail");
        fail.SetActive(false);
        //////////////////////////////////////////
    }

    void Awake()
    {

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (IsPaused())
        {
            return;
        }

        if (canClick && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Ball>() != null && hit.collider.GetComponent<Ball>().canClick) 
                {
                    GameObject ball = hit.collider.gameObject;

                    ball.GetComponent<Ball>().canClick = false;

                    targetSphere = hit.collider.gameObject;
                    MoveSpheresTowards(targetSphere);
                }
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
    public bool IsPaused()
    {
        return isPaused;
    }
    
    public void MoveSpheresTowards(GameObject targetSphere)
    {

        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag(targetSphere.tag))
        {
            playerRb = targetSphere.GetComponent<Rigidbody>();
            playerRb.constraints = RigidbodyConstraints.FreezePosition;
            moveScript = sphere.GetComponent<MovementController>();
            moveScript.moveTween.Kill();
            
            if (sphere == targetSphere)
            {
                keepScore.addScore();
            }
            else
            {
                sphere.transform.DOMove(targetSphere.transform.position, moveSpeed)
                    .SetEase(Ease.Linear)
                    .SetSpeedBased()
                    .OnComplete(() =>
                    {
                        scaleDownSphere(sphere);
                        scaleDownSphere(targetSphere);
                        keepScore.addScore();
                    });
            }

        }
    }

    void scaleDownSphere(GameObject sphere)
    {
        sphere.transform.DOScale(0, 1.0f);
    }
}
