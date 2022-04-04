using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, GameInput.IGameplayActions
{
    public static InputReader input;

    private GameInput gameInput;

    public event UnityAction jumpStartEvent = delegate { };
    public event UnityAction jumpEndEvent = delegate { };

    public event UnityAction suckStartEvent = delegate { };
    public event UnityAction suckEndEvent = delegate { };

    public event UnityAction interactEvent = delegate { };

    public event UnityAction restartEvent = delegate { };

    [HideInInspector]
    public Vector2 moveDirection;

    [HideInInspector]
    public Vector2 suck;

    void Awake()
    {
        input = this;

        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
        }

        gameInput.Gameplay.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnJumpStart(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            jumpStartEvent();
    }

    public void OnJumpEnd(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            jumpEndEvent();
    }

    public void OnSuck(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            suckStartEvent();
        else if (context.phase == InputActionPhase.Canceled)
            suckEndEvent();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            interactEvent();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            restartEvent();
    }
}
