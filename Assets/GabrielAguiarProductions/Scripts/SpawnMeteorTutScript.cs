using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorTutScript : MonoBehaviour
{
    public GameObject camHolder;
    public GameObject vfx;
    public Transform startPoint;
    public Transform endPoint;

    void Start()
    {
        var startPos = startPoint.position;
        GameObject objVFX = Instantiate(vfx, startPos, Quaternion.identity) as GameObject;
        var pm = objVFX.GetComponent<ProjectileMoveTutScript>();
        if (pm != null)
        {
            var camAnim = camHolder.GetComponent<Animation>();
            if (camAnim != null)
                pm.SetCamAnim(camAnim);
        }

        var endPos = endPoint.position;

        RotateTo (objVFX, endPos);        
    }

    void RotateTo (GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp (obj.transform.rotation, rotation, 1);
    }
}
