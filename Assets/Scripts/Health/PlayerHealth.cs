using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] Events playerDead_E;
    [SerializeField] float pushedVel;
    private Animator bodyAnim;
    private Rigidbody myRB;
    private PlayerController playerController;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        bodyAnim = GetComponentsInChildren<Animator>()
            .Where<Animator>(anim => anim.gameObject.GetInstanceID() != gameObject.GetInstanceID()).ToList()[0];
        myRB = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    public override bool TakeDamage(int damage, Transform opponent)
    {
        // Still in CoolTime
        if (currDamageCoolTime > 0f) return false;
        // Get into CoolTime
        else currDamageCoolTime = damageCoolTime;

        // Damage process
        base.TakeDamage(damage, opponent);
        Vector3 pushDir = new Vector3(transform.position.x - opponent.position.x, 0f, 0f).normalized;
        myRB.AddForce(pushDir * pushedVel, ForceMode.VelocityChange);


        // Dead
        if (currHealth == 0)
        {
            bodyAnim.SetBool("isDead", true);
            playerController.Disable();
        }
        // Damage Animation
        else
        {
            float angle = Vector3.Angle(transform.forward, opponent.position - transform.position);
            float absAngle = Mathf.Abs(angle);
            // hit from front
            if (absAngle < 45f)
            {
                bodyAnim.SetTrigger("HitFromFront");
            }
            // hit from back
            else
            {
                bodyAnim.SetTrigger("HitFromBack");
            }
        }

        return true;
    }

    // Observer Pattern
    public void PlayerDead()
    {
        playerDead_E.OnOccur();
    }
}
