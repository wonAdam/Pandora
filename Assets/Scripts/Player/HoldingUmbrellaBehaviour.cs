﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingUmbrellaBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<PlayerUmbrella>().UmbrellaFolding(false);
        animator.GetComponentInParent<PlayerUmbrella>().PlayUmbrellaUnfoldSoundOnce();
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<PlayerUmbrella>().UmbrellaFolding(true);
    }

}
