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

    public void StartTurn(int rollValue, bool diceDouble){
        if(CheckDouble(diceDouble)){
            SendToJail();
            return;
        }

        GetComponent<Pawn>().MovePawn(rollValue, currentField);
        UpdateCurrentField(rollValue);

        if(diceDouble){
            Debug.Log("dice double "+GetDiceDoubleFlag() + " player " +playerNumber);
            SetDiceDoubleFlag(true);
        }
        else{
            SetDiceDoubleFlag(false);
            FieldManagement();
        }

    }
    
    public void EndTurn(){
        GameInfo.instance.ChangePlayer();
    }

    public void UpdateMoney(int value){
        money += value;
    }

    public int GetMoney(){
        return money;
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

    public void SetPlayerNumber(int i){
        playerNumber = i;
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
    
    private bool CheckDouble(bool diceDouble){
        if(diceDouble && diceDoubleFlag){
            return true;
        }
        return false;
    }
    
    public bool GetDiceDoubleFlag(){
        return diceDoubleFlag;
    }

    private void SetDiceDoubleFlag(bool value){
        diceDoubleFlag = value;
    }
    
    private void SendToJail(){
        SetImprisoned(true);
        SetDiceDoubleFlag(false);
        SetCurrentField(11);
        GetComponent<Pawn>().SetPrisonPosition();
        GameInfo.instance.ChangePlayer();
    }

    private void FieldManagement(){
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

    private void PayFee(){
        UI.instance.DisableDiceButton();
        UI.instance.EnableEndTurnButton();

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

    private void ManageSpecial(){
        UI.instance.DisableDiceButton();
        UI.instance.EnableEndTurnButton();
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
