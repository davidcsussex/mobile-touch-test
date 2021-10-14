using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public joystickMove joystick; // drag instance of joystick script onto this in editor
    public GameObject bulletPrefab;
    public float xOffset=3.6f;
    public float yOffset=0.36f;
    Rigidbody2D rb;

    bool buttonReleased=true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        rb.velocity = joystick.GetDirection8() * 10;
        rb.velocity = joystick.GetDirection() * 10;
        print("xy=" + rb.velocity.x + "  " + rb.velocity.y);

        if(  joystick.IsButtonPressed() == true )
        {
            
            Vector3 pos;
            pos = transform.position;
            pos.x += xOffset;
            pos.y -= yOffset;

            if( buttonReleased == true )
            {
                Instantiate(bulletPrefab, pos, Quaternion.identity);
                
            }
            buttonReleased=false;
        }
        else
        {
            buttonReleased=true;
        }
        
    }
}
