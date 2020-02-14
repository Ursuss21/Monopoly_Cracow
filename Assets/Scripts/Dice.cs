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

        UI.instance.UpdateDiceSprite(diceOneObject, diceOneValue - 1);
        UI.instance.UpdateDiceSprite(diceTwoObject, diceTwoValue - 1);

        if(diceOneValue == diceTwoValue){
            SetDiceDouble(true);
        }

        Pawn.instance.MovePawn(diceOneValue + diceTwoValue, diceDouble);
    }

    public float GetDiceOneValue(){
        return diceOneValue;
    }

    public float GetDiceTwoValue(){
        return diceTwoValue;
    }

    private bool GetDiceDouble(){
        return diceDouble;
    }

    private void SetDiceDouble(bool value){
        diceDouble = value;
    }
}
