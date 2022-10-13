using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //public Transform player;
    public Rigidbody rb;
    public float height = 10f;
    public float distance = 20f;
    public float angle = 45f;
    public float rotationSpeed = 0.5f;
    public float smoothSpeed = 0.5f;
    Vector3 movement;

    private Vector3 refVelocity;
    public Camera camera;
    public float moveSpeed = 5f;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("RotateCameraQ"))
            angle += rotationSpeed;
        if (Input.GetButton("RotateCameraE"))
            angle -= rotationSpeed;
        if (Input.GetButton("ZoomIn"))
        {
            camera.fieldOfView--;
        }
        if (Input.GetButton("ZoomOut"))
        {
            camera.fieldOfView++;
        }
        MoveCamera();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    protected virtual void MoveCamera()
    {
        //if (!player)
        //    return;
        //Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);

        //Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

        //Vector3 flatTargetPos = player.position;
        //flatTargetPos.y = 0f;
        //Vector3 finalPos = flatTargetPos + rotatedVector;

        //transform.position = Vector3.SmoothDamp(transform.position,finalPos, ref refVelocity,smoothSpeed);
        //transform.LookAt(flatTargetPos);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
