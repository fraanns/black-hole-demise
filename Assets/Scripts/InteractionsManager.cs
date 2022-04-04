using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : MonoBehaviour
{
    IInteractable currentInteractable;

    public TMPro.TMP_Text interactPromptText;
    public GameObject interactPrompt;

    InputReader inputReader;

    private void Start()
    {
        inputReader = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputReader>();

        inputReader.interactEvent += OnInteract;
    }

    private void OnInteract()
    {
        if (currentInteractable != null)
            currentInteractable.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            if (!interactable.CanInteract())
                return;

            string interactText = interactable.GetInteractText();

            interactPrompt.SetActive(true);
            interactPromptText.text = $"(E){interactText}";

            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactPrompt.SetActive(false);

        currentInteractable = null;
    }
}
