using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    [SerializeField] float moveAcceleration;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    private Rigidbody myRB;
    private Animator myAnim;
    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // movement animation blend tree parameter
        myAnim.SetFloat("Speed", Mathf.Clamp(Mathf.Abs(myRB.velocity.x) / moveSpeed, 0f, 1f));
    }

    public void MoveFront()
    {
        float xSpeed = myRB.velocity.x + transform.forward.x * moveAcceleration * Time.deltaTime;

        float clampSpeed = Mathf.Clamp(xSpeed, -moveSpeed, moveSpeed);

        // move
        myRB.velocity = new Vector3(clampSpeed, myRB.velocity.y, myRB.velocity.z);
    }

    public void MoveFront(float speed)
    {
        float xSpeed = myRB.velocity.x + transform.forward.x * moveAcceleration * Time.deltaTime;

        float clampSpeed = Mathf.Clamp(xSpeed, -speed, speed);

        // move
        myRB.velocity = new Vector3(clampSpeed, myRB.velocity.y, myRB.velocity.z);
    }

    public void Stop()
    {
        myRB.velocity = new Vector3(0f, myRB.velocity.y, myRB.velocity.z);
    }

    public void TurnAroundTo(Quaternion dir)
    {
        if (Mathf.Abs(Quaternion.Angle(transform.rotation, dir)) < 5f)
        {
            transform.rotation = dir;
            return;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, dir, Time.deltaTime * turnSpeed);
    }
}
