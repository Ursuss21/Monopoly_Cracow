using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasableField : Field
{
    [SerializeField]
    protected int cost;
    [SerializeField]
    protected int mortgageCost;

    [SerializeField]
    protected int set;
    [SerializeField]
    protected string propertyName;

    override protected void Start() {

    }

    override public void EnablePurchasePanel(){
        if(owner == -1){
            UI.instance.DisableDiceButton();
            UI.instance.EnableEndTurnButton();
            UI.instance.EnablePurchasePanel();
        }
    }

    override public int GetCost(){
        return cost;
    }

    override public int GetFee(){
        return 0;
    }
}
