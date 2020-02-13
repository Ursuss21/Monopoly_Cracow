using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance { get; set; }
    
    private GameObject playerMoney;
    private GameObject propertyPanel;

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
    }

    //Upper account state bar

    public void UpdateAccountStates(GameObject[] players){
        Debug.Log(""+players[0].GetComponent<Player>().GetMoney());
        for(int i = 0; i < players.Length; ++i){
            GameObject.Find("Player"+i+"MoneyText").GetComponent<Text>().text = players[i].GetComponent<Player>().GetMoney().ToString();
        }
    }

    //Property menu overlay

    public void EnablePropertyPanel(){
        Debug.Log("anankin");
        propertyPanel.SetActive(true);
    }

    public void DisablePropertyPanel(){
        propertyPanel.SetActive(false);
    }


}
