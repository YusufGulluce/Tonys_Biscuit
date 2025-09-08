using UnityEngine;
using System.Collections;

public class zAxisTraps : PathTraps
{
    private bool itIs;
    private void Start()
    {
        Starting();

        GetComponent<Collider2D>().enabled = false;
    }

    private void FixedUpdate()
    {
        if(transform.position.z > -4f && transform.position.z < 4f)
            GetComponent<Collider2D>().enabled = true;
        
        else
            GetComponent<Collider2D>().enabled = false;

    }
}
