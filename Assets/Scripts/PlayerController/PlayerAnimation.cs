using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    public Animator m_Animator;

    [SerializeField]
    private AnimationCurve m_AnimationSpeedCurve;

    private PlayerMovement m_PlayerMovement;

    private float m_FallenDuration = 1.0f;

    private float m_MoveDelay = 2.5f;

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

    public void MakeCharacterFall() 
    {
        StartCoroutine("FallAction");
    }

    IEnumerator FallAction()
    {
        yield return StartCoroutine(SetFalls());
        yield return StartCoroutine(SetMoves());
    }

    IEnumerator SetFalls() 
    {
        m_PlayerMovement.m_cannotMove = true;
        m_Animator.SetTrigger("Fallen");
        yield return new WaitForSeconds(m_FallenDuration);
        m_Animator.ResetTrigger("Fallen");
    }

    IEnumerator SetMoves()
    {
        m_PlayerMovement.m_cannotMove = true;
        yield return new WaitForSeconds(m_MoveDelay);
        m_PlayerMovement.m_cannotMove = false;
    }
    
}
