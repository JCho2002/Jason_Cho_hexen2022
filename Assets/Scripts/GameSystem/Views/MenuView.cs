using System;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public EventHandler PlayClicked;

    public void Play()
        => OnPlayClicked(EventArgs.Empty);

    protected virtual void OnPlayClicked(EventArgs eventArgs)
    {
        var handler = PlayClicked;
        handler?.Invoke(this, eventArgs);
    }
}
