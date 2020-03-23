﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(PlayerStance))]
public class SkillHandler : MonoBehaviour
{
    private PlayerStance m_PlayerStance;
    private const int m_SkillLimit = 3;
    private Queue<SkillItem> m_ItemSkillSlots;
    
    void Start()
    {
        m_PlayerStance = GetComponentInParent<PlayerStance>();
        AetherInput.GetPlayerActions().UseSkill.performed += UseSkillAt;
        AetherInput.GetPlayerActions().SwitchSkills.performed += SwitchSkills;
        m_ItemSkillSlots = new Queue<SkillItem>();
    }

    //key bindings
    public void UseSkillAt(InputAction.CallbackContext ctx)
    {
        ButtonControl button = ctx.control as ButtonControl;
        if (!button.wasPressedThisFrame)
            return;

        if (m_ItemSkillSlots.Count == 0)
            return;

        SkillItem currentSkill = m_ItemSkillSlots.Peek();

        if (currentSkill.IsGroundOnlySpell())
        {
            if (!m_PlayerStance.CanPerformAction(PlayerStance.Action.ACTION_CASTSPELL))
                return;
        }

        currentSkill.UseSkill(this.transform);
        currentSkill.DecrementUses();

        if (currentSkill.HasNoMoreUses())
            RemoveSkill();
    }

    public void AddSkill(SkillItem skillItem)
    {

        if (m_ItemSkillSlots.Count >= m_SkillLimit)
            return;

        m_ItemSkillSlots.Enqueue(skillItem);
        UIManager.Instance.SaveSkill(skillItem);
    }

    public void RemoveSkill()
    {
        UIManager.Instance.RemoveSkill();
        m_ItemSkillSlots.Dequeue();
    }

    public void SwitchSkills(InputAction.CallbackContext ctx)
    {
        UIManager.Instance.SwitchPlayerSkills();
        m_ItemSkillSlots.Enqueue(m_ItemSkillSlots.Dequeue());
    }
    
}
