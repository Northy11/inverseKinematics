using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followspeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform followTarget;
    private Rigidbody body;
    // Start is called before the first frame update
    private void Start()
    {
        //Physics Movement
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        //Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }

    // Update is called once per frame
   private void Update()
    {

        
    }
    private void PhysicsMove()
    {
        //Position

        var positionWithOffest = followTarget.TransformPoint(positionOffset); 
        var distance = Vector3.Distance(positionWithOffest, transform.position);
        body.velocity = (positionWithOffest - transform.position).normalized * (followspeed * distance);

        //Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);

    }
}
