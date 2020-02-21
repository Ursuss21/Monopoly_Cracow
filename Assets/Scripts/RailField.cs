using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailField : PurchasableField
{
    [SerializeField]
    protected int[] fees = new int[4];

    override protected void Start() {
        SetOwner(-1);
    }

    override public int GetFee(){
        return fees[GameInfo.instance.GetPlayerObject(GetOwner()).GetRailsCount() - 1];
    }
}
