using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementController : MonoBehaviour
{
    [SerializeField] bool vertical = false;
    [SerializeField] float verticalDistance = 5f;
    [SerializeField] float verticalDuration = 2f;

    [SerializeField] bool horizontal = true;
    [SerializeField] float horizontalDistance = 5f;
    [SerializeField] float horizontalDuration = 2f;

    [SerializeField] int startingDirection = 1;

    private GameManager gameManager;
    
    private Vector3 startPosition;

    public Tween moveTween;

    void Start()
    {
        gameManager = GameManager.instance;
        startPosition = transform.position;
        StartMoving();
    }

    private void Update()
    {
        if (gameManager.IsPaused())
        {
            moveTween.Pause();
            return;
        }
        else
        {
            moveTween.Play();
        }
    }

    void StartMoving()
    {
        Vector3 endPosition;

        if (horizontal)
        {
            endPosition = new Vector3(startPosition.x + horizontalDistance * startingDirection, startPosition.y, startPosition.z);
            moveTween = transform.DOMoveX(endPosition.x, horizontalDuration)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.InOutSine);
        }

        if(vertical)
        {
            endPosition = new Vector3(startPosition.x, startPosition.y + verticalDistance * startingDirection, startPosition.z);
            moveTween = transform.DOMoveY(endPosition.y, verticalDuration)
                     .SetLoops(-1, LoopType.Yoyo)
                     .SetEase(Ease.InOutSine);
        }
    }
}
