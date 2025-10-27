using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speedMultiplier = 1;
    private UnityEvent OnLeftClick;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_InputField sensitivityText;
    [SerializeField] private GameObject speedParticles2;
    [SerializeField] private GameObject speedParticles3;
    [SerializeField] private GameObject speedParticles4;
    [SerializeField] private GameObject speedParticles5;

    public bool gameIsPaused = false;
    public bool canMove = true;
    public float SpeedMultiplier => speedMultiplier;
    PlayerInput playerInput;
    PlayerInput.MainActions input;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    [Header("Controller")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;

    public Vector3 _PlayerVelocity;

    public bool isGrounded;

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;
    float zRotation = 0f;
    /*public float dashSpeed = 10f;
    public float dashDuration = 2f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector3 dashDirection;*/
    private float coyoteTiming = 0f;

    [SerializeField] TutorialManager tutorialManager;

    void Awake()
    { 
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        OnLeftClick = new UnityEvent();
        OnLeftClick.AddListener(Attack);
    }


    void Update()
    {
        isGrounded = controller.isGrounded;

        // Repeat Inputs
        if(input.Attack.IsPressed())
        { 
            Attack(); 
        }

        SetAnimations();

        if (speedMultiplier > 1)
        {
            timeSinceLastDecrement += Time.deltaTime;

            if (timeSinceLastDecrement >= decrementInterval)
            {
                SpeedMultiplierDecrement();
                timeSinceLastDecrement = 0f;
            }
        }
        /*if (Input.GetMouseButtonDown(1) && !isDashing)
        {
            if (canMove)
            {
                dashDirection = cam.transform.forward;
                dashTimer = dashDuration;
                isDashing = true;
            }
        }

        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }*/

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopGame();
        }
        SpeedEffect();
    }

    void FixedUpdate() 
    { 
        MoveInput(input.Movement.ReadValue<Vector2>()); 
        if (!isGrounded)
        {
            coyoteTiming += Time.deltaTime;
        }
        else
        {
            coyoteTiming = 0f;
        }
    }

    void LateUpdate() 
    { 
        if (!gameIsPaused)
        {
            LookInput(input.Look.ReadValue<Vector2>());
        }
    }


    void MoveInput(Vector2 input)
    {
        if (canMove)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;

            controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * speedMultiplier * Time.deltaTime);
            _PlayerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && _PlayerVelocity.y < 0)
            {
                _PlayerVelocity.y = -2f;
            }
            controller.Move(_PlayerVelocity * Time.deltaTime);
        }
    }

    private void SpeedEffect()
    {
        switch(speedMultiplier)
        {
            case 2:
                speedParticles2.SetActive(true);
                speedParticles3.SetActive(false);
                speedParticles4.SetActive(false);
                speedParticles5.SetActive(false);
                break;
            case 3:
                speedParticles2.SetActive(false);
                speedParticles3.SetActive(true);
                speedParticles4.SetActive(false);
                speedParticles5.SetActive(false);
                break;
            case 4:
                speedParticles2.SetActive(false);
                speedParticles3.SetActive(false);
                speedParticles4.SetActive(true);
                speedParticles5.SetActive(false);
                break;
            case 5:
                speedParticles2.SetActive(false);
                speedParticles3.SetActive(false);
                speedParticles4.SetActive(false);
                speedParticles5.SetActive(true);
                break;
            default:
                speedParticles2.SetActive(false);
                speedParticles3.SetActive(false);
                speedParticles4.SetActive(false);
                speedParticles5.SetActive(false);
                break;
        }
    }
    void LookInput(Vector2 input)
    {
        if (!canMove) return;

        float mouseX = input.x * sensitivity;
        float mouseY = input.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Rotación vertical (pitch) solo en la cámara
        cam.transform.localRotation =
            Quaternion.Euler(xRotation, 0f, zRotation);

        // Rotación horizontal (yaw) en el jugador
        transform.Rotate(Vector3.up * mouseX);
    }
    public void OnEnable() 
    { 
        input.Enable(); 
    }

    public void OnDisable()
    {
        input.Disable(); 
    }

    void Jump()
    {
        // Adds force to the player rigidbody to jump
        if (canMove)
        {
            if (isGrounded || coyoteTiming < 0.2f)
            {
                _PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
    }

    void AssignInputs()
    {
        input.Jump.performed += ctx => Jump();
        input.Attack.started += ctx => Attack();
    }

    // ---------- //
    // ANIMATIONS //
    // ---------- //

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) 
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        // If player is not attacking
        if(!attacking)
        {
            if(_PlayerVelocity.x == 0 &&_PlayerVelocity.z == 0)
            { ChangeAnimationState(IDLE); }
            else
            { ChangeAnimationState(WALK); }
        }
    }

    // ------------------- //
    // ATTACKING BEHAVIOUR //
    // ------------------- //

    [Header("Attacking")]
    public float attackDistance = 1.5f;
    public float attackSpeed = 0.4f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    [Header("Delay")]
    public float decrementInterval = 3f;
    public float timeSinceLastDecrement = 0f;


    public void Attack()
    {
        if (canMove)
        {
            if (!readyToAttack || attacking) return;

            readyToAttack = false;
            attacking = true;

            Invoke(nameof(ResetAttack), attackSpeed);
            AttackRaycast();

            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(swordSwing);

            if (attackCount == 0)
            {
                ChangeAnimationState(ATTACK1);
                attackCount++;
            }
            else
            {
                ChangeAnimationState(ATTACK2);
                attackCount = 0;
            }
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        int rayCount = 5;
        float angleSpread = 45f;
        float halfSpread = angleSpread / 2f;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-halfSpread, halfSpread, (float)i / (rayCount - 1));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * cam.transform.forward;
            if (Physics.Raycast(cam.transform.position, direction, out RaycastHit hit, attackDistance * speedMultiplier, attackLayer))
            {
                HitTarget(hit.point);
                if (hit.transform.TryGetComponent<Actor>(out Actor T))
                {
                    if (speedMultiplier < 5)
                    {
                        speedMultiplier++;
                    }                    
                    T.TakeDamage(attackDamage);
                    timeSinceLastDecrement = 0f;
                    break;
                }
            }
        }        
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    private void SpeedMultiplierDecrement()
    {
        speedMultiplier--;
    }

    public void TiltCamera(float tiltAmount)
    {
        if(zRotation < tiltAmount)
        {
            zRotation+= 0.5f;
        }
        if(zRotation > tiltAmount)
        {
            zRotation-= 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Movement":
                tutorialManager.OnPlayerMoved();
                break;
            case "Jump":
                tutorialManager.OnPlayerJump();
                break;
            case "WallRunning":
                tutorialManager.OnPlayerWallRun();
                break;
            case "AttackEnemy":
                tutorialManager.OnPlayerAttackEnemy();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialManager.Desactivate();
    }

    public void FreeTheMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void EncloseTheMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StopGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        FreeTheMouse();
        pauseMenu.SetActive(true);
    }

    public void UpdateSensitivityFromSlider()
    {
        sensitivity = sensitivitySlider.value;
        sensitivityText.text = sensitivity.ToString();
    }

    public void UpdateSensitivityFromText()
    {
        if (float.TryParse(sensitivityText.text, out float number))
        {
            sensitivity = number;
            sensitivitySlider.value = sensitivity;
        }
    }
}