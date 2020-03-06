using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int money;
    private List<GameObject> properties;

    private int playerNumber;

    private int currentField;
    private bool imprisoned;
    private int imprisonedTimer;
    private bool diceDoubleFlag;

    private void Start() {
        money = 3000;
        properties = new List<GameObject>();

        currentField = 1;
        imprisoned = false;
        imprisonedTimer = 0;
        diceDoubleFlag = false;
    }
    
    public void SendToJail(){
        SetImprisoned(true);
        SetImprisonedTimer(2);
        SetDiceDoubleFlag(false);
        SetCurrentField(11);
        GetComponent<Pawn>().SetPrisonPosition();
        Turn.instance.EndTurn();
    }

    public void GetOutOfJail(){
        SetImprisonedTimer(0);
        SetImprisoned(false);
    }

    public void SetImprisoned(bool prison){
        Debug.Log("prison");
        imprisoned = prison;
    }

    public bool GetImprisoned(){
        return imprisoned;
    }

    public void SetImprisonedTimer(int value){
        imprisonedTimer = value;
    }

    public int GetImprisonedTimer(){
        return imprisonedTimer;
    }

    public bool CheckDouble(){
        if(Dice.instance.GetDiceDouble() && GetDiceDoubleFlag()){
            return true;
        }
        return false;
    }
    
    public bool GetDiceDoubleFlag(){
        return diceDoubleFlag;
    }

    public void SetDiceDoubleFlag(bool value){
        diceDoubleFlag = value;
    }

    public int GetMoney(){
        return money;
    }

    public void UpdateMoney(int value){
        money += value;
    }

    public int GetCurrentField(){
        return currentField;
    }

    public void SetCurrentField(int i){
        currentField = i;
    }

    public void UpdateCurrentField(int i){
        currentField = (currentField + i)%40;
        if(currentField == 0){
            currentField = 40;
        }
    }

    public int GetPlayerNumber(){
        return playerNumber;
    }

    public void SetPlayerNumber(int i){
        playerNumber = i;
    }
    
    public void FieldManagement(){
        Field field = GameObject.Find(""+currentField).GetComponent<Field>();
        if(field.GetOwner() == -1){
            field.EnablePurchasePanel();
        }
        else if(field.GetOwner() != -2){
            PayFee();
        }
        else{
            ManageSpecial();
        }
    }

    public void AddProperty(){
        GameObject field = GameObject.Find(""+currentField);
        properties.Add(field);
        field.GetComponent<Field>().SetOwner(GameInfo.instance.GetCurrentPlayer());
        UpdateMoney(-field.GetComponent<Field>().GetCost());
    }

    public void PayFee(){
        UI.instance.DisableDiceButton();
        UI.instance.ShowEndTurnButton();

        Field field = GameObject.Find(""+currentField).GetComponent<Field>();

        int fee = field.GetFee();
        UpdateMoney(-fee);
        
        int otherIndex = field.GetOwner();
        GameObject other = GameInfo.instance.GetPawn(otherIndex);
        other.GetComponent<Player>().UpdateMoney(fee);
        GameInfo.instance.SetPawn(other, otherIndex);
        
        UI.instance.UpdateAccountState();
        Debug.Log("owner " + field.GetOwner() + " fee " + fee);
    }

    public void ManageSpecial(){
        UI.instance.DisableDiceButton();
        UI.instance.ShowEndTurnButton();
    }

    public int GetRailsCount(){
        int count = 0;
        foreach(GameObject g in properties){
            if(g.GetComponent<RailField>() != null){
                ++count;
            }
        }
        return count;
    }

    public int GetSuppliesCount(){
        int count = 0;
        foreach(GameObject g in properties){
            if(g.GetComponent<SupplyField>() != null){
                ++count;
            }
        }
        return count;
    }
}
