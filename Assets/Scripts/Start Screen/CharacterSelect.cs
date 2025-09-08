using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    private StartScreenController controller;

    [SerializeField]
    SpriteRenderer nameTag;

    [SerializeField]
    private int code;

    private SpriteRenderer sr;
    private Color fadedColor;
    private Color aliveColor;



    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        aliveColor = sr.color;
        fadedColor = aliveColor;
        fadedColor.a = .7f;

        sr.color = fadedColor;
        nameTag.enabled = false;
    }
    private void OnMouseDown()
    {
        controller.CharacterSelected(code);

        SceneManager.LoadScene(1);
    }

    private void OnMouseEnter()
    {
        sr.color = aliveColor;
        nameTag.enabled = true;
    }

    private void OnMouseExit()
    {
        sr.color = fadedColor;
        nameTag.enabled = false;
    }
}
