using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameObject pawnPrefab;
    private int pawnCount = 4;

    private GameObject[] pawns;

    private int[] playerOrder;
    private int currentPlayer;
    private bool diceDoubleFlag = false;

    //Create singleton

    public static Pawn instance { get; set; }

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

        SpawnPawns();

        currentPlayer = playerOrder[0];

        UI.instance.DisablePurchasePanel();
    }

    private void SpawnPawns(){
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        for(int i = 0; i < pawnCount; ++i){
            switch(i){
                case 0:
                    spawnPoint = new Vector3(0.5f, 0, 0.5f);
                    break;
                case 1:
                    spawnPoint = new Vector3(-0.5f, 0, 0.5f);
                    break;
                case 2:
                    spawnPoint = new Vector3(-0.5f, 0, -0.5f);
                    break;
                case 3:
                    spawnPoint = new Vector3(0.5f, 0, -0.5f);
                    break;
            }
            pawns[i] = Instantiate(pawnPrefab, spawnPoint, Quaternion.identity);
            SetPawnProperties(i);
        }
    }

    private void SetPawnProperties(int i){
        SetPawnName(i);
        SetPawnColor(pawns[i], i);
        SetPlayerOrder(i);
    }

    private void SetPawnName(int i){
        pawns[i].name = "Pawn"+i;
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

    private void SetPlayerOrder(int i){
        playerOrder[i] = i;
    }

    public void MovePawn(int rollValue, bool diceDouble){
        Player player = pawns[currentPlayer].GetComponent<Player>();
        int currentField = player.GetCurrentField();

        if(CheckDouble(diceDouble)){
            SendToJail(player);
            return;
        }

        SetPawnPosition(currentField, rollValue);
        player.ChangeCurrentField(rollValue);
        FieldManagement(player);

        if(!diceDouble){
            ChangePlayer();
        }
        else{
            SetDiceDoubleFlag(true);
        }

        UI.instance.UpdateAccountStates(pawns);
    }

    private bool CheckDouble(bool diceDouble){
        if(diceDouble && diceDoubleFlag){
            return true;
        }
        return false;
    }

    private void SetDiceDoubleFlag(bool value){
        diceDoubleFlag = value;
    }

    private void SendToJail(Player player){
        int currentField = player.GetCurrentField();
        int rollValue = 40 - currentField + 11;
        player.SetImprisoned(true);
        pawns[currentPlayer].transform.position += GetNewPawnPosition(currentField, currentField + rollValue);
    }

    private Vector3 GetNewPawnPosition(int currentField, int nextField){
        Vector3 temp = new Vector3(0, 0, 0);
        for(int i = currentField; i < nextField; ++i){
            if(i%40 > 0 && i%40 < 11){
                temp += new Vector3(0, 0, 3.1f);
            }
            else if(i%40 >= 11 && i%40 < 21){
                temp += new Vector3(3.1f, 0, 0);
            }
            else if(i%40 >=21 && i%40 < 31){
                temp += new Vector3(0, 0, -3.1f);
            }
            else if((i%40 >= 31 && i%40 < 41) || i%40 == 0){
                temp += new Vector3(-3.1f ,0, 0);
            }
        }
        return temp;
    }

    private void SetPawnPosition(int currentField, int rollValue){
        pawns[currentPlayer].transform.position += GetNewPawnPosition(currentField, currentField + rollValue);
    }

    private void FieldManagement(Player player){
        int currentField = player.GetCurrentField();
        Field field = GameObject.Find(""+currentField).GetComponent<Field>();
        field.EnablePurchasePanel();
    }

    private void ChangePlayer(){
        ++currentPlayer;
        if(currentPlayer == pawnCount){
            currentPlayer = 0;
        }
    }
}
