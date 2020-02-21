using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance { get; set; }
    
    private GameObject playerMoney;
    private GameObject purchasePanel;
    private GameObject endTurnButton;
    private GameObject diceButton;

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
        purchasePanel = GameObject.Find("PurchaseOptions");
        endTurnButton = GameObject.Find("EndTurnButton");
        diceButton = GameObject.Find("DiceButton");
        diceFaces = Resources.LoadAll<Sprite>(diceSpritesFolder);

        DisablePurchasePanel();
        DisableEndTurnButton();
    }

    public void UpdateAccountState(){
        string moneyText;
        for(int i = 0; i < GameInfo.instance.GetPawnCount(); ++i){
            moneyText = GameInfo.instance.GetPlayerObject(i).GetMoney().ToString();
            GameObject.Find("Player"+i+"MoneyText").GetComponent<Text>().text = moneyText;
        }
    }

    public void EnablePurchasePanel(){
        purchasePanel.SetActive(true);
    }

    public void DisablePurchasePanel(){
        purchasePanel.SetActive(false);
    }

    public void EnableEndTurnButton(){
        endTurnButton.SetActive(true);
    }

    public void DisableEndTurnButton(){
        endTurnButton.SetActive(false);
    }

    public void EnableDiceButton(){
        diceButton.SetActive(true);
    }
    
    public void DisableDiceButton(){
        diceButton.SetActive(false);
    }

    public void UpdateDiceSprite(GameObject dice, int face){
        dice.GetComponent<Image>().sprite = diceFaces[face];
    }

    public void BuyProperty(){
        GameInfo.instance.GetCurrentPlayerObject().AddProperty();
        DisablePurchasePanel();
        UpdateAccountState();
    }

    public void EndTurn(){
        GameInfo.instance.GetCurrentPlayerObject().EndTurn();
        EnableDiceButton();
        DisableEndTurnButton();
    }
}
