using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float wallJumpUpForce;
    [SerializeField] private float wallJumpSideForce;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Reference")]
    [SerializeField] private Transform orientation;
    private PlayerController pm;
    private CharacterController characterController;
    [SerializeField] private GameObject playerCamera;

    private bool isWallRunning = false;
    private string wallName;
    private string lastWallName = "None";

    private void Start()
    {
        pm = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
        wallRunForce = pm.moveSpeed * pm.SpeedMultiplier;

        if (isWallRunning && Input.GetButtonDown("Jump"))
        {
            CheckForWall();
            if (wallRight)
            {
                wallName = "RightWall";
            }
            if (wallLeft)
            {
                wallName = "LeftWall";
            }
            if (lastWallName != wallName)
            {
            }
            WallJump();
            lastWallName = wallName;
        }
        if(isWallRunning)
        {
            if (wallLeft)
            {
                pm.TiltCamera(-15f);
            }
            if (wallRight)
            {
                pm.TiltCamera(15f);
            }
        }
        else
        {
            pm.TiltCamera(0f);
        }
        if (pm.isGrounded)
        {
            // Resetear la velocidad horizontal al aterrizar
            pm._PlayerVelocity.x = 0f;
            pm._PlayerVelocity.z = 0f;

            if (!Input.GetButton("Jump"))
            {
                pm._PlayerVelocity.y = -1f; // pequeña fuerza hacia abajo para mantener contacto
            }
            lastWallName = "None";
        }
    }

    private void FixedUpdate()
    {
        if (isWallRunning)
        {
            WallRunningMovement();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, wallLayer);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, wallLayer);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundLayer);
    }

    private void StateMachine()
    {
        //Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // State 1 - Wallrunning
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            isWallRunning = true;
        }
        else
        {
            pm.gravity = -9.8f;
            isWallRunning = false;
        }
        //Debug.Log(pm.gravity);
    }

    /*private void WallFriction()
    {
        pm.gravity = Mathf.MoveTowards(pm.gravity, -9.8f, 2 * Time.deltaTime);
    }*/


    private void WallRunningMovement()
    {
        // Se desactiva la gravedad manualmente
        pm.gravity = -2.5f;

        // Se calcula la dirección del wall running
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        // Se asegura que la dirección esté alineada a la visión del jugador
        if (Vector3.Dot(wallForward, transform.forward) < 0)
        {
            wallForward = -wallForward;
        }
        // Se aplica movimiento en la dirección del wall run
        Vector3 moveDirection = wallForward * wallRunForce;
        moveDirection.y = pm.gravity;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void WallJump()
    {
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        // Dirección hacia adelante del jugador, proyectada en el plano horizontal
        Vector3 forwardDirection = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        Vector3 inputDirection = orientation.right * horizontalInput;
        //                      inpulso vertical                impulso lateral                       //Empuje desde la pared
        Vector3 jumpDirection = transform.up * wallJumpUpForce + inputDirection * wallJumpSideForce + wallNormal * wallJumpSideForce * 1.5f;
        StartCoroutine(pm.StartWallJump(jumpDirection));
        isWallRunning = false;
    }
}