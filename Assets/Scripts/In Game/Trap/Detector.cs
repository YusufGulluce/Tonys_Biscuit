using UnityEngine;
using System.Collections;
using System;

public class Detector : MonoBehaviour
{
    [SerializeField]
    private ForShadow trigger;
    [SerializeField]
    private float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            trigger.Init(timer);
            Destroy(gameObject);
        }

    }
}
