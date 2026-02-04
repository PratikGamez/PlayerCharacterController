using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;
    [Header("Movement Setting")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float turingSpeed = 1f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpHeight = 3f;


    private float verticalVelocity;

    [Header("Input")]
    private float inputX;
    private float inputY;
    private bool jumpInput;
    private Vector3 moveDir;
   private InputManager inputManager;

    private void Start()
    {   
        inputManager = FindAnyObjectByType<InputManager>();
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
       jumpInput = inputManager.jumpInput;

        GetInput();
        Movement();
    }

    private void Movement()
    {
        GroundMovement();
        Turn();
    }
    

    private void GroundMovement()

    {
        moveDir = new Vector3(inputX, 0f, inputY);
        moveDir = camera.transform.TransformDirection(moveDir);
        
        moveDir *= walkSpeed;

        moveDir.y = VerticalForceCalculation();
        controller.Move(moveDir * Time.deltaTime);
    }
    
    private void Turn()
    {
        if(Mathf.Abs(inputX) > 0 ||  Mathf.Abs(inputY) > 0)
        {
          Vector3 currentLookDir = controller.velocity.normalized;
          currentLookDir.y = 0;

          currentLookDir.Normalize();

          Quaternion targetRotation = Quaternion.LookRotation(currentLookDir);
          transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turingSpeed);    
        }   
    }

    private float VerticalForceCalculation()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = -1f;
            if(jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        
        return verticalVelocity;
    }


    private void GetInput()
    {
        inputX = inputManager.playerMoveDir.x;
        inputY = inputManager.playerMoveDir.y;
    }

}
