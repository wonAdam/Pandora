using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lantern : MonoBehaviour, IBreakable
{
    [SerializeField] GameObject normalLantern;
    [SerializeField] GameObject brokenLantern;
    [SerializeField] ParticleSystem smokeVFX;
    
    [SerializeField] float explsionForceMin;
    [SerializeField] float explsionForceMax;
    [SerializeField] float explosionRadius;

    [SerializeField] UnityEvent OnBreak;


    private Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }


    public void Break()
    {
        normalLantern.SetActive(false);
        brokenLantern.SetActive(true);
        Rigidbody[] brokenPieces = brokenLantern.transform.GetComponentsInChildren<Rigidbody>();

        foreach (var piece in brokenPieces)
        {
            float randForce = UnityEngine.Random.Range(explsionForceMin, explsionForceMax);
            piece.AddExplosionForce(randForce, transform.position, explosionRadius);
        }

        smokeVFX.Stop(false, ParticleSystemStopBehavior.StopEmitting);

        GetComponent<Collider>().enabled = false;

        //Stop SmokeDeadZone 
        OnBreak.Invoke();


    }
}
