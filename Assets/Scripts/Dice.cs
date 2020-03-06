using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private int diceOneValue;
    private int diceTwoValue;

    private GameObject diceOneObject;
    private GameObject diceTwoObject;

    bool diceDouble;

    public static Dice instance { get; set; }

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    
    private void Start() {
        diceOneObject = GameObject.Find("DiceOne");
        diceTwoObject = GameObject.Find("DiceTwo");
        diceDouble = false;
    }

    private int GetDiceRollValue(){
        return (int) Mathf.Floor(Random.Range(1f, 6.999999f));
    }

    public void DiceRoll(){
        SetDiceDouble(false);
        diceOneValue = GetDiceRollValue();
        diceTwoValue = GetDiceRollValue();

        UpdateDicesSprites();

        if(diceOneValue == diceTwoValue){
            SetDiceDouble(true);
        }

        InitializePlayerTurn();
    }

    private void InitializePlayerTurn(){
        Player player = GameInfo.instance.GetCurrentPlayerObject();
        GameInfo.instance.SetLastRoll(diceOneValue+diceTwoValue);
        Turn.instance.InitializeTurn(player, diceOneValue + diceTwoValue);
    }

    public float GetDiceOneValue(){
        return diceOneValue;
    }

    public float GetDiceTwoValue(){
        return diceTwoValue;
    }

    public bool GetDiceDouble(){
        return diceDouble;
    }

    private void SetDiceDouble(bool value){
        diceDouble = value;
    }

    private void UpdateDicesSprites(){
        UI.instance.UpdateDiceSprite(diceOneObject, diceOneValue - 1);
        UI.instance.UpdateDiceSprite(diceTwoObject, diceTwoValue - 1);
    }
}
