using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IBreakable, IPushable
{
    [SerializeField] GameObject normalJar;
    [SerializeField] GameObject brokenJar;
    [SerializeField] float explsionForceMin;
    [SerializeField] float explsionForceMax;
    [SerializeField] float explosionRadius;
    [SerializeField] float breakingImpulseMagnitude;
    [SerializeField] LayerMask unbreakableByLayer;
    [SerializeField] float breakingRelativeVelMagnitude;
    [SerializeField] float pushingVel;

    private Rigidbody myRB;
    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public void Pushed(Vector3 dir)
    {
        myRB.velocity = dir * pushingVel;
    }

    public void Break()
    {
        normalJar.SetActive(false);
        brokenJar.SetActive(true);
        Rigidbody[] brokenPieces = brokenJar.transform.GetComponentsInChildren<Rigidbody>();

        foreach(var piece in brokenPieces)
        {
            float randForce = UnityEngine.Random.Range(explsionForceMin, explsionForceMax);
            piece.AddExplosionForce(randForce, transform.position, explosionRadius);
        }

        Collider[] colliders = GetComponents<Collider>();
        foreach(var c in colliders)
        {
            c.enabled = false;
        }

        //GetComponent<Rigidbody>().isKinematic = true;
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " collision.impulse.magnitude: " + collision.impulse.magnitude);
        Debug.Log(gameObject.name + " collision.relativeVelocity.magnitude: " + collision.relativeVelocity.magnitude);

        //Debug.Log((int)Mathf.Pow(2, collision.collider.gameObject.layer));
        //Debug.Log(unbreakableByLayer.value);
        //Debug.Log(((int)Mathf.Pow(2, collision.collider.gameObject.layer) & unbreakableByLayer.value) == 0);

        if (collision.impulse.magnitude > breakingImpulseMagnitude && 
            collision.relativeVelocity.magnitude > breakingRelativeVelMagnitude &&
            ((int)Mathf.Pow(2, collision.collider.gameObject.layer) & unbreakableByLayer.value) == 0)
        {
            Break();
        }
    }

}
