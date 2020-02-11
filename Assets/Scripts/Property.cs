using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    [SerializeField]
    int cost;
    [SerializeField]
    int mortgageCost;
    [SerializeField]
    bool isRail;
    [SerializeField]
    bool isSupplies;

    [SerializeField]
    int homeCost;
    [SerializeField]
    int hotelCost;

    [SerializeField]
    int[] fees = new int[6];
    [SerializeField]
    int set;

    [SerializeField]
    string propertyName;

    int owner;

    private void Start() {
        
    }
}
