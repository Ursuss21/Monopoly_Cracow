using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int money;
    private List<Field> properties;

    private int playerNumber;

    private int currentField;
    private bool imprisoned;
    private int imprisonedTimer;
    private bool diceDoubleFlag;

    private void Start() {
        money = 3000;
        properties = new List<Field>();

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
        SetCurrentField(rollValue);
        FieldManagement();

        if(!diceDouble){
            GameInfo.instance.ChangePlayer();
        }
        else{
            SetDiceDoubleFlag(true);
        }

        UI.instance.UpdateAccountState();
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
        currentField = (currentField + i)%40;
        if(currentField == 0){
            currentField = 40;
        }
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

    private void SetDiceDoubleFlag(bool value){
        diceDoubleFlag = value;
    }
    
    private void SendToJail(){
        int rollValue = 40 - currentField + 11;
        SetImprisoned(true);
        GetComponent<Pawn>().SetPawnPosition(currentField, rollValue);
    }

    private void FieldManagement(){
        Field field = GameObject.Find(""+currentField).GetComponent<Field>();
        field.EnablePurchasePanel();
    }
}
