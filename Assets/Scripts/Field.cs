using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    protected int fieldNumber;

    protected int owner;

    virtual protected void Start() {
        
    }

    public int GetOwner(){
        return owner;
    }

    public void SetOwner(int newOwner){
        owner = newOwner;
    }

    virtual public void EnablePurchasePanel(){
        UI.instance.DisableDiceButton();
        UI.instance.ShowEndTurnButton();
    }

    virtual public int GetCost(){
        return 0;
    }
    
    virtual public int GetFee(){
        return 0;
    }
}
