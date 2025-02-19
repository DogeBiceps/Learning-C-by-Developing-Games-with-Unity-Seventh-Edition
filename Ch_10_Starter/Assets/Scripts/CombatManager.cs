using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : BaseManager
{
    public override string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public override void Initialize()
    {
        _state = "Tomas Combat Manager initialized..";
        Debug.Log(_state);
    }
}