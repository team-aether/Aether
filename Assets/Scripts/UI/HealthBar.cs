using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image m_HealthRedBar;
    [SerializeField]
    private float m_UpdateSpeedSeconds = 0.5f;
    [SerializeField]
    private Camera m_MainCam;

    private void Awake()
    {
        GetComponentInParent<Health>().OnHealthPercentageChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float percent)
    {
        StartCoroutine(ChangeToPercent(percent));
    }

    private IEnumerator ChangeToPercent(float newHealthPercent)
    {
        float currentPercentage = m_HealthRedBar.fillAmount;
        float timeElapsed = 0f;

        while(timeElapsed < m_UpdateSpeedSeconds)
        {
            timeElapsed += Time.deltaTime;
            m_HealthRedBar.fillAmount = Mathf.Lerp(currentPercentage, newHealthPercent, timeElapsed / m_UpdateSpeedSeconds);
            yield return null;
        }
        // Ensure that the final intended health percentage is reached.
        m_HealthRedBar.fillAmount = newHealthPercent;
    }

    private void LateUpdate()
    {
        this.gameObject.transform.position = this.gameObject.transform.parent.transform.position + Vector3.up * 2.5f;
        // Rotate healthbar towards viewing camera
        transform.LookAt(m_MainCam.transform);
        transform.Rotate(0, 180, 0);
    }
}
