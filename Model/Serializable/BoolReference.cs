using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoolReference {

    public bool useConstant;
    public bool constantValue;
    [SerializeField]
    private BoolVariable variable;

    public bool Value
    {
        get { return useConstant ? constantValue : variable.value; }
        set
        {
            if (useConstant) constantValue = value;
            else variable.value = value;
        }
    }
}
