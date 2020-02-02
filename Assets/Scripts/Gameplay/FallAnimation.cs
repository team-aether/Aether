using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    public void MakeCharacterFall()
    {
        StartCoroutine("FallAction");
    }

    IEnumerator FallAction()
    {
        m_Animator.SetTrigger("Fallen");
        yield return new WaitForSeconds(1);
        m_Animator.ResetTrigger("Fallen");
    }
}
