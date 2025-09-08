using UnityEngine;
using System.Collections;

public class AnimationMono : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;
    [SerializeField]
    private float fps;

    private SpriteRenderer sr;

    private int index;
    private float framePeriod;
    private float timer = 0f;
    // Use this for initialization
    void Start()
    {
        index = 0;
        framePeriod = 1 / fps;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= framePeriod)
        {
            timer = 0f;
            index %= images.Length;
            sr.sprite = images[index];
            index++;
        }
    }
}
