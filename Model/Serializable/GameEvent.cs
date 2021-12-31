using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEvent {

    public delegate void EventAction();
    private event EventAction OnEvent;

    public void Invoke()
    {
        if (OnEvent != null)
            OnEvent();
    }

    public void AddListener(EventAction call)
    {
        OnEvent += call;
    }
}
