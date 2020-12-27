using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class MonsterAI : MonoBehaviour
{
    public enum Direction
    {
        LEFT, RIGHT
    }
    public Direction dir;

    protected PlayerHealth playerHealth;
    protected MonsterFallDetect mFallDetect;
    protected MonsterWallDetect mWallDetect;
    protected MonsterMover monsterMover;
    protected Animator myAnim;

    protected void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        mFallDetect = GetComponent<MonsterFallDetect>();
        mWallDetect = GetComponent<MonsterWallDetect>();
        monsterMover = GetComponent<MonsterMover>();

        myAnim = GetComponentInChildren<Animator>();

        // 초기 방향을 검사
        float fromRight = Vector3.Angle(Vector3.right, transform.forward);
        float fromLeft = Vector3.Angle(Vector3.left, transform.forward);
        dir = Mathf.Abs(fromRight) > Mathf.Abs(fromLeft) ? Direction.LEFT : Direction.RIGHT;
    }

    [Task]
    protected bool ShouldTurnAround()
    {
        if (dir == Direction.LEFT && Vector3.Angle(Vector3.left, transform.forward) < 0.01f)
        {
            Task.current.debugInfo = string.Format("Angle Left: {0}", Vector3.Angle(Vector3.left, transform.forward));
            return false;
        }
        else if (dir == Direction.RIGHT && Vector3.Angle(Vector3.right, transform.forward) < 0.01f)
        {
            Task.current.debugInfo = string.Format("Angle Left: {0}", Vector3.Angle(Vector3.right, transform.forward));
            return false;
        }
        else
        {
            return true;
        }
    }

    [Task]
    protected void TurnAround()
    {
        // Turn To Left
        if (dir == Direction.LEFT)
        {
            monsterMover.TurnAroundTo(Quaternion.LookRotation(Vector3.left, Vector3.up));
        }
        // Turn To Right
        else
        {
            monsterMover.TurnAroundTo(Quaternion.LookRotation(Vector3.right, Vector3.up));
        }

        Task.current.Succeed();
    }

    [Task]
    protected void WalkFront()
    {
        monsterMover.MoveFront();
        Task.current.Succeed();
    }

    [Task]
    protected bool FallDetect()
    {
        return GetComponent<MonsterFallDetect>().fallDetected;
    }

    [Task]
    protected bool WallDetect()
    {
        return GetComponent<MonsterWallDetect>().wallDetected;
    }

    [Task]
    public bool IsPlayerFront()
    {
        if (!playerHealth)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        return dir == Direction.LEFT ?
            transform.position.x > playerHealth.transform.position.x :
            transform.position.x < playerHealth.transform.position.x;
    }

    [Task]
    protected void ChangeDirection()
    {
        dir = dir == Direction.LEFT ? Direction.RIGHT : Direction.LEFT;
        Task.current.Succeed();
    }

    [Task]
    protected void Idle()
    {
        monsterMover.Stop();
        Task.current.Succeed();
    }
}
