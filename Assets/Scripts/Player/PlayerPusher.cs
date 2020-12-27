using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerPusher : MonoBehaviour
{
    [SerializeField] Vector3 centerOfMass;
    [SerializeField] Vector3 checkOrigin;
    [SerializeField] float checkDistance;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] Key LeftKey;
    [SerializeField] Key RightKey;

    private Rigidbody myRB;
    private Transform bodyTR;
    private Animator bodyAnim;
    public IPushable pushableObject;
    public bool isMoving;
    public bool isPushing;
    public bool isPushableFront;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myRB.centerOfMass = centerOfMass;
        bodyTR = GetComponentsInChildren<Animator>()
             .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0].transform;


        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
    }

    private void Update()
    {
        CheckPushableFront();
        if (isPushing && isPushableFront)
        {
            bodyAnim.SetBool("isPushing", true);
            pushableObject.Pushed(bodyTR.forward);
        }
        else
        {
            bodyAnim.SetBool("isPushing", false);
        }

    }

    private void CheckPushableFront()
    {
        RaycastHit hit;
        Debug.DrawLine(bodyTR.position + checkOrigin, bodyTR.position + checkOrigin + bodyTR.forward * checkDistance, Color.red);
        if(Physics.Raycast(bodyTR.position + checkOrigin, bodyTR.forward, out hit, checkDistance, checkLayer))
        {
            isPushableFront = true;
            if (isMoving)
            {
                isPushing = true;
                pushableObject = hit.transform.GetComponent<IPushable>();
            }
            else
            {
                isPushing = false;
                pushableObject = null;
            }
            
        }
        else
        {
            isPushableFront = false;
            pushableObject = null;
        }
    }

}
