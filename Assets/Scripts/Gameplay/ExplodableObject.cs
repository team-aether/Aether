using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    private float m_TerrainRevealRadius = 30.0f;
    private static float m_TimeOfLastSFX = 0.0f;

    private int timeDelay = 3;

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, timeDelay);
    }

    void OnDestroy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_TerrainRevealRadius);

        foreach (Collider c in colliders)
        {
            RevealableTerrain target = c.GetComponent<RevealableTerrain>();

            if (target != null)
            {
                target.PaintAtPosition(transform.position, m_TerrainRevealRadius);
            }

            RevealableObject theObject = c.GetComponent<RevealableObject>();

            if (theObject != null)
            {
                theObject.Reveal();
            }

            FallAnimation anim = c.GetComponent<FallAnimation>();

            if (anim != null)
            {
                anim.MakeCharacterFall();
            }
        }
    }
}
