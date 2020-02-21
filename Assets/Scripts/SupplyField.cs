using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyField : PurchasableField
{
    int baseCost;
    
    override protected void Start() {
        baseCost = 10;
        SetOwner(-1);
    }

    override public int GetFee(){
        return baseCost * GameInfo.instance.GetLastRoll() * GameInfo.instance.GetPlayerObject(GetOwner()).GetSuppliesCount();
    }
}
