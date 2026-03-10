using System;
using System.ComponentModel;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<onProgressChangedEventArgs> onProgressChanged;

    public class onProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
