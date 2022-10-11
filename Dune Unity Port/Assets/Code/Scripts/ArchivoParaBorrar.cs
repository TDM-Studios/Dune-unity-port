using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchivoParaBorrar : MonoBehaviour
{
    public EnemyContoller a;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            a.pendingToDelete = true;
        }
    }
}
