using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyMiaCanSee : MonoBehaviour
{
    private PlayerStance playerStance;

    public void BeVisible()
    {
        gameObject.SetActive(true);
    }

    public void BeInvisible()
    {
        gameObject.SetActive(false);
    }
}
