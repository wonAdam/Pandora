using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class SpiderAI : MonsterAI
{
    public enum STATE
    {
        WAIT, DASH, COOLTIME
    }
    public STATE state = STATE.WAIT;
    [SerializeField] float dashingXRange;
    [SerializeField] float dashingCoolTime;
    public float currDashingCoolTime;
    public float targetXPos;
    public float startXPos;

    private void Update()
    {
        if (state == STATE.COOLTIME)
        {
            currDashingCoolTime += Time.deltaTime;
            if (dashingCoolTime <= currDashingCoolTime)
            {
                currDashingCoolTime = 0f;
                state = STATE.WAIT;
            }
        }
    }

    [Task]
    public bool IsPlayerInDashRange()
    {
        if (!playerHealth) playerHealth = FindObjectOfType<PlayerHealth>();
        return Mathf.Abs(playerHealth.transform.position.x - transform.position.x) <= dashingXRange;
    }

    [Task]
    public bool IsDashingCoolTime()
    {
        return state == STATE.COOLTIME;
    }

    [Task]
    public bool IsInDashingState()
    {
        return state == STATE.DASH;
    }

    [Task]
    public bool IsArrivedAtDestination()
    {
        return Mathf.Abs(startXPos - targetXPos) <= Mathf.Abs(startXPos - transform.position.x) ;
    }

    [Task]
    public void StartDash()
    {
        startXPos = transform.position.x;
        targetXPos = playerHealth.transform.position.x;

        state = STATE.DASH;

        Task.current.Succeed();
    }

    [Task]
    public void EndDash()
    {
        state = STATE.COOLTIME;
        monsterMover.Stop();
        Task.current.Succeed();
    }

    [Task]
    public void ProcessDash()
    {
        monsterMover.MoveFront(Mathf.Abs(targetXPos - startXPos) / 0.7f);
        Task.current.Succeed();
    }


}
