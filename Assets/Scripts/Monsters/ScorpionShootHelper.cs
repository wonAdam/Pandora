using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionShootHelper : MonoBehaviour
{
    private ScorpionAI scorpionAI;
    // Start is called before the first frame update
    void Start()
    {
        scorpionAI = GetComponentInParent<ScorpionAI>();
    }

    public void ShootThroneAnimationEventHelper()
    {
        scorpionAI.ShootThrone();
    }
}
