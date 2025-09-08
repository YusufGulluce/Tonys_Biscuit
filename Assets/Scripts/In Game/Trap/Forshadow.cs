using UnityEngine;
using System.Collections;
using System;

public class ForShadow : MonoBehaviour
{
    [SerializeField]
    public PathTraps trap;
    public static float timerMult = 30f;
    public float timer = 0;



    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        this.enabled = false;
    }
    public void Init(float timer)
    {
        this.timer = timer;
        this.enabled = true;
    }

    private void Update()
    {
        if(timer <= 0f)
        {
            trap.Init();
            Destroy(gameObject);
        }
        else
        {
            float a = (Mathf.Sin(timerMult * timer) + 1) * .5f;
            a *= a;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
            timer -= Time.deltaTime;
        }    

    }

    
}
