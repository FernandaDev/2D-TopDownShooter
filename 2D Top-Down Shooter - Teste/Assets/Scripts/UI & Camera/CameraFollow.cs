using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] float followSpeed;

    private void Awake()
    {
        targetToFollow = targetToFollow ?? FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        if (targetToFollow)
        {
            this.transform.position = Vector3.Lerp(transform.position,
                                                   new Vector3(targetToFollow.position.x, 
                                                               targetToFollow.position.y, 
                                                               this.transform.position.z), 
                                                   followSpeed * Time.deltaTime); 
            
        }
    }
}
