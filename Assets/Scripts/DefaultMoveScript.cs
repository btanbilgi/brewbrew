using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DefaultMoveScript : MonoBehaviour
{
    public bool vertical = false;
    public float verticalDistance = 5f;
    public float verticalDuration = 2f;

    public bool horizontal = true;
    public float horizontalDistance = 5f;
    public float horizontalDuration = 2f;
    
    public int startingDirection = 1;

    private GameManager gameManagerScript;
    
    private Vector3 startPosition;

    public Tween moveTween;

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPosition = transform.position;
        StartMoving();
    }

    private void Update()
    {
        if (gameManagerScript.IsPaused())
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
