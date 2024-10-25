using System;

using UnityEngine;

public class AnimationObserver : MonoBehaviour
{
    public event EventHandler<AnimationEventArgs> Alerted;

    public void AlertObservers(string message)
    {
        Alerted?.Invoke(this, new AnimationEventArgs
        {
            SourceObject = gameObject,
            Message = message
        });
    }
}


public class AnimationEventArgs : EventArgs
{
    public GameObject SourceObject;
    public string Message;
}
