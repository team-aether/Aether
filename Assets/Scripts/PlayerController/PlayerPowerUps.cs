using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private bool m_canDoubleSpeed;
    private bool m_canDoubleJump;
    private TextChanger m_textChanger;

    private const float m_doubleBuffDuration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_textChanger = GetComponent<TextChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
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

    public void UpdateText() 
    {
        m_textChanger.IndicateBoost(m_canDoubleSpeed, m_canDoubleJump);
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
        SetDoubleJump(true);
        yield return new WaitForSeconds(m_doubleBuffDuration);
        SetDoubleJump(false);
    }

    IEnumerator DoubleUpSpeed()
    {
        SetDoubleSpeed(true);
        yield return new WaitForSeconds(m_doubleBuffDuration);
        SetDoubleSpeed(false);
    }
}
