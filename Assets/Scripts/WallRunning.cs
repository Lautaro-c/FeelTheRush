using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float wallRunForce;
    [SerializeField] private float maxWallRunTime;
    [SerializeField] private float wallRunTimer;
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

    private bool isWallRunning = false;

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
            /*pm.DoFOV(90f);
            if (wallLeft) 
            { 
                pm.DoTilting(-5f); 
            }
            if (wallLeft)
            {
                pm.DoTilting(5f);
            }*/
            WallJump();
        }
        if (pm.isGrounded) // o tu método de detección de suelo
        {
            // Resetear la velocidad horizontal al aterrizar
            pm._PlayerVelocity.x = 0f;
            pm._PlayerVelocity.z = 0f;

            // Si querés, también podés resetear la vertical si no estás saltando
            if (!Input.GetButton("Jump"))
            {
                pm._PlayerVelocity.y = -1f; // pequeña fuerza hacia abajo para mantener contacto
            }

            //resetear camara
            //pm.DoFOV(80f);
            //pm.DoTilting(0f);
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
            Debug.Log("We are almost wallrunning");
            isWallRunning = true;
        }
        else
        {
            pm.gravity = -9.8f;
            isWallRunning = false;
        }
    }

    private void WallRunningMovement()
    {
        // Desactivamos gravedad manualmente
        pm.gravity = -2.5f;

        // Calculamos la dirección del wall run
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        // Aseguramos que la dirección esté alineada con el movimiento del jugador
        if (Vector3.Dot(wallForward, transform.forward) < 0)
        {
            wallForward = -wallForward;
        }
        // Aplicamos movimiento en la dirección del wall run
        Vector3 moveDirection = wallForward * wallRunForce;
        moveDirection.y = pm.gravity;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void WallJump()
    {
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        // Dirección hacia adelante del jugador, proyectada en el plano horizontal
        Vector3 forwardDirection = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        // Impulso: hacia arriba + en la dirección del jugador + alejándose de la pared
        Vector3 inputDirection = orientation.right * horizontalInput;
        Vector3 jumpDirection = transform.up * wallJumpUpForce
                              + inputDirection * wallJumpSideForce
                              + wallNormal * wallJumpSideForce * 0.5f;

        pm._PlayerVelocity = jumpDirection;
        isWallRunning = false;
    }

}

