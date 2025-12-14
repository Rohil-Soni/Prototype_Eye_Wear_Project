using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesTracking : MonoBehaviour
{
    [SerializeField] private float distanceFromFace = 0.4f;
    
    void LateUpdate()
    {
        Transform head = Camera.main.transform;
        Vector3 facePosition = head.position + head.forward * distanceFromFace;
        
        transform.position = Vector3.Lerp(transform.position, facePosition, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, head.rotation, 10f * Time.deltaTime);
    }
}
