using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance { get; set; }
    
    private GameObject playerMoney;
    private GameObject purchasePanel;

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
        diceFaces = Resources.LoadAll<Sprite>(diceSpritesFolder);

        DisablePurchasePanel();
    }

    //Upper account state bar

    public void UpdateAccountState(){
        Debug.Log(""+GameInfo.instance.GetPlayerObject().GetMoney());
        string moneyText = GameInfo.instance.GetPlayerObject().GetMoney().ToString();
        GameObject.Find("Player"+GameInfo.instance.GetCurrentPlayer()+"MoneyText").GetComponent<Text>().text = moneyText;
    }

    //Property menu overlay

    public void EnablePurchasePanel(){
        purchasePanel.SetActive(true);
    }

    public void DisablePurchasePanel(){
        purchasePanel.SetActive(false);
    }

    public void UpdateDiceSprite(GameObject dice, int face){
        dice.GetComponent<Image>().sprite = diceFaces[face];
    }

}
