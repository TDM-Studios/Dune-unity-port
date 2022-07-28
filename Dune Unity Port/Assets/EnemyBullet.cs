using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector3 shootDir;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += this.shootDir * this.bulletSpeed * Time.deltaTime;
        transform.forward = this.shootDir;
    }

    public void SetUp(Vector3 dir)
    {
        this.shootDir = dir;
    }
}
