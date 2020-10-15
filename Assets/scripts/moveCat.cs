using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=sXc8baUK3iY

public class moveCat : MonoBehaviour
{
    // Start is called before the first frame update

    float deltaX, deltaY;


    // reference to Rigidbody2D component
    Rigidbody2D rb;
    Collider2D col;
    bool moveAllowed = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handle native touch events
        foreach (Touch touch in Input.touches) {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0) {
            if (Input.GetMouseButtonDown(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0) ) {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }


    }

    private void HandleTouch(int touchFingerId, Vector3 touchPos, TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
        case TouchPhase.Began:
    
        if (col == Physics2D.OverlapPoint (touchPos))
        {


                    // get the offset between position you touches
                    // and the center of the game object
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;


                    // if touch begins within the ball collider
                    // then it is allowed to move
                    moveAllowed = true;


                    // restrict some rigidbody properties so it moves
                    // more  smoothly and correctly
                    rb.freezeRotation = true;
                    rb.velocity = new Vector2 (0, 0);
                    rb.gravityScale = 0;
                    GetComponent<CircleCollider2D> ().sharedMaterial = null;
                }



    
            break;
        case TouchPhase.Moved:
            if (col == Physics2D.OverlapPoint (touchPos) && moveAllowed)
                    rb.MovePosition (new Vector2 (touchPos.x - deltaX, touchPos.y - deltaY));
            break;
        case TouchPhase.Ended:
            // TODO
            moveAllowed = false;
            break;
        }
    }

}
