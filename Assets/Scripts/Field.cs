using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private int fieldNumber;

    [SerializeField]
    private int cost;
    [SerializeField]
    private int mortgageCost;

    [SerializeField]
    private int homeCost;
    [SerializeField]
    private int hotelCost;

    [SerializeField]
    private int[] fees = new int[6];
    [SerializeField]
    private int set;

    [SerializeField]
    private bool isRail = false;
    [SerializeField]
    private bool isSupplies = false;
    [SerializeField]
    private bool isChance = false;
    [SerializeField]
    private bool isTreasure = false;
    [SerializeField]
    private bool isSpecial = false;

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

    public void CheckAvailability(){
        if(!isChance && !isTreasure && !isSpecial){
            if(owner == -1){
                UI.instance.EnablePurchasePanel();
            }
        }
    }
}
