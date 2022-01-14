using UnityEngine;
using static scr_Models;

public class scr_PlayerController : MonoBehaviour
{
    Rigidbody characterRigidBody;
    Animator characterAnimator;
    PlayerInputActions PlayerInputActions;
    [HideInInspector]
    public Vector2 input_Movement;
    [HideInInspector]
    public Vector2 input_View; 

    Vector3 playerMovement;

    [Header("Settings")]
    public PlayerSettingsModel settings;
    public bool isTargetMode;

    [Header("Camera")]
    public Transform cameraTarget;
    public scr_CameraController cameraController;

    [Header("Movement")]
    public float movementSpeedOffset = 1;
    public float movementSmoothdamp = 0.3f;
    public bool isWalking;
    [HideInInspector]
    public bool isSprinting;

    private float verticalSpeed;
    private float targetVerticalSpeed;
    private float verticalSpeedVelocity;

    private float horizontalSpeed;
    private float targetHorizontalSpeed;
    private float horizontalSpeedVelocity;

    private Vector3 relativePlayerVelocity;

    [Header("Stats")]
    public PlayerStatsModel playerStats;

    [Header("Gravity")]
    public float gravity = 10;
    public LayerMask groundMask;
    private Vector3 gravityDirection;

    [Header("Jumping/Falling")]
    private float fallingSpeed;
    private float fallingSpeedPeak;
    public float fallingThreshold;
    public float fallingMovementSpeed;
    public float fallingRunningMovementSpeed;

    private bool jumpingTriggered;
    private bool fallingTriggered;
    public float maxFallingMovementSpeed;

    private Vector3 cameraRelativeForward;
    private Vector3 cameraRelativeRight;

    [Header("Combat")]
    public bool isFaceTarget;
    public Transform target;
    [HideInInspector]
    public bool isAttacking;
    public float stickyTargetDistance = 1;
    public float stickyTargetAmount = 1;
    public float combatCooldown = 2;
    public float currentCombatCooldown;

    private float fire1Timer;
    private float kickTimer;

    #region - Awake -
    private void Awake()
    {
        characterRigidBody = GetComponent<Rigidbody>();
        characterAnimator = GetComponent<Animator>();

        PlayerInputActions = new PlayerInputActions();

        PlayerInputActions.Movement.Movement.performed += x => input_Movement = x.ReadValue<Vector2>();
        PlayerInputActions.Movement.View.performed += x => input_View = x.ReadValue<Vector2>();
        
        PlayerInputActions.Actions.Jump.performed += x => Jump();

        PlayerInputActions.Actions.WalkingToggle.performed += x => ToggleWalking();
        PlayerInputActions.Actions.Sprint.performed += x => Sprint();

        PlayerInputActions.Actions.Fire1.performed += x => Fire1();
        PlayerInputActions.Actions.Fire1Hold.performed += x => Fire1Hold();

        PlayerInputActions.Actions.Kick.performed += x => Kick();

        PlayerInputActions.Enable();

        gravityDirection = Vector3.down;
    }
    #endregion

    #region - Jumping -
    private void Jump()
    {
        if(jumpingTriggered)
        {
            return;
        }
        jumpingTriggered = true;

        if(isMoving() && isInputMoving() && !isWalking)
        {
            characterAnimator.SetTrigger("RunningJump");
        }
        else if(isMoving() && isInputMoving() && isWalking)
        {
            characterAnimator.SetTrigger("WalkingJump");
        }
        else
        {
            characterAnimator.SetTrigger("Jump");
        }
    }

    public void applyJumpForce()
    {
        if(!isGrounded())
        {
            return;
        }
        characterRigidBody.AddForce(transform.up * settings.JumpingForce, ForceMode.Impulse);
        fallingTriggered = true;
    }
    #endregion

    #region - Sprinting -
    private void Sprint()
    {
        if(!CanSprint())
        {
            return;
        }
        if(playerStats.Stamina > (playerStats.MaxStamina / 4))
        {
            isSprinting = true;
        }
    }

    private bool CanSprint()
    {
        if(isTargetMode)
        {
            return false;
        }

        var sprintFalloff = 0.8f;

        if((input_Movement.y < 0 ? input_Movement.y * -1: input_Movement.y) < sprintFalloff && (input_Movement.x < 0 ? input_Movement.x * -1 : input_Movement.x) < sprintFalloff)
        {
            return false;
        }

        return true;
    }

    private void CalculateSprint()
    {
        if(!CanSprint())
        {
            isSprinting = false;
        }
        if(isSprinting)
        {
            if(playerStats.Stamina > 0)
            {
                playerStats.Stamina -= playerStats.StaminaDrain * Time.deltaTime;
            }
            else
            {
                isSprinting = false;
            }
            playerStats.StaminaCurrentDelay = playerStats.StaminaDelay;
        }
        else
        {
            if(playerStats.StaminaCurrentDelay <= 0)
            {
                if(playerStats.Stamina < playerStats.MaxStamina)
                {
                    playerStats.Stamina += playerStats.StaminaRestore * Time.deltaTime;
                }
                else
                {
                    playerStats.Stamina = playerStats.MaxStamina;
                }
            }
            else
            {
                playerStats.StaminaCurrentDelay -= Time.deltaTime;
            }
        }
    }
    #endregion

    #region - Gravity -
    private bool isGrounded()
    {
        if (Physics.CheckSphere(transform.position, 0.25f, groundMask))
        {
            return true;
        }
        return false;
    }

    private bool isFalling()
    {
        if(fallingSpeed < fallingThreshold)
        {
            return true;
        }
        return false;
    }

    private void CalculateGravity()
    {
        Physics.gravity = gravityDirection * gravity;
    }

    private void CalculateFalling()
    {
        fallingSpeed = relativePlayerVelocity.y;

        if(fallingSpeed < fallingSpeedPeak && fallingSpeed < -0.1f && (fallingTriggered || jumpingTriggered))
        {
            fallingSpeedPeak = fallingSpeed;
        }

        if((isFalling() && !isGrounded() && !jumpingTriggered && !fallingTriggered) || (jumpingTriggered && !fallingTriggered && !isGrounded()))
        {
            fallingTriggered = true;
            characterAnimator.SetTrigger("Falling");
        }

        if(fallingTriggered && isGrounded() && fallingSpeed < -0.1f)
        {
            fallingTriggered = false;
            jumpingTriggered = false;
            
            if(fallingSpeedPeak < -7)
            {
                characterAnimator.SetTrigger("HardLand");
            }
            else
            {
                characterAnimator.SetTrigger("Land");
            }
            
            fallingSpeedPeak = 0;
        }
    }
    #endregion

    #region - Combat -
    
    public void Fire1()
    {
        if (!isAttacking)
        {
            if(fire1Timer <= 0)
            {
                fire1Timer = 0.4f;
                return;
            }
            StartAttacking();
            characterAnimator.SetTrigger("SwordAttack1");
        }    
    }

    public void Fire1Hold()
    {
        if (!isAttacking)
        {
            StartAttacking();
            characterAnimator.SetTrigger("HeavyAttack");
        }    
    }

    public void Kick()
    {
        if (!isAttacking)
        {
            if(kickTimer <= 0)
            {
                kickTimer = 0.4f;
                return;
            }
            StartAttacking();
            characterAnimator.SetTrigger("Kick");
        }    
    }

    public void CalculateCombat()
    {
        if(fire1Timer >= 0)
        {
            fire1Timer -= Time.deltaTime;
        }

        if(kickTimer >= 0)
        {
            kickTimer -= Time.deltaTime;
        }

        if(currentCombatCooldown > 0)
        {
            if(!isAttacking)
            {
                currentCombatCooldown -= Time.deltaTime;
            }
        }
        else
        {
            isTargetMode = false;
        }
        if(isFalling())
        {
            isTargetMode = false;
            isAttacking = false;
        }
    }
    
    #endregion

    #region - Update -
    private void Update()
    {
        CalculateGravity();
        CalculateFalling();
        Movement();
        CalculateSprint();
        CalculateCombat();
    }
    #endregion

    #region - Movement -
    private void ToggleWalking()
    {
        isWalking = !isWalking;
    }

    public bool isMoving()
    {
        if(relativePlayerVelocity.x > 0.4f || relativePlayerVelocity.x < -0.4f)
        {
            return true;
        }
        if(relativePlayerVelocity.z > 0.4f || relativePlayerVelocity.z < -0.4f)
        {
            return true;
        }
        return false;
    }

    public bool isInputMoving()
    {
        if(input_Movement.x > 0.2f || input_Movement.x < -0.2f)
        {
            return true;
        }
        if(input_Movement.y > 0.2f || input_Movement.y < -0.2f)
        {
            return true;
        }
        return false;
    }

    private void Movement()
    {   
        characterAnimator.SetBool("IsTargetMode", isTargetMode);

        relativePlayerVelocity = transform.InverseTransformDirection(characterRigidBody.velocity);

        if(isTargetMode)
        {
            if(input_Movement.y > 0)
            {
                targetVerticalSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed);
            }
            else
            {
                targetVerticalSpeed = (isWalking ? settings.WalkingBackwardSpeed : settings.RunningBackwardSpeed);
            }

            targetHorizontalSpeed = (isWalking ? settings.WalkingStrafingSpeed : settings.RunningStrafingSpeed);

            if (isFaceTarget && target)
            {
                var lookDirection = target.position - transform.position;
                lookDirection.y = 0;

                var currentRotation = transform.rotation;

                transform.LookAt(lookDirection + transform.position, Vector3.up);
                var newRotation = transform.rotation;

                transform.rotation = Quaternion.Lerp(currentRotation, newRotation, settings.CharacterRotationSmoothdamp);
            }
            else
            {
                var currentRotation = transform.rotation;

                var newRotation = currentRotation.eulerAngles;
                newRotation.y = cameraController.targetRotation.y;

                currentRotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(newRotation), settings.CharacterRotationSmoothdamp);
                transform.rotation = currentRotation;
            }
        }
        else
        {
            var originalRotation = transform.rotation;
            transform.LookAt(playerMovement + transform.position, Vector3.up);
            var newRotation = transform.rotation;

            transform.rotation = Quaternion.Lerp(originalRotation, newRotation, settings.CharacterRotationSmoothdamp);

            float playerSpeed = 0;

            if(isSprinting)
            {
                playerSpeed = settings.SprintingSpeed;
            }
            else
            {
                playerSpeed = (isWalking ? settings.WalkingSpeed : settings.RunningSpeed);
            }

            targetVerticalSpeed = playerSpeed;
            targetHorizontalSpeed = playerSpeed;
        }

        targetVerticalSpeed = (targetVerticalSpeed * movementSpeedOffset) * input_Movement.y;
        targetHorizontalSpeed = (targetHorizontalSpeed * movementSpeedOffset) * input_Movement.x;

        verticalSpeed = Mathf.SmoothDamp(verticalSpeed, targetVerticalSpeed, ref verticalSpeedVelocity, movementSmoothdamp);
        horizontalSpeed = Mathf.SmoothDamp( horizontalSpeed, targetHorizontalSpeed, ref horizontalSpeedVelocity, movementSmoothdamp);

        if(isTargetMode)
        {
            var relativeMovement = transform.InverseTransformDirection(playerMovement);

            characterAnimator.SetFloat("Vertical", relativeMovement.z);
            characterAnimator.SetFloat("Horizontal", relativeMovement.x);
        }
        else
        {
            float verticalActualSpeed = verticalSpeed < 0 ? verticalSpeed * -1 : verticalSpeed;
            float horizontalActualSpeed = horizontalSpeed < 0 ? horizontalSpeed * -1 : horizontalSpeed;

            float animatorVertical = verticalActualSpeed > horizontalActualSpeed ? verticalActualSpeed : horizontalActualSpeed;

            characterAnimator.SetFloat("Vertical", animatorVertical);
        }

        if(isInputMoving())
        {
            cameraRelativeForward = cameraController.transform.forward;
            cameraRelativeRight = cameraController.transform.right;
        }

        playerMovement = cameraRelativeForward * verticalSpeed;
        playerMovement += cameraRelativeRight * horizontalSpeed;

        //characterController.Move(); rigidbody

        if(jumpingTriggered || isFalling())
        {
            characterAnimator.applyRootMotion = false;

            if (Vector3.Dot(characterRigidBody.velocity, playerMovement) < maxFallingMovementSpeed)
            {
                characterRigidBody.AddForce(playerMovement * (isWalking ? fallingMovementSpeed : fallingRunningMovementSpeed));
            }
        }
        else
        {
            characterAnimator.applyRootMotion = true;
        }

        if (isAttacking && isGrounded() && isTargetMode)
        {
            isFaceTarget = true;

            if(Vector3.Distance(transform.position, target.transform.position) > stickyTargetDistance)
            {
                characterRigidBody.AddRelativeForce(Vector3.forward * stickyTargetAmount, ForceMode.Force);
            }
        }
    }
    #endregion

    #region - Events -
    public void StartAttacking()
    {
        isAttacking = true;
        isTargetMode = true;
    }
    public void FinishAttacking()
    {
        currentCombatCooldown = combatCooldown;
        isAttacking = false;
    }
    #endregion

    #region - Enable/Disable -
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }

    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }

    #endregion
    
    #region - Gizmos -
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, 0.25f);
    }
    #endregion
}
