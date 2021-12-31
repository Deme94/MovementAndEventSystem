using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatReference {

    public bool useConstant;
    public float constantValue;
    [SerializeField]
    private FloatVariable variable;

    public float Value
    {
        get { return useConstant ? constantValue : variable.value; }
        set { if (useConstant) constantValue = value;
              else variable.value = value; }
    }
}
