using UnityEngine;
using System.Collections;
using UnityEngine.Splines;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private SplineContainer path;

    [SerializeField]
    private Transform tony;

    [SerializeField]
    private Transform biscuit;

    [SerializeField]
    private float chaseSpeed;

    [SerializeField]
    private float biscuitSpinSpeed;

    [SerializeField]
    private float gap;

    private Spline spline;
    private float timer;

    // Use this for initialization
    void Start()
    {
        spline = path.Spline;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        tony.position = (Vector3)spline.EvaluatePosition((chaseSpeed * timer) % 1f) + path.transform.position;
        biscuit.position = (Vector3)spline.EvaluatePosition((chaseSpeed * timer + gap) % 1f) + path.transform.position;
        biscuit.Rotate(biscuitSpinSpeed * Time.deltaTime * Vector3.forward);

    }
}
