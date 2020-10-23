using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purpleThing : MonoBehaviour
{
    // Start is called before the first frame update

    joystickMove script;
    float speed = 5;

    void Start()
    {
        GameObject joystick = GameObject.Find("joystickInner");
        script = joystick.GetComponent<joystickMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(script.direction*Time.deltaTime*speed);
    }
}
