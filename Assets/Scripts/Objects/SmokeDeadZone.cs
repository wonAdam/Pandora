using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDeadZone : MonoBehaviour
{
    [SerializeField] LayerMask damagingLayer;
    [SerializeField] int damage;
    public Light zoneLight;
    [SerializeField] float lightOffSpeed;

    private ParticleSystem myVFX;
    private Collider myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        myVFX = GetComponent<ParticleSystem>();
        myCollider = GetComponent<Collider>();

        zoneLight = GetComponentInChildren<Light>();
    }

    public void Stop()
    {
        myVFX.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        myCollider.enabled = false;
        StartCoroutine(LightLinearOff());
    }

    IEnumerator LightLinearOff()
    {
        while (true)
        {
            zoneLight.intensity = Mathf.Clamp(zoneLight.intensity - Time.deltaTime * lightOffSpeed, 0f, Mathf.Infinity);
            yield return null;

            if (zoneLight.intensity == 0f) break;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        Debug.Log(damagingLayer.value);

        if (Mathf.Pow(2, other.gameObject.layer) == damagingLayer.value)
        {
            Debug.Log("Player Dead");
            other.GetComponentInParent<PlayerHealth>().TakeDamage(damage, transform);
        }
    }
}
