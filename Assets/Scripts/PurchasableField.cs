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

    protected int owner;

    override protected void Start() {
        
    }

    public int GetOwner(){
        return owner;
    }

    public void SetOwner(int newOwner){
        owner = newOwner;
    }

    override public void EnablePurchasePanel(){
        if(owner == -1){
            UI.instance.EnablePurchasePanel();
        }
    }
}
