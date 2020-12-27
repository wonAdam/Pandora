using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField] Events e;
    [SerializeField] UnityEvent func;

    private void OnEnable()
    {
        e.Register(this);
    }

    private void OnDisable()
    {
        e.Unregister(this);
    }


    public void Invoke()
    {
        func.Invoke();
    }
}
