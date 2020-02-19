using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceField : Field
{
    override protected void Start() {
        SetOwner(-2);
    }
}
