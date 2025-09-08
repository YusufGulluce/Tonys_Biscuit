using UnityEngine;
using System.Collections;

public class StartScreenButton : MonoBehaviour
{
    [SerializeField]
    private StartScreenController controller;

    private void OnMouseDown()
    {
        controller.PlayButtonPressed();
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }
}
