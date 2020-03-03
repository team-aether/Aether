﻿using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSkill : MonoBehaviour
{
    protected int m_NoOfUses;
    public Image m_SkillIcon;
    
    public abstract void UseSkill();
    public abstract void InitializeSkill();
}
