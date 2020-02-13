using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    public GameObject pawnPrefab;
    private int pawnCount = 4;

    private GameObject[] pawns;

    private int[] playerOrder;
    private int currentPlayer;
    private bool diceDoubleFlag = false;

    //Create singleton

    public static PawnController instance { get; set; }

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    //Initialize variables

    private void Start(){
        pawns = new GameObject[pawnCount];
        playerOrder = new int[pawnCount];

        //Set pawns position on board

        float x = 0;
        float y = 0;
        for(int i = 0; i < pawnCount; ++i){
            switch(i){
                case 0:
                    x = 0.5f;
                    y = 0.5f;
                    break;
                case 1:
                    x = -0.5f;
                    y = 0.5f;
                    break;
                case 2:
                    x = -0.5f;
                    y = -0.5f;
                    break;
                case 3:
                    x = 0.5f;
                    y = -0.5f;
                    break;
            }
            pawns[i] = Instantiate(pawnPrefab, new Vector3(x, 0, y), Quaternion.identity);
            SetPawnColor(pawns[i], i);
            pawns[i].name = "Pawn"+i;
            playerOrder[i] = i;
        }
        currentPlayer = playerOrder[0];

        UIController.instance.DisablePropertyPanel();
    }

    private void Update()
    {
        
    }

    private void SetPawnColor(GameObject currentPawn, int colorNumber){
        Renderer[] meshRenderers = currentPawn.GetComponentsInChildren<Renderer>();
        foreach (Renderer meshRenderer in meshRenderers){
            switch(colorNumber){
                case 0:
                    meshRenderer.material.SetColor("_Color", Color.red);
                    break;
                case 1:
                    meshRenderer.material.SetColor("_Color", Color.blue);
                    break;
                case 2:
                    meshRenderer.material.SetColor("_Color", Color.yellow);
                    break;
                case 3:
                    meshRenderer.material.SetColor("_Color", Color.green);
                    break;
            }
        }
    }

    public void MovePawn(int value, bool diceDouble){
        Vector3 temp = new Vector3(0, 0, 0);
        Player player = pawns[currentPlayer].GetComponent<Player>();
        int currentField = player.GetCurrentField();

        //Check if player rolled double after double, if yes send him to prison

        if(diceDouble && diceDoubleFlag){
            if(currentField <= 11){
                value = 11 - currentField;
            }
            else{
                value = 40 - currentField + 11;
            }
            diceDoubleFlag = false;
            diceDouble = false;
        }
        else if(!diceDouble && diceDoubleFlag){
            diceDoubleFlag = false;
        }

        //Check if player went into 31 field (go to jail), if not check if he went through the start and pay him money

        if(currentField + value == 31){
            value += 20;
            player.SetImprisoned(true);
        }
        else if(!diceDouble && !diceDoubleFlag && currentField + value > 40){
            pawns[currentPlayer].GetComponent<Player>().UpdateMoney(400);
        }

        //Move player around the board

        for(int i = currentField; i < currentField + value; ++i){
            if(i%40 > 0 && i%40 < 11){
                temp = new Vector3(0, 0, 3.1f);
            }
            else if(i%40 >= 11 && i%40 < 21){
                temp = new Vector3(3.1f, 0, 0);
            }
            else if(i%40 >=21 && i%40 < 31){
                temp = new Vector3(0, 0, -3.1f);
            }
            else if((i%40 >= 31 && i%40 < 41) || i%40 == 0){
                temp = new Vector3(-3.1f ,0, 0);
            }
            pawns[currentPlayer].transform.position += temp;
        }
        player.ChangeCurrentField(value);

        PropertyController.instance.ManageProperties(player);

        //Check if double has been rolled, if yes do not change player

        if(!diceDouble && !diceDoubleFlag){
            ChangePlayer();
        }
        else{
            diceDoubleFlag = true;
        }

        //Update UI

        UIController.instance.UpdateAccountStates(pawns);
    }

    private void ChangePlayer(){
        ++currentPlayer;
        if(currentPlayer == pawnCount){
            currentPlayer = 0;
        }
    }
}
