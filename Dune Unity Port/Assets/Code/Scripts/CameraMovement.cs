using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    public Transform player;
    private CharacterController controller;
    public float angle = 45f;
    public float rotationSpeed = 0.5f;
    RaycastHit hit;
    public float smoothSpeed = 0.5f;
    Vector3 movement;

    //Zoom
    private float targetZoom;
    [SerializeField] private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;


    public float moveSpeed = 5f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        targetZoom = cam.fieldOfView;
        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        MoveCamera();
    }
    public void Zoom()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 15f, 80f);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, Time.deltaTime * zoomLerpSpeed);
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
