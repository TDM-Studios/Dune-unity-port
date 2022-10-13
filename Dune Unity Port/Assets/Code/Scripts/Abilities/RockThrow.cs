using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    GameObject owner;
    Vector3 destination;
    public AbilitySystem player1;
    Rigidbody rb;
    SphereCollider soundArea;

    // Counter to dissapear
    bool groundHit = false;
    float count = 0f;
    float maxTime = 10f;


    void Awake()
    {
        player1 = GameObject.Find("Player 1").GetComponent<AbilitySystem>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        owner = player1.gameObject;
        destination = player1.hit.point;

        this.transform.LookAt(destination, Vector3.up);
        ThrowToPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(groundHit)
        {
            count += Time.deltaTime;
            if(count > maxTime)
            {
                count = 0f;
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {
            soundArea = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            soundArea.isTrigger = true;
            soundArea.radius = 50f;

            rb.isKinematic = true;

            groundHit = true;
        }
    }

    private void ThrowToPoint()
    {
        float distX = Mathf.Abs((destination.x - owner.transform.position.x));
        float distY = destination.y - owner.transform.position.y;
        float distZ = Mathf.Abs((destination.z - owner.transform.position.z));

        Vector2 distXZ = new Vector2(distX, distZ);
        float norm = distXZ.magnitude;

        float velocity = 15f;

        float insideRoot = Mathf.Abs(Mathf.Pow(velocity, 4) - (Physics.gravity.y * (Physics.gravity.y * Mathf.Pow(norm, 2)) + (2 * distY * Mathf.Pow(velocity, 2))));
        float tan = (Mathf.Pow(velocity, 2) + Mathf.Sqrt(insideRoot)) / (Physics.gravity.y * norm);
        float angle = Mathf.Atan(Mathf.Abs(tan));

        float velY = velocity * Mathf.Cos(angle);
        float velZ = velocity * Mathf.Sin(angle);

        Vector3 launchVelocity = new Vector3(0f, velY, velZ);

        launchVelocity = rb.transform.TransformDirection(launchVelocity);
        rb.AddForce(launchVelocity, ForceMode.Impulse);
    }
}
