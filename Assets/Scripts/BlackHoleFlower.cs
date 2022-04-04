using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleFlower : MonoBehaviour, IInteractable
{
    public bool grown = true;

    public GameObject visuals;

    public bool CanInteract()
    {
        return grown;
    }

    public string GetInteractText()
    {
        return "Take";
    }

    public void Interact()
    {
        if (!grown)
            return;


        visuals.SetActive(false);

        grown = false;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().RepairBlackHole();

        StartCoroutine("Grow");
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(10);
        grown = true;
        visuals.SetActive(true);
    }


}
