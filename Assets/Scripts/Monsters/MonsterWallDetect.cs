using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWallDetect : MonoBehaviour
{
    [SerializeField] float detectOriginZ;
    [SerializeField] float detectOriginY;

    [SerializeField] float detectDistance;
    [SerializeField] LayerMask detectLayer;

    public bool wallDetected = false;


    // Update is called once per frame
    void Update()
    {
        DetectWall();
    }

    private void DetectWall()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + transform.forward * detectOriginZ + transform.up * detectOriginY;
        Debug.DrawLine(origin, origin + transform.forward * detectDistance, Color.red);
        if (Physics.Raycast(origin, transform.forward, out hit, detectDistance, detectLayer))
        {
            wallDetected = true;
        }
        else
        {
            wallDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + transform.forward * detectOriginZ + transform.up * detectOriginY;
        Debug.DrawLine(origin, origin + transform.forward * detectDistance, Color.red);
    }
}
