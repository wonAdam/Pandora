using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;


public class ScorpionAI : MonsterAI
{
    public enum STATE
    { 
        WAIT, SHOOT, COOLTIME
    }
    public STATE state = STATE.WAIT;

    [SerializeField] GameObject scorpionThrone;
    [SerializeField] Transform shootingPos;
    [SerializeField] float shootingXRange;
    [SerializeField] float shootingCoolTime;
    [SerializeField] int thornDamage;
    public float currShootingCoolTime;

    private void Update()
    {
        if(state == STATE.COOLTIME)
        {
            currShootingCoolTime += Time.deltaTime;
            if(shootingCoolTime <= currShootingCoolTime)
            {
                currShootingCoolTime = 0f;
                state = STATE.WAIT;
            }
        }
    }

    [Task]
    public bool IsPlayerInShootingRange()
    {
        if(!playerHealth)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
        return Mathf.Abs(transform.position.x - playerHealth.transform.position.x) <= shootingXRange;
    }

    [Task]
    public void Shoot()
    {
        myAnim.SetTrigger("Shoot");
    }

    

    [Task]
    public bool IsPlayerFarEnough()
    {
        if (!playerHealth)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        return Mathf.Abs(playerHealth.transform.position.x - transform.position.x) > 2f; 
    }

    [Task]
    public bool IsShootingCoolTime()
    {
        return state == STATE.COOLTIME;
    }

    [Task]
    public bool IsInShootingState()
    {
        return myAnim.GetCurrentAnimatorStateInfo(0).IsName("Shoot");
    }

    // Animation Event
    public void ShootThrone()
    {
        Vector3 selfToPlayer = playerHealth.transform.position - shootingPos.position;
        Vector3 selfForward = shootingPos.forward;
        float angleToPlayer = Vector3.SignedAngle(selfForward, selfToPlayer, Vector3.forward);
        if (dir == Direction.LEFT) angleToPlayer *= -1f;

        Vector3 projectileForward;
        if (angleToPlayer > 30f)
        {
            projectileForward = Quaternion.AngleAxis(30f, -shootingPos.right) * shootingPos.forward;
        }
        else if(angleToPlayer < -30f)
        {
            projectileForward = Quaternion.AngleAxis(-30f, -shootingPos.right) * shootingPos.forward;
        }
        else
        {
            projectileForward = playerHealth.transform.position - shootingPos.position;
        }
        Vector3 up30Vec = Quaternion.AngleAxis(30f, shootingPos.position + shootingPos.right) * (shootingPos.position + shootingPos.forward);
        Vector3 down30Vec = Quaternion.AngleAxis(-30f, shootingPos.position + shootingPos.right) * (shootingPos.position + shootingPos.forward);
        
        GameObject thorn = Instantiate(scorpionThrone, shootingPos.position, Quaternion.LookRotation(projectileForward));
        thorn.GetComponent<ScorpionThrone>().damage = thornDamage;
    }

    // Animation Event
    public void StartCoolTime()
    {
        currShootingCoolTime = 0f;
        state = STATE.COOLTIME;
    }

}
