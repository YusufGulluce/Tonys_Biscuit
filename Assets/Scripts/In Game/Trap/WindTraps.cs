using UnityEngine;
using System.Collections;

public class WindTraps : MonoBehaviour
{
    [SerializeField]
    private Vector2 wind;

    [SerializeField]
    private float slow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            collision.gameObject.GetComponent<Mouse>().wind = wind;
            if(slow > 0f)   collision.gameObject.GetComponent<Mouse>().Slow(slow);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13 )
        {
            collision.gameObject.GetComponent<Mouse>().wind = Vector2.zero;
            if(slow > 0f)   collision.gameObject.GetComponent<Mouse>().UnSlow();
        }
    }

}
