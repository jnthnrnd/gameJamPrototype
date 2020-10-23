using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10f; //<-- higher value, the faster. the smaller value, the slower
    public Vector3 cameraOffset;

    void LateUpdate()
    {
        Vector3 desiredPos = target.position + cameraOffset;

        /*Lerp - Linear interpolation
            process of smoothly going from point A  to point B.
            point A - current position (starting point)
            point B - desired position (end point)
            
            float t = often want to Lerp over time. 't' is any value from 0 to 1
            if 0 = giving the starting position
            if 1 = giving the end point
            anyvalue between 0 and 1 will give mix of Point A and Point B
        */
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        /* Good idea to multiply smoothSpeed with Time.deltaTime to make smoothing occur at the same speed
         * no matter the frame rate. if doing this, make sure to increase smoothSpeed to a higher value like 10f. 
         */
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }

}
