using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityModifier : MonoBehaviour
{
    public float ModifyXVelocity(float velocityVector, PowerUpsManager powerups)
    {
        if (powerups.GetDoubleSpeed())
        {
            velocityVector *= 2.0f;
        }

        return velocityVector;
    }

    public float ModifyYVelocity(float velocityVector, PowerUpsManager powerups)
    {
        if (powerups.GetDoubleJump())
        {
            if (velocityVector < 0)
            {
                velocityVector *= 0.5f;
            }
            else
            {
                velocityVector *= 2.0f;
            }
        }

        return velocityVector;
    }

    public float ModifyZVelocity(float velocityVector, PowerUpsManager powerups)
    {
        if (powerups.GetDoubleSpeed())
        {
            velocityVector *= 2.0f;
        }

        return velocityVector;
    }
}
