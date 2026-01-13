using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform mainCamera;
    [Header("Movement Setting")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float turingSpeed = 1f;

    [Header("Input")]
    private float inputX;
    private float inputY;
    private Vector3 moveDir;
   private InputManager inputManager;

    private void Start()
    {   
        inputManager = FindAnyObjectByType<InputManager>();
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        GetInput();
        GroundMovement();
    }

    private void Movement()
    {
        GroundMovement();
    }

    private void GroundMovement()

    {
        moveDir = new Vector3(inputX, 0f, inputY) * walkSpeed;
        controller.Move(moveDir * Time.deltaTime);

    }


    private void GetInput()
    {
        inputX = inputManager.playerMoveDir.x;
        inputY = inputManager.playerMoveDir.y;
    }

}
