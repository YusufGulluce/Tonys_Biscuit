using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComicBookShow : MonoBehaviour
{

    [SerializeField]
    private bool openImm = true;
    [SerializeField]
    private StartScreenController controller;
    [SerializeField]
    public SpriteRenderer background;

    [SerializeField]
    List<SpriteRenderer> SRs;

    [SerializeField]
    List<int> headers;

    [SerializeField]
    private float openingTime;

    [SerializeField]
    private float endingTime;

    private int pageNo;

    
    private SpriteRenderer sr;
    private float timer;
    private bool opening;


    private void Start()
    {
        opening = false;
        sr = null;
        timer = 0f;

        background.enabled = false;
        foreach (SpriteRenderer sr in SRs)
            sr.enabled = false;

        if(openImm)
            Init();

    }

    public void Init()
    {
        Debug.Log("does it work???");
        background.enabled = true;
        if (background.enabled)
            Debug.Log("olması lazım amk");
        
        pageNo = 0;
        ShowNext();
    }

    private void Update()
    {
        //if (background.enabled == false)
        //    background.enabled = true;
        if (Input.anyKeyDown)
            ShowNext();

        if(sr != null)
        {
            float a = 0;

            if (timer <= 0f)
            {
                timer = 0f;
                Color color = sr.color;
                color.a = 1f;
                sr.color = color;
                sr = null;
            }
            else
            {
                timer -= Time.deltaTime;

                if (opening)
                    a = Mathf.Cos(timer * Mathf.PI / openingTime) + 1;

                else
                    a = Mathf.Sin((timer - openingTime * .5f) * Mathf.PI / openingTime) + 1;

                a *= .5f;

                Color color = sr.color;
                color.a = a;
                sr.color = color;
            }

        }
    }

    private void ShowNext()
    {
        if(timer > 0f)
        {
            Debug.Log("pushed eraly buddy");
            timer = 0f;
            Color color = sr.color;
            color.a = 1f;
            sr.color = color;
            sr = null;
        }
        else
        {
            if (pageNo >= SRs.Count || headers != null && headers.Count != 0 && pageNo == headers[0])
            {
                Debug.Log("wait.. deleting others");
                for (int i = 0; i < pageNo; ++i)
                    SRs[i].enabled = false;
                if(headers.Count > 0)
                    headers.RemoveAt(0);
            }
            if (pageNo >= SRs.Count)
                Finish();
            else
                FadeOpen(SRs[pageNo]);
            pageNo++;
        }


            
    }

    private void FadeOpen(SpriteRenderer sr)
    {
        sr.enabled = true;
        timer = openingTime;
        opening = true;
        this.sr = sr;

        Debug.Log("Fade open");
    }

    private void FadeClose(SpriteRenderer sr)
    {
        timer = endingTime;
        opening = false;
        this.sr = sr;
    }

    private void Finish()
    {
        background.enabled = false;
        if(controller != null)
            controller.ComicShowEnded();
        enabled = false;
    }


}
