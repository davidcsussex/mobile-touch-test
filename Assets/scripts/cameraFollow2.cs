using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    void Start()
    {
        offset=new Vector3( 0, 15, -40 );
        smoothSpeed = 1.6f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position+offset;

        

        Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;
        
        
    }
}
