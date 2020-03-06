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

        HidePurchasePanel();
        HideEndTurnButton();
    }

    public void UpdateAccountState(){
        string moneyText;
        for(int i = 0; i < GameInfo.instance.GetPawnCount(); ++i){
            moneyText = GameInfo.instance.GetPlayerObject(i).GetMoney().ToString();
            GameObject.Find("Player"+i+"MoneyText").GetComponent<Text>().text = moneyText;
        }
    }

    public void ShowPurchasePanel(){
        purchasePanel.SetActive(true);
    }

    public void HidePurchasePanel(){
        purchasePanel.SetActive(false);
    }

    public void ShowEndTurnButton(){
        endTurnButton.SetActive(true);
    }

    public void HideEndTurnButton(){
        endTurnButton.SetActive(false);
    }
    
    public void DisableDiceButton(){
        diceButton.GetComponent<Button>().interactable = false;
    }

    public void EnableDiceButton(){
        diceButton.GetComponent<Button>().interactable = true;
    }

    public void UpdateDiceSprite(GameObject dice, int face){
        dice.GetComponent<Image>().sprite = diceFaces[face];
    }

    public void BuyProperty(){
        GameInfo.instance.GetCurrentPlayerObject().AddProperty();
        HidePurchasePanel();
        UpdateAccountState();
    }

    public void EndTurn(){
        Turn.instance.EndTurn();
        EnableDiceButton();
        HideEndTurnButton();
    }
}
