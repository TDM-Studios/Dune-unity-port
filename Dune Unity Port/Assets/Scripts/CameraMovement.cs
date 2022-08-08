using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    private CharacterController controller;
    public float angle = 45f;
    public float rotationSpeed = 0.5f;
    RaycastHit hit;
    public float smoothSpeed = 0.5f;
    Vector3 movement;

    public float moveSpeed = 5f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("ZoomIn"))
        {
            float frustumHeight = transform.localScale.y;
            float distance = (float)(frustumHeight * 0.5 / Mathf.Tan((float)(Camera.main.fieldOfView * 0.5 * Mathf.Deg2Rad)));

            // Since front side of the block is not at pivot
            distance += (float)(transform.localScale.z * 0.5);
            Camera.main.transform.position = Vector3.back * distance;
        }
        if (Input.GetButton("ZoomOut"))
        {
            Camera.main.fieldOfView -= Time.deltaTime * 50;
        }
        MoveCamera();
    }
    
    protected virtual void MoveCamera()
    {
        Vector3 fwd = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 move = transform.right * movement.x + fwd * movement.z;
        controller.Move(move * Time.fixedDeltaTime * moveSpeed);

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        if(Physics.Raycast(transform.position, transform.forward,out hit, 1000f))
        {
            if (Input.GetButton("RotateCameraQ"))
                transform.RotateAround(hit.point, angle * Vector3.up, angle);
            if (Input.GetButton("RotateCameraE"))
                transform.RotateAround(hit.point, -angle * Vector3.up, angle);
        }
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
