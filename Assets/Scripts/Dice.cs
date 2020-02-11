using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private float diceOneValue;
    private float diceTwoValue;

    private string diceSpritesFolder = "Sprites/Dice";

    private GameObject diceOneObject;
    private GameObject diceTwoObject;

    private Sprite[] diceFaces;

    private void Start() {
        diceOneObject = GameObject.Find("DiceOne");
        diceTwoObject = GameObject.Find("DiceTwo");
        diceFaces = Resources.LoadAll<Sprite>(diceSpritesFolder);
    }

    void DiceRoll(){
        bool diceDouble = false;
        diceOneValue = Mathf.Floor(Random.Range(1f, 6.999999f));
        diceTwoValue = Mathf.Floor(Random.Range(1f, 6.999999f));

        diceOneObject.GetComponent<Image>().sprite = diceFaces[(int) diceOneValue - 1];
        diceTwoObject.GetComponent<Image>().sprite = diceFaces[(int) diceTwoValue - 1];

        if(diceOneValue == diceTwoValue){
            diceDouble = true;
        }

        PawnController.instance.MovePawn((int)(diceOneValue + diceTwoValue), diceDouble);
    }

    float GetDiceOneValue(){
        return diceOneValue;
    }

    float GetDiceTwoValue(){
        return diceTwoValue;
    }
}
