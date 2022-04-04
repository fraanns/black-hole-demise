using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    public string checkpointName;

    public bool CanInteract()
    {
        return true;
    }

    public string GetInteractText()
    {
        return "Set Checkpoint";
    }

    public void Interact()
    {
        GameObject.Find("MessageText").GetComponent<MessageText>().Message($"Checkpoint {checkpointName} set");

        GameObject.Find("Player").GetComponent<PlayerMovement>().SetCheckpoint();
    }
}
