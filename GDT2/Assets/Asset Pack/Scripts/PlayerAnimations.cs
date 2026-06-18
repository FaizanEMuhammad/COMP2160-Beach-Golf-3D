using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    //Setup animator
    private Animator animator;
    //Player action Setup
    private PlayerActions playerActions;
    private InputAction walkAction;
    private InputAction sprintAction;
    private InputAction kickAction;
    private InputAction grabAction;
  
    void Awake()
    {
        playerActions = new PlayerActions();
        walkAction = playerActions.Movement.Walk;
        sprintAction = playerActions.Movement.Sprint;
        kickAction = playerActions.Movement.Kick;
        grabAction = playerActions.Movement.Grab;
        walkAction.performed += OnWalk;
        walkAction.canceled += DoIdle;
        sprintAction.performed += OnSprint;
        sprintAction.canceled += DoIdle;
        grabAction.performed += OnGrab;
        grabAction.canceled += OnKick;
    }

    void OnEnable()
    {
        walkAction.Enable();
        sprintAction.Enable();
        kickAction.Enable();
        grabAction.Enable();
    }

    void OnDisable()
    {
        walkAction.Disable();
        sprintAction.Disable();
        kickAction.Disable();
        grabAction.Disable();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnWalk(InputAction.CallbackContext context)
    {
        animator.SetInteger("Action", 1);
    }
    void OnSprint(InputAction.CallbackContext context)
    {
        animator.SetInteger("Action", 2);
    }
    void OnGrab(InputAction.CallbackContext context)
    {
        animator.SetInteger("Action", 3);
    }
    void OnKick(InputAction.CallbackContext context)
    {
        animator.SetInteger("Action", 4);
    }
    void DoIdle(InputAction.CallbackContext context)
    {
        animator.SetInteger("Action", 0);
    }
}
