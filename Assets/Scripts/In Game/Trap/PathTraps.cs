using UnityEngine;
using System.Collections;
using UnityEngine.Splines;

public class PathTraps : HarmfullObjects
{
    [SerializeField]
    private bool loseEffect = false;
    [SerializeField]
    private bool loop;
    [SerializeField]
    private float gravity = 0f;
    [SerializeField]
    private bool turnSolid = false;
    [SerializeField]
    private bool tpToPath = true;

    [SerializeField]
    private bool followAngle = false;

    [SerializeField]
    private SplineContainer path;

    [SerializeField]
    private float firstLinearVelocity;
    private float linearVelocity;

    [SerializeField]
    private float angularVelocity;

    private float percentage = 1f;
    private float timer = 0f;
    private float plank;


    private void Start()
    {
        Starting();
    }
    public void Starting()
    {
        linearVelocity = firstLinearVelocity;
        percentage = linearVelocity / path.Spline.GetLength();
        enabled = false;

        plank = 0.01f / path.Spline.GetLength();
        if (tpToPath)
            transform.position = (Vector3)path.Spline.EvaluatePosition(0f) + path.transform.position;

    }

    public void Init()
    {
        enabled = true;
    }

    private void Update()
    {
        if (percentage * timer < 1f)
        {
            timer += Time.deltaTime;

            if(gravity > 0f)
            {
                float yGap = (path.Spline.EvaluatePosition(timer * percentage) - path.Spline.EvaluatePosition(timer * percentage - plank)).y;
                //Debug.Log("yGap = " + yGap);
                linearVelocity -= yGap * gravity;
                percentage = linearVelocity / path.Spline.GetLength();
            }

            transform.position = (Vector3)path.Spline.EvaluatePosition(timer * percentage) + path.transform.position;

            if (followAngle)
            {
                Vector3 forward = path.EvaluateTangent(timer * percentage);
                Vector3 up = path.EvaluateUpVector(timer * percentage);
                Vector3 right = Vector3.Cross(forward, up).normalized;
                transform.up = right;
            }
            else
                transform.Rotate(Vector3.forward * angularVelocity * Time.deltaTime);
        }
        else
        {
            if (loop)
            {
                timer = 0;
                linearVelocity = firstLinearVelocity;
            }
            else
            {
                //enabled = false;
                if (loseEffect)
                    gameObject.layer = 15;
                if(turnSolid)
                    GetComponent<Collider2D>().isTrigger = false;
                enabled = false;

            }
        }

    }



}
