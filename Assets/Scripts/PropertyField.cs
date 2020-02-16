using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyField : PurchasableField
{
    [SerializeField]
    protected int homeCost;
    [SerializeField]
    protected int hotelCost;

    [SerializeField]
    protected int[] fees = new int[6];

    protected int buildings;
    
    override protected void Start() {
        buildings = 0;
        SetOwner(-1);
    }
}
