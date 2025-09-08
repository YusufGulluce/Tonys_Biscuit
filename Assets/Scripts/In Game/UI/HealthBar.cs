using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform top;
    [SerializeField]
    private RectTransform mid;
    [SerializeField]
    private RectTransform bot;

    private float maxHP;

    private float timer;
    private float followTimer;

    private float rightest;
    private float leftest;
    private float currRight;
    private float newRight;

    public void Set(float maxHP)
    {
        this.maxHP = maxHP;

        timer = 0f;
        followTimer = 0f;
        rightest = top.anchorMax.x;
        leftest = top.anchorMin.x;
        currRight = rightest;
        newRight = currRight;
    }

    public void UpdateHP(float HP)
    {
        newRight = (HP / maxHP) * (rightest - leftest) + leftest;

        top.anchorMax = new Vector2(newRight, top.anchorMax.y);
    }

    private void Update()
    {
        float b = Mathf.Lerp(top.anchorMax.x, mid.anchorMax.x, .98f);

        //top.anchorMax = new Vector2(a, top.anchorMax.y);
        mid.anchorMax = new Vector2(b, mid.anchorMax.y);
    }


}
