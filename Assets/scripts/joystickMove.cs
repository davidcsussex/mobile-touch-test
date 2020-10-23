using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 pointA, pointB;
    private bool moveAllowed;
    
    
    // this variable is referenced by the gameObject that requires to read joystick input
    public Vector2 direction;

    public Transform outerJoystick;

    
    void Start()
    {
        moveAllowed=false;
    }

    // Update is called once per frame
    void Update()
    {
        handleTouchEvents();
    }



    void handleTouchEvents()
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
    
            pointA = touchPos;
            pointB = touchPos;

            moveAllowed = true;


            Debug.Log("begin");
    
            break;
        case TouchPhase.Moved:

        pointB = touchPos;
            if ( moveAllowed)
            {
                    
                    
                Debug.Log("moved");
            }
            
            break;
        case TouchPhase.Ended:
            
            pointA = touchPos;
            pointB = touchPos;
            moveAllowed = false;
            
            break;
        }


    }


    void FixedUpdate()
    {
        doMove();
        
    }

    void doMove()
    {
        Vector2 offset=pointB-pointA;
        direction = Vector2.ClampMagnitude(offset,1.0f);


        // set the position of the outer part
        outerJoystick.position = new Vector2(pointA.x, pointA.y);

        // set the position of the inner part
        transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

        // disable the gameobjects if the screen is not being touched
        outerJoystick.GetComponent<SpriteRenderer>().enabled = moveAllowed;
        GetComponent<SpriteRenderer>().enabled = moveAllowed;

    }
}
