using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private float volume;

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    public float zPos = 10;

    float startT = 0;
    float endT = 0;
    float depth = 0;
    float focusTimer = 0f;
    float oldZPos = 0f;

    [SerializeField]
    private float shakeSpeed = 0f;

    [SerializeField]
    private bool startShake = false;
    [SerializeField]
    private float testShakeTimer = 4f;
    [SerializeField]
    private float testShakePower = 3.1f;
    [SerializeField]
    private float testShakeFreq = 0.2f;

    Vector3 shakeOffset = Vector2.zero;
    float shakeTimer = 0;
    float shakeMiniTimer = 0f;
    float power = 0;
    float freq = 0f;
    float oldSmoothSpeed = 0f;

    private void Start()
    {
        //Shake(4f, 3.1f, 0.2f);
    }

    void FixedUpdate()
    {
        if (startShake)
        {
            startShake = false;
            Shake(testShakeTimer, testShakePower, testShakeFreq);
        }

        Vector3 desiredPosition = target.position + locationOffset + shakeOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, zPos);

        if(depth != 0f)
        {
            if(startT > 0f)
            {
                float zoom = depth * (Mathf.Cos((focusTimer / startT) * Mathf.PI) + 1) * .5f;

                zPos = oldZPos - zoom;

                focusTimer -= Time.deltaTime;

                if (focusTimer < 0f)
                {
                    startT = 0f;
                    focusTimer = endT;
                }
            }
            else
            {
                float zoom = depth * (Mathf.Sin(((focusTimer - endT * .5f) / endT) * Mathf.PI) + 1) * .5f;

                zPos = oldZPos - zoom;

                focusTimer -= Time.deltaTime;

                if (focusTimer < 0f)
                {
                    zPos = oldZPos;
                    endT = 0f;
                    depth = 0f;
                }
            }
        }

        if(power > 0f)
        {


            shakeMiniTimer -= Time.deltaTime;
            shakeTimer -= Time.deltaTime;

            if(shakeMiniTimer <= 0f)
            {
                shakeMiniTimer = freq;
                shakeOffset *= -1;


                shakeOffset.y = Random.Range(-power, power);
            }

            if(shakeTimer <= 0f)
            {
                smoothSpeed = 0.1f;
                freq = 0f;
                shakeTimer = 0f;
                shakeMiniTimer = 0f;
                power = 0f;
                shakeOffset = Vector3.zero;
            }
        }
    }

    public void Shake(float timer, float power, float freq)
    {
        shakeTimer = timer;
        this.power = power;
        this.freq = freq;
        shakeMiniTimer = freq;
        oldSmoothSpeed = smoothSpeed;
        smoothSpeed = shakeSpeed;

        shakeOffset = Vector3.right * power;
    }

    public void Focus(float startT, float endT, float depth)
    {
        this.startT = startT;
        this.endT   = endT;
        this.depth  = depth;
        focusTimer = startT;
        oldZPos = zPos;
    }
}