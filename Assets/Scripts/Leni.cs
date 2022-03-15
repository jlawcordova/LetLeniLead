using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class Leni : MonoBehaviour
{
    TouchMover touchMover;
    public GameObject camera;

    public int Speed = 1;

    void Awake()
    {
        Debug.Log("Leni Instance!");
        touchMover = TouchMover.Instance;
    }

    void OnEnable()
    {
        touchMover.OnStartTouch += Move;
    }

    void OnDisable()
    {
        touchMover.OnStartTouch -= Move;
    }


    void Move(Vector2 position)
    {
        Debug.Log("Leni Move!");
        var touchPosition = new Vector2(position.x, position.y);
        var worldPosition = camera.GetComponent<Camera>().ScreenToWorldPoint(touchPosition);
        worldPosition.x = transform.position.x;
        worldPosition.z = 0;
        transform.position = worldPosition;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Debug.Log("Input.touchCount " + Input.touchCount);
        }
        // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        // {
        //     Debug.Log("Touched");

        //     // Get movement of the finger since last frame
        //     Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        //     // Move object across X plane
        //     transform.Translate(touchDeltaPosition.x * Speed, 0, 0);
        // }

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.down);

        // If it hits something...
        if (hit.collider != null)
        {
            var heart = hit.collider.gameObject.GetComponent<Heart>();
            var score = heart.Consume();
            ScoreManager.AddScore(score);
            Destroy(hit.collider.gameObject);
        }
    }

    
}
