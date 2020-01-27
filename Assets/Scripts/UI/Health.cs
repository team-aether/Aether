using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int m_MaxHealth = 100;

    private int m_CurrentHealth;
    public event Action<float> OnHealthPercentageChanged = delegate { };

    private void OnEnable()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    public void ModifyHealth(int amount)
    {
        m_CurrentHealth += amount;
        float currentHealthPercent = (float)m_CurrentHealth / (float)m_MaxHealth;
        OnHealthPercentageChanged(currentHealthPercent);
    }

    private void Update()
    {
        if(Input.GetKeyDown("z"))
        {
            ModifyHealth(-10);
        }
    }
}
