using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickMove : MonoBehaviour
{
    
    public bool IsButtonPressed()
    {
        return buttonPressed;

    }

    private Vector2 direction;     // joystick direction as vector2
    private Vector2 direction8;        // x and y joystick direction for 8 way control (-1,0,1)

    public float joystickX = -25;   //position of joystick on screen
    public float joystickY = -10;

    private bool buttonPressed;

    private Vector2 pointA, pointB;
    private bool touchStart=false;
    private bool moveAllowed;

    public GameObject player;
    public Transform outerJoystick;
    public Transform innerJoystick;
    public SpriteRenderer fireButton;   // button gameobject
    public Sprite buttonUp;             // button up sprite
    public Sprite buttonDown;           // button down sprite

    

    // Start is called before the first frame update
    void Start()
    {
        moveAllowed=false;
    }

    // Update is called once per frame
    

    void Update()
    {
        HandleTouchEvents();
        Do8Way();


    }



    void HandleTouchEvents()
    {
        buttonPressed=false;

        // the touch is handled in two ways
        // native touch = finger presses for a mobile build
        // simulated touch = using mouse presses and mouse pointer for testing with a windows build


        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, touch.position, touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)  // if there are no finger presses handle via mouse pointer
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 50.0f; 

            // button pressed for first time
            if (Input.GetMouseButtonDown(0) ) {
                HandleTouch(10, mousePos, TouchPhase.Began);
            }

            // button held down
            if (Input.GetMouseButton(0) ) {
                HandleTouch(10, mousePos, TouchPhase.Moved);
            }

            // button released
            if (Input.GetMouseButtonUp(0) ) {
                HandleTouch(10, mousePos, TouchPhase.Ended);
            }
        }

    }

     private void HandleTouch(int touchFingerId, Vector3 touchPos, TouchPhase touchPhase)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint( touchPos );

            // get xpos of touch on screen, range 0 to 1
            float xpos = touchPos.x/Camera.main.pixelRect.width;
            
            // if xpos is on left side (0-0.7) it is a jostick press, otherwise it's a button press
            if( xpos > 0.7f )
            {
                // finger is outside of joystick zone, so it must be a fire button press
                buttonPressed=true;
                moveAllowed=false;
                pointA = worldPos;
                pointB = worldPos;
                return;
            }

        
        switch (touchPhase)
        {
        case TouchPhase.Began:
            pointA = worldPos;
            pointB = worldPos;
            moveAllowed = true;
        break;

        case TouchPhase.Moved:
            if ( moveAllowed)
            {
                pointB = worldPos;
            }
            
        break;

        case TouchPhase.Ended:
            pointA = worldPos;
            pointB = worldPos;
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
        // set the button up or down sprite
        if( buttonPressed )
        {
               fireButton.sprite = buttonDown;
        }
        else
        {
               fireButton.sprite = buttonUp;
        }

        
        // clamp the vector2 length
        Vector2 offset=pointB-pointA;
        direction = Vector2.ClampMagnitude(offset,1.0f);


        // draw the inner and outer sprites at the desired location

        // set the position of the outer part
        outerJoystick.position = new Vector2(pointA.x, pointA.y);

        // set the position of the inner part
        innerJoystick.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);

        // disable the gameobjects if the screen is not being touched
        outerJoystick.GetComponent<SpriteRenderer>().enabled = moveAllowed;
        innerJoystick.GetComponent<SpriteRenderer>().enabled = moveAllowed;

    }



    // Get direction angle and convert to 8 way 
    void Do8Way()
    {
        float inpAngle;    //The input angle in degrees
        float outAngle;    //The output angle in degrees, to pass to the player character
 
        Vector2 p1, p2;

        p1.x = pointA.x;
        p1.y = pointA.y;
        p2.x = pointA.x;
        p2.y = 100;

        Vector2 v2 = pointB - pointA;
        
        // get angle between two vectors
        inpAngle = Vector2.SignedAngle(p1-p2,v2);
        outAngle = (Mathf.Round(inpAngle / 45.0f) * 45.0f)/45;
       
       // get left/right/up/down based on outAngle value of -4 to +3
       direction8.x=direction8.y=0;
        if( outAngle == -1 || outAngle == -2 || outAngle == -3 )
            direction8.x=-1;

        if( outAngle == 1 || outAngle == 2 || outAngle == 3 )
            direction8.x=1;

        if( outAngle == -3 || outAngle == -4 || outAngle == 3 || outAngle==4 )
            direction8.y=1;

        if( outAngle == -1 || outAngle == 0 || outAngle == 1  )
            direction8.y=-1;

        if( moveAllowed == false || pointA == pointB )
            direction8.y=0;

        //direction8.x contains -1, 0 or 1 for left/right            
        //direction8.y contains -1, 0 or 1 for up/down 
    }

    public Vector2 GetDirection8()
    {
        return direction8;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }



}

