using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    private PlayerControl playerControl;

    private void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }


    public void OnLeftDown()
    {
        playerControl?.HorizontalInput(-1f);
        Debug.Log(playerControl);
    }

    public void OnLeftUp()
    {
        playerControl?.HorizontalInput(0f);
    }

    public void OnRightDown()
    {
        playerControl?.HorizontalInput(1f);
    }

    public void OnRightUp()
    {
        playerControl?.HorizontalInput(0f);
    }

    public void OnJump()
    {
        playerControl?.Jump();
    }

}
