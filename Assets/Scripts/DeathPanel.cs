using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public void Respawn() 
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Respawn();
    }
}
