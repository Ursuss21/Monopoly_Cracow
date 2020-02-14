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

    private void Start() {
        money = 3000;
        properties = new List<Field>();

        currentField = 1;
        imprisoned = false;
        imprisonedTimer = 0;
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

    public void ChangeCurrentField(int i){
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
}
