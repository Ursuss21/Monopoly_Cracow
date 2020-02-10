using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int money;
    private List<Property> properties;

    private void Start() {
        money = 3000;
        properties = new List<Property>();
    }

    void UpdateMoney(int value){
        money += value;
    }

}
