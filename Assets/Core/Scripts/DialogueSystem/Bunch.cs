using Dialogue_system;
using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public abstract class Bunch : ScriptableObject
{
    [SerializeField] private Bunch _nextBunch;
    [ReadOnly, SerializeField] private Bunch _previousBunch;

    public Bunch NextBunch 
    { 
         get => _nextBunch;        
    }


    protected void OnValidate()
    {
        Bunch[] bunches = Resources.LoadAll<Bunch>("Bunches");

        if (_previousBunch == this)
        {
            _previousBunch = null;
            return;
        }

        if(_nextBunch == this )
        {
            _nextBunch = null;
            return;
        }

        if (_nextBunch == _previousBunch)
        {

        }

        foreach (Bunch bunch in bunches)
        {
            if (bunch._nextBunch == this)
            {
                _previousBunch = bunch;
                return;
            }
        }

        _previousBunch = null;
        return;
    }

    
}
