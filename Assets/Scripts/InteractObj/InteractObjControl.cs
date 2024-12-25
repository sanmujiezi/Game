using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;


public class TriggerInteractCommand : AbstractCommand
{
    private Collider collider;
    public TriggerInteractCommand(Collider collider)
    {
        this.collider = collider;
    }
    protected override void OnExecute()
    {
        this.SendEvent(new TraggerInteractEvent(){ _collider = collider});
    }
}

public struct TraggerInteractEvent
{
    public Collider _collider;
}

public class InteractObjControl : MonoBehaviour,IController
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.SendCommand(new TriggerInteractCommand(other));
            Destroy(gameObject);
        }
    }

    public IArchitecture GetArchitecture()
    {
        return PlayerControlArchitecture.Interface;
    }
}
