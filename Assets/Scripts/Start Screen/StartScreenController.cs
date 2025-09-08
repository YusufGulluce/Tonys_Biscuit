using UnityEngine;
using System.Collections;

public class StartScreenController : MonoBehaviour
{
    [SerializeField]
    private ComicBookShow comic;

    [SerializeField]
    private SpriteRenderer characterSelectBG;
    [SerializeField]
    private Collider2D[] characters;
    private SpriteRenderer[] charSRs;
    private void Start()
    {
        charSRs = new SpriteRenderer[characters.Length];
        for (int i = 0; i < characters.Length; ++i)
            charSRs[i] = characters[i].GetComponent<SpriteRenderer>();

        characterSelectBG.enabled = false;
        foreach (SpriteRenderer sr in charSRs)
            sr.enabled = false;
        foreach (Collider2D c in characters)
            c.enabled = false;
    }

    public void PlayButtonPressed()
    {
        comic.Init();
    }

    public void ComicShowEnded()
    {
        characterSelectBG.enabled = true;
        foreach (SpriteRenderer sr in charSRs)
            sr.enabled = true;
        foreach (Collider2D c in characters)
            c.enabled = true;
    }

    public void CharacterSelected(int index)
    {
        Mouse.type = index;

        foreach (Collider2D c in characters)
            c.enabled = false;

        characterSelectBG.color = Color.gray;

    }

}
