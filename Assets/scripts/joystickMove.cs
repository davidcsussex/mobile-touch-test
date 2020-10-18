using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickMove : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 pointA, pointB;
    private bool touchStart=false;
    private bool moveAllowed;

    float deltaX, deltaY;

    Collider2D col;
    Rigidbody2D rb;


    public Transform outerJoystick;
    public Transform innerJoystick;

    float speed = 5;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
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
        Vector2 direction = Vector2.ClampMagnitude(offset,1.0f);


        transform.Translate(direction*Time.deltaTime*speed);

        // set the position of the outer part
        outerJoystick.position = new Vector2(pointA.x, pointA.y);

        // set the position of the inner part
        innerJoystick.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

        // disable the gameobjects if the screen is not being touched
        outerJoystick.GetComponent<SpriteRenderer>().enabled = moveAllowed;
        innerJoystick.GetComponent<SpriteRenderer>().enabled = moveAllowed;

    }
}
