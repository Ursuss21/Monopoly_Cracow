using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    [SerializeField]
    private int cost;
    [SerializeField]
    private int mortgageCost;
    [SerializeField]
    private bool isRail;
    [SerializeField]
    private bool isSupplies;

    [SerializeField]
    private int homeCost;
    [SerializeField]
    private int hotelCost;

    [SerializeField]
    private int[] fees = new int[6];
    [SerializeField]
    private int set;

    [SerializeField]
    private string propertyName;

    int owner;
    int buildings;

    private void Start() {
        owner = -1;
        buildings = 0;
    }

    public int GetOwner(){
        return owner;
    }
}
