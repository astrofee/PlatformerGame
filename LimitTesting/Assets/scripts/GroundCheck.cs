using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerBehaviour player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            player.ToggleGrounded();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            player.ToggleGrounded();
        }
    }
}
