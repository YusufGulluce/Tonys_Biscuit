using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    [SerializeField]
    private Image dieBG;
    [SerializeField]
    private Image dieImage;
    [SerializeField]
    private Button reButton;
    [SerializeField]
    private Button mMButton;
    [SerializeField]
    private Scene mainMenu;

    [SerializeField]
    private Mouse tony;

    private float BGtimer = 0f;
    private float ImgTimer = 0f;

    private Color tempColor;


    private void Start()
    {
        dieBG.enabled       = false;
        dieImage.enabled    = false;
        reButton.enabled    = false;
        mMButton.enabled    = false;
        mMButton.GetComponent<Image>().enabled = false;

    }

    public void DieScreen()
    {
        Debug.Log("diedd");

        tempColor = Color.white;
        tempColor.a = 0f;
        dieImage.color = tempColor;

        tempColor = dieBG.color;
        tempColor.a = 0f;
        dieBG.color = tempColor;

        dieBG.enabled       = true;
        dieImage.enabled    = true;
        reButton.enabled    = true;
        mMButton.enabled    = true;
        mMButton.GetComponent<Image>().enabled = true;


        BGtimer = 1f;

        Destroy(tony);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainScreen()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(BGtimer > 0f)
        {
            BGtimer -= Time.deltaTime;
            float time = 1f - BGtimer;

            tempColor.a = Mathf.Sin(Mathf.PI * time * .5f) * .95f;

            dieBG.color = tempColor;
            if (BGtimer <= 0f)
            {
                ImgTimer = 0.1f;
                tempColor = Color.white;
            }

        }
        else if(ImgTimer > 0f)
        {
            ImgTimer -= Time.deltaTime;
            float time = 1f - ImgTimer;

            tempColor.a = Mathf.Sin(Mathf.PI * time * .5f);

            dieImage.color = tempColor;
            if (ImgTimer <= 0f)
                ImgTimer = 0f;

        }
    }

    public void Win()
    {
        SceneManager.LoadScene(2);
    }
}
