using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform rect;
    private Image image;

    private float rightest;
    private float leftest;
    private float gap;
    private float top;

    private float dashCooldown;
    private float timer = 999f;

    public void Set(float dashCooldown)
    {
        image = rect.GetComponent<Image>();

        this.dashCooldown = dashCooldown;
        rightest = rect.anchorMax.x;
        leftest = rect.anchorMin.x;
        gap = rightest - leftest;
        top = rect.anchorMax.y;

        timer = dashCooldown;
    }

    public void Trigger()
    {
        image.color = Color.gray;
        rect.anchorMax = new Vector2(0, top);
        timer = 0f;
    }

    private void Update()
    {
        if(timer < dashCooldown)
        {
            image.color = Color.white;
            timer += Time.deltaTime;
            rect.anchorMax = new Vector2((timer / dashCooldown) * gap + leftest, top);
        }
    }
}
