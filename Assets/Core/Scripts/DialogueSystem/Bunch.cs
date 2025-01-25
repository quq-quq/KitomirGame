using Dialogue_system;
using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Bunch : ScriptableObject
{
    [SerializeField] private Bunch nextBunch;
    [ReadOnly, SerializeField]
    private Bunch _previousBunch;

#if UNITY_EDITOR
    private void OnValidate()
    {
        Bunch[] bunches = Resources.LoadAll<Bunch>("Bunches");
        foreach (Bunch bunch in bunches)
        {
            if (bunch.nextBunch == this)
            {
                _previousBunch = bunch;
                break;
            }
        }
    }
#endif
}
