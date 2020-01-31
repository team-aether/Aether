using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    [Range(0, 10)]
    private float m_MoveSpeed = 6;

    [SerializeField]
    private Transform m_GroundCheck = null;

    [SerializeField]
    private LayerMask m_LayerMask = new LayerMask();

    [SerializeField]
    private float m_Gravity = -9.8f;

    [SerializeField]
    private float m_FallingGravityMultiplier = 2.0f;

    [SerializeField]
    private float m_JumpHeight = 1.3f;

    [SerializeField]
    private GameObject m_TheFlag;

    private CharacterController m_CharacterController;
    private TextChanger m_textChanger;

    private Vector3 m_Velocity;
    private Vector2 m_LastKnownInput;

    private float m_LandingRecoveryTime = 0.05f;
    private float m_LandingSpeedModifier = 0.0f;
    private float m_LandingTime = 0;
    private bool m_IsMidAir;
    private bool m_JumpedInCurrentFrame;
    private bool m_canDoubleSpeed;
    private bool m_canDoubleJump;
    private bool m_hasFlag;

    void Start()
    {
        AetherInput.GetPlayerActions().Jump.performed += HandleJump;
        AetherInput.GetPlayerActions().Fire.performed += HandleFlag;
        m_CharacterController = GetComponent<CharacterController>();
        m_textChanger = GetComponent<TextChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetIsGrounded())
        {
            // Gravity should not accumulate when player is grounded. We set velocity to -2 instead of 0
            // because the collision may register before we actually fully touch the ground. This value is purely
            // empirical.
            if (m_Velocity.y < 0)
            {
                m_Velocity.y = -2.0f;
            }

            if (m_IsMidAir)
            {
                m_IsMidAir = false;
                // Landing frame
                m_LandingTime = Time.time;
            }
        }

        HandleGravity();
        HandleMovement();
        UpdateText();

        float t = Time.deltaTime;
        float t2 = t * t;

        // m_canDoubleSpeed
        float xVelocity = m_Velocity.x;
        float yVelocity = m_Velocity.y * t + 0.5f * GetGravityMagnitude() * t2; 
        float zVelocity = m_Velocity.z;

        if (m_canDoubleSpeed)
        {
            xVelocity *= 2.0f;
            zVelocity *= 2.0f;
        }

        if (m_canDoubleJump) 
        {
            if (yVelocity < 0) 
            {
                yVelocity *= 0.5f;
            } else {
                yVelocity *= 2.0f;
            }
        }

        m_CharacterController.Move(new Vector3(xVelocity, yVelocity, zVelocity));
    }

    public void GetFlag()
    {
        m_hasFlag = true;
        m_textChanger.IndicateFlag(m_hasFlag);
    }

    public void HandleFlag(InputAction.CallbackContext ctx)
    {
        ButtonControl button = ctx.control as ButtonControl;
        if (!button.wasPressedThisFrame)
            return;

        if (!m_hasFlag)
            return;

        m_hasFlag = false;
        m_textChanger.IndicateFlag(m_hasFlag);
        Instantiate(m_TheFlag, transform.position+(transform.forward*2)+(transform.up), transform.rotation);
    }

    public void UpdateText() 
    {
        m_textChanger.IndicateBoost(m_canDoubleSpeed, m_canDoubleJump);
    }

    private void LateUpdate()
    {
        m_JumpedInCurrentFrame = false;
    }

    private void HandleGravity()
    {
        m_Velocity.y += GetGravityMagnitude() * Time.deltaTime;
    }
    
    private void HandleMovement()
    {
        m_LastKnownInput = AetherInput.GetPlayerActions().Move.ReadValue<Vector2>();
        Vector3 move = Camera.main.transform.right * m_LastKnownInput.x + Camera.main.transform.forward * m_LastKnownInput.y;
        move *= Time.deltaTime * m_MoveSpeed;

        // Slow the player down after a fall
        if (IsRecoveringFromFall())
        {
            float mod = (Time.time - m_LandingTime) / m_LandingRecoveryTime;
            move *= Mathf.Lerp(m_LandingSpeedModifier, 1, mod);
        }

        m_Velocity = new Vector3(move.x, m_Velocity.y, move.z);
    }

    public void HandleJump(InputAction.CallbackContext ctx)
    {
        ButtonControl button = ctx.control as ButtonControl;
        if (!button.wasPressedThisFrame)
            return;

        if (!GetIsGrounded())
            return;

        if (IsRecoveringFromFall())
            return;

        m_JumpedInCurrentFrame = true;
        m_IsMidAir = true;
    }

    public Vector2 GetLastKnownInput()
    {
        return m_LastKnownInput;
    }

    public Vector3 GetVelocity()
    {
        return m_Velocity;
    }

    public float GetGravityMagnitude()
    {
        return m_Gravity * (m_Velocity.y >= 0 ? 1 : m_FallingGravityMultiplier);
    }

    public bool IsRecoveringFromFall()
    {
        return Time.time - m_LandingTime < m_LandingRecoveryTime;
    }

    public bool GetIsGrounded()
    {
        return Physics.CheckSphere(m_GroundCheck.position, 0.5f, m_LayerMask) && !GetJumpedInCurrentFrame();
    }

    public bool GetJumpedInCurrentFrame()
    {
        return m_JumpedInCurrentFrame;
    }

    public bool GetDoubleSpeed()
    {
        return m_canDoubleSpeed;
    }

    public void SetDoubleSpeed(bool boolean)
    {
        m_canDoubleSpeed = boolean;
    }

    public bool GetDoubleJump()
    {
        return m_canDoubleJump;
    }

    public void SetDoubleJump(bool boolean)
    {
        m_canDoubleJump = boolean;
    }

    // This should be an animation callback for more visually appealing jumps
    public void Jump()
    {
        m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2 * m_Gravity);
    }
}
