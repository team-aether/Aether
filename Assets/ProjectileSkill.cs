﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ProjectileSkill : ItemSkill
{
    public override  void InitializeSkill()
    {
        GameObject hitPoint = transform.GetChild(0).gameObject;
        hitPoint.SetActive(true);
    }

    public override void UseSkill()
    {
       
    }
}
