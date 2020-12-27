using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFallDetect : MonoBehaviour
{
    [SerializeField] float detectOriginZ;
    [SerializeField] float detectOriginY;

    [SerializeField] float detectDistance;
    [SerializeField] LayerMask detectLayer;

    public bool fallDetected = false;

    // Update is called once per frame
    void Update()
    {
        DetectFall();
    }

    private void DetectFall()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + transform.forward * detectOriginZ + transform.up * detectOriginY;
        Debug.DrawLine(origin, origin - transform.up * detectDistance, Color.blue);
        if (Physics.Raycast(origin, -transform.up, out hit, detectDistance, detectLayer))
        {
            fallDetected = false;
        }
        else
        {
            fallDetected = true;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + transform.forward * detectOriginZ + transform.up * detectOriginY;
        Debug.DrawLine(origin, origin - transform.up * detectDistance, Color.blue);
    }
}
