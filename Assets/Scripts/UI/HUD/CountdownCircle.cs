using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownCircle : MonoBehaviour
{
    public Image m_CountdownBar;
    public Image m_Center; 
    public RawImage m_Icon;    
    private const float m_BuffDuration = 5.0f;

    public float m_CurrentAmount;
    public const float m_Speed = 100.0f / m_BuffDuration;
    private bool m_Activated;


    // Start is called before the first frame update
    void Start()
    {
        EnableCountdown(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Activated) {
            Countdown();
        } 
    }

    void Countdown() 
    {
        if (m_CurrentAmount > 0) {
            m_CurrentAmount -= m_Speed * Time.deltaTime;
        }
        else {
            EnableCountdown(false);
        }

        m_CountdownBar.fillAmount = m_CurrentAmount / 100;
    }

    private void EnableCountdown(bool canCountdown)
    {   
        m_Activated = canCountdown;
        m_CountdownBar.enabled = canCountdown;
        m_Center.enabled = canCountdown;
        m_Icon.enabled = canCountdown;
    }
}
