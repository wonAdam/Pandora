using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiaCanSeeMgr : MonoBehaviour
{
    private List<OnlyMiaCanSee> miaCanSeeObjects;
    private PlayerStance playerStance;

    private void Start()
    {
        miaCanSeeObjects = FindObjectsOfType<OnlyMiaCanSee>().ToList();
    }

    public void OnStanceChangeEvent()
    {
        if (playerStance == null) playerStance = FindObjectOfType<PlayerStance>();
        if (playerStance == null)
        {
            foreach (var ob in miaCanSeeObjects)
            {
                ob.BeInvisible();
            }

            return;
        }

        if (playerStance.state == PlayerStance.PlayerStanceState.MIA)
        {
            for(int i = 0; i < miaCanSeeObjects.Count; i++)
            {
                miaCanSeeObjects[i].BeVisible();
            }
            
        }
        else
        {
            foreach (var ob in miaCanSeeObjects)
            {
                ob.BeInvisible();
            }
        }

    }


}
