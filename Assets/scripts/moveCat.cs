using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=sXc8baUK3iY
//https://gist.github.com/sdabet/3bda94676a4674e6e4a0

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
        col = gameObject.GetComponent<CircleCollider2D>();

        if(!Input.gyro.enabled)
         {
             //Input.gyro.enabled = true;
         }
        
    }

    // Update is called once per frame
    void Update()
    {
return;

          // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        

        Vector3 screenPosDepth = Input.mousePosition;
        screenPosDepth.z = 50.0f; 

        
        //Debug.Log("ix=" + Input.mousePosition.x );

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0) )
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(screenPosDepth), TouchPhase.Began);
                
            }
            if (Input.GetMouseButton(0) )
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(screenPosDepth), TouchPhase.Moved);
                
            }
            if (Input.GetMouseButtonUp(0) )
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(screenPosDepth), TouchPhase.Ended);
                
            }
        }


    }

    private void HandleTouch(int touchFingerId, Vector3 touchPos, TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
        case TouchPhase.Began:

        //Debug.Log("col=" + col);

    
             if ( col == Physics2D.OverlapPoint (touchPos)) 
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
            {
                rb.MovePosition (new Vector2 (touchPos.x - deltaX, touchPos.y - deltaY));
                //Debug.Log("tx=" + touchPos.x + "  ty=" + touchPos.y);
            }
            break;
        case TouchPhase.Ended:
            // TODO
            moveAllowed = false;
            break;
        }
    }

}
