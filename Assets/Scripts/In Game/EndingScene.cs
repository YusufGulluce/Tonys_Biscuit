using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer image;

    private bool isOk = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isOk)
        {
            Color color = image.color;
            color.a = Mathf.Lerp(image.color.a, 1f, 0.004f);
            image.color = color;
        }
        if(Input.anyKeyDown)
        {
            if(isOk)
                SceneManager.LoadScene(0);
            
            image.color = Color.white;
            isOk = true;
            
        }
    }
}
