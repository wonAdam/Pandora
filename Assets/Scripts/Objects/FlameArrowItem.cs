using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameArrowItem : Item
{


    [SerializeField] float regenerateSec;
    private Collider myCollider;
    private Renderer myRenderer;
    public bool tiktoking = false;
    public float tiktokSec = 0f;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
        myRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (tiktoking)
        {
            tiktokSec += Time.deltaTime;
            if (tiktokSec >= regenerateSec)
            {
                tiktokSec = 0f;
                tiktoking = false;
                SetVisible(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Consume(other.transform);
            tiktoking = true;
            SetVisible(false);
        }
    }


    private void SetVisible(bool on)
    {
        myCollider.enabled = on;
        myRenderer.enabled = on;
    }

    public override void Consume(Transform player)
    {
        PlayerArcher playerArcher = player.GetComponentInParent<PlayerArcher>();
        playerArcher.isFlame = true;
    }
}
