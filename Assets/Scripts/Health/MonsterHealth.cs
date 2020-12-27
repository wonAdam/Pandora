using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class MonsterHealth : Health
{
    private Animator bodyAnim;
    protected override void Start()
    {
        base.Start();
        bodyAnim = GetComponentInChildren<Animator>();
    }

    public override bool TakeDamage(int damage, Transform opponent)
    {
        // Still in CoolTime
        if (currDamageCoolTime > 0f) return false;
        // Get into CoolTime
        else currDamageCoolTime = damageCoolTime;

        // Damage process
        base.TakeDamage(damage, opponent);

        // Dead
        if (currHealth == 0)
        {
            bodyAnim.SetBool("isDead", true);
        }
        // Damage Animation
        else
        {

        }

        return true;

    }

    public void DisableAI()
    {
        GetComponent<PandaBehaviour>().enabled = false;
        GetComponent<MonsterAI>().enabled = false;
    }
}
