
using System;
using UnityEngine;

public class TriggerObserver: MonoBehaviour
{
    public Action<Collider> OnEnter;
    private void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke(other);
    }



}

