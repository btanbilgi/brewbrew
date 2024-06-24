/*
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform[] spheres; // Array to hold all sphere transforms
    private Coroutine[] moveCoroutines; // Array to store move coroutines for each sphere

    public Transform planeTransform; // The transform of the plane GameObject
    public float speed = 5.0f; // Speed of the spheres' movement

    void Start()
    {
        mainCamera = Camera.main; // Use Camera.main to find the main camera

        // Assuming spheres are children of this GameObject, get all sphere transforms
        spheres = new Transform[transform.childCount];
        moveCoroutines = new Coroutine[spheres.Length];

        for (int i = 0; i < spheres.Length; i++)
        {
            spheres[i] = transform.GetChild(i); // Assign each child transform to the array
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray to check which sphere was clicked
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Transform clickedSphere = hit.transform;

                // Move all other spheres towards the clicked sphere
                for (int i = 0; i < spheres.Length; i++)
                {
                    if (spheres[i] != clickedSphere)
                    {
                        MoveSphere(spheres[i], clickedSphere.position);
                    }
                }
            }
        }
    }

    void MoveSphere(Transform sphere, Vector3 targetPosition)
    {
        // Stop any existing tween on the sphere
        int index = System.Array.IndexOf(spheres, sphere);
        if (moveCoroutines[index] != null)
        {
            StopCoroutine(moveCoroutines[index]);
        }

        // Use DoTween to move the sphere to the target position over time
        moveCoroutines[index] = StartCoroutine(MoveObject(sphere, targetPosition));
    }

    IEnumerator MoveObject(Transform sphere, Vector3 targetPosition)
    {
        while (Vector3.Distance(sphere.position, targetPosition) > 0.1f)
        {
            sphere.position = Vector3.Lerp(sphere.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        sphere.position = targetPosition; // Ensure the final position is exactly the target
    }

    private bool IsPointOnPlane(Vector3 targetPosition)
    {
        float x = targetPosition.x;
        float y = targetPosition.y;
        return -5 < x && x < 5 && -5 < y && y < 5; // Adjust as per your plane's dimensions
    }
}
*/
using UnityEngine;
using DG.Tweening; // Import the DoTween namespace

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private bool canClick = true; // Flag to control clicking on spheres
    private Rigidbody playerRb;
    public string sphereTag;
    
    public float moveSpeed = 1.5f;
    public float scaleEndValue = 0;
    public float scaleDuration = 3.0f;
    GameObject targetSphere;

    void Start()
    {
        mainCamera = Camera.main; // Use Camera.main to find the main camera
    }

    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
        {
            // Raycast to detect which sphere is clicked
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object is a sphere and same colour
                if (hit.collider.CompareTag(sphereTag))
                {
                    // Disable further clicking until movement is complete
                    canClick = false;
                    targetSphere = hit.collider.gameObject;
                    // Move all spheres towards the clicked sphere
                    MoveSpheresTowards(targetSphere);
                }
            }
        }

        if (GameObject.FindGameObjectsWithTag(sphereTag).Length == 1)
        {
            targetSphere.transform.DOScale(scaleEndValue, scaleDuration);
                //.OnComplete(() => DestroySphere(targetSphere))
        }
    }

    void MoveSpheresTowards(GameObject targetSphere)
    {
        //foreach (GameObject sphere in GameObject.FindGameObjectsWithTag(transform.tag))
        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag(sphereTag))
        {
            if (sphere == targetSphere) // Skip the sphere that was clicked
            {
                playerRb = sphere.GetComponent<Rigidbody>();
                playerRb.constraints = RigidbodyConstraints.FreezePosition;
            }
            else
            {
                // Use DoTween to move the sphere towards the target position
                sphere.transform.DOMove(targetSphere.transform.position, moveSpeed)
                    .SetEase(Ease.Linear)
                    .SetSpeedBased()
                    .OnComplete(() => DestroySphere(sphere)); // Destroy the sphere after movement
                    //.OnComplete(() => targetSphere.transform.DOScale(scaleEndValue, scaleDuration));
                
            }
        }
    }

    

    void DestroySphere(GameObject sphere)
    {
        // Destroy or deactivate the sphere
        Destroy(sphere);
        // Optionally, you can also instantiate a particle effect or perform other actions here
        // Instantiate(particleEffect, sphere.transform.position, Quaternion.identity);
    }
}
