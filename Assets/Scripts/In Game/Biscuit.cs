using UnityEngine;
using System.Collections;
using UnityEngine.Splines;

public class Biscuit : MonoBehaviour
{
    [SerializeField]
    private StageController controller;
    [SerializeField]
    private SplineContainer path;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float angularSpeed;

    private float percentage;
    private float timer;
    // Use this for initialization
    void Start()
    {
        percentage = speed / path.Spline.GetLength();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer * percentage < 1f)
        {
            timer += Time.deltaTime;
            transform.position = (Vector3)path.EvaluatePosition(timer * percentage) + path.transform.position;
            transform.Rotate(angularSpeed * Time.deltaTime * Vector3.forward);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
            controller.Win();
    }
}
