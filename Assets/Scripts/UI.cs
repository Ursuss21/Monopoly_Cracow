﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance { get; set; }
    
    private GameObject playerMoney;
    private GameObject propertyPanel;

    private string diceSpritesFolder = "Sprites/Dice";
    private Sprite[] diceFaces;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start() {
        playerMoney = GameObject.Find("PlayerMoney");
        propertyPanel = GameObject.Find("PropertyOptions");
        diceFaces = Resources.LoadAll<Sprite>(diceSpritesFolder);
    }

    //Upper account state bar

    public void UpdateAccountStates(GameObject[] players){
        Debug.Log(""+players[0].GetComponent<Player>().GetMoney());
        string moneyText;
        for(int i = 0; i < players.Length; ++i){
            moneyText = players[i].GetComponent<Player>().GetMoney().ToString();
            GameObject.Find("Player"+i+"MoneyText").GetComponent<Text>().text = moneyText;
        }
    }

    //Property menu overlay

    public void EnablePropertyPanel(){
        propertyPanel.SetActive(true);
    }

    public void DisablePropertyPanel(){
        propertyPanel.SetActive(false);
    }

    public void UpdateDiceSprite(GameObject dice, int face){
        dice.GetComponent<Image>().sprite = diceFaces[face];
    }

}