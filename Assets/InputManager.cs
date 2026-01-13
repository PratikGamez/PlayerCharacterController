using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{

    public Vector2 playerMoveDir {get; private set;}
    public bool jumpInput { get; private set;}

    private PlayerControl inputActions;

    private void Start()
    {
       inputActions = new PlayerControl();    
    }

    public void OnMovement(InputAction.CallbackContext  callbackContext)
    {   
        if(callbackContext.performed)
        {
          playerMoveDir = callbackContext.ReadValue<Vector2>();
          Debug.Log("moved!");

        }
    }

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {

            jumpInput = true;
         Debug.Log("Jumped");             
        }
        if(callbackContext.canceled)
        {
           jumpInput = false; 
        }
    }
}
