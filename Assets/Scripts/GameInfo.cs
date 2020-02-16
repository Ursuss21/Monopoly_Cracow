using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{    
    private int pawnCount = 4;
    private GameObject[] pawns;

    private int[] playerOrder;
    private int currentPlayer;

    public static GameInfo instance { get; set; }

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start() {
        pawns = new GameObject[pawnCount];
        playerOrder = new int[pawnCount];

        currentPlayer = playerOrder[0];
    }

    public GameObject GetPawn(int index){
        return pawns[index];
    }

    public void AddPawn(GameObject pawn, int index){
        pawns[index] = pawn;
    }

    public int GetCurrentPlayer(){
        return currentPlayer;
    }

    public void SetCurrentPlayer(int nextPlayer){
        currentPlayer = nextPlayer;
    }
    
    public void SetPlayerOrder(int index){
        playerOrder[index] = index;
    }

    public Player GetPlayerObject(){
        return pawns[currentPlayer].GetComponent<Player>();
    }

    public int GetPawnCount(){
        return pawnCount;
    }
    
    public void ChangePlayer(){
        ++currentPlayer;
        if(currentPlayer == pawnCount){
            currentPlayer = 0;
        }
    }
}
