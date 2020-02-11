using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int money;
    private List<Property> properties;

    private int playerNumber;

    private int currentField;

    private void Start() {
        money = 3000;
        properties = new List<Property>();

        currentField = 1;
    }

    public void UpdateMoney(int value){
        money += value;
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
}
