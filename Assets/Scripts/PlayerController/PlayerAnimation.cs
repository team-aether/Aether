﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    [SerializeField]
    private AnimationCurve m_AnimationSpeedCurve;

    private PlayerMovement m_PlayerMovement;

    private const float m_doubleBuffDuration = 5.0f;

    private bool m_hasJumpPower;
    private bool m_hasSpeedPower;

    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Vector2 movementInput = m_PlayerMovement.GetLastKnownInput();
        m_Animator.SetFloat("MovementInput", movementInput.magnitude);

        Vector3 velocity = m_PlayerMovement.GetVelocity();
        m_Animator.SetFloat("VerticalVelocity", velocity.y);

        // remove y-velocity
        velocity.y = 0;

        if (velocity.magnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity.normalized), Time.deltaTime * 10);

        // Set Grounded boolean
        m_Animator.SetBool("Grounded", m_PlayerMovement.GetIsGrounded());

        // Handle Jumping callback
        if (m_PlayerMovement.GetJumpedInCurrentFrame())
            m_Animator.SetTrigger("Jumping");

        // Set walking animation speed based on players actual velocity. This allows slightly better sync between
        // animation and gameplay.
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (movementInput.magnitude > 0.01f)
                m_Animator.speed = m_AnimationSpeedCurve.Evaluate(movementInput.magnitude); // Make running animation speed match actual movement speed
            else 
                m_Animator.speed = 1;
        }
    }

    public bool hasJumpPowerUp() 
    {
        return m_hasJumpPower;
    }

    public bool hasSpeedPowerUp()
    {
        return m_hasSpeedPower;
    }

    public void GetFlag()
    {
        m_PlayerMovement.GetFlag();
    }

    public void GoFaster()
    {
        StartCoroutine("DoubleUpSpeed");
    }

    public void JumpHigher()
    {
        StartCoroutine("DoubleUpJump");
    }

    IEnumerator DoubleUpJump()
    {
        m_hasJumpPower = true;
        m_PlayerMovement.SetDoubleJump(true);
        yield return new WaitForSeconds(m_doubleBuffDuration);
        m_hasJumpPower = false;
        m_PlayerMovement.SetDoubleJump(false);
    }

    IEnumerator DoubleUpSpeed()
    {
        m_hasSpeedPower = true;
        m_PlayerMovement.SetDoubleSpeed(true);
        yield return new WaitForSeconds(m_doubleBuffDuration);
        m_hasSpeedPower = false;
        m_PlayerMovement.SetDoubleSpeed(false);
    }
}
