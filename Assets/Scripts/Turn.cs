using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private Player player;

    public static Turn instance { get; set; }

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void InitializeTurn(Player currentPlayer, int rollValue){
        player = currentPlayer;
        StartTurn(rollValue);
    }

    public void StartTurn(int rollValue){
        if(player.GetImprisoned()){
            PrisonTurn(rollValue);
        }
        else{
            NormalTurn(rollValue);
        }
        Debug.Log("imprisoned "+player.GetImprisoned());
    }

    public void PrisonTurn(int rollValue){
        if(player.GetImprisonedTimer() == 0 || Dice.instance.GetDiceDouble()){
            player.GetOutOfJail();
            NormalTurn(rollValue);
        }
        else{
            player.SetImprisonedTimer(player.GetImprisonedTimer() - 1);
            EndTurn();
        }
    }

    public void NormalTurn(int rollValue){
        if(player.CheckDouble()){
            player.SendToJail();
            return;
        }

        player.GetComponent<Pawn>().MovePawn(rollValue, player.GetCurrentField());
        player.UpdateCurrentField(rollValue);

        if(Dice.instance.GetDiceDouble()){
            Debug.Log("dice double "+player.GetDiceDoubleFlag() + " player " +player.GetPlayerNumber());
            player.SetDiceDoubleFlag(true);
        }
        else{
            player.SetDiceDoubleFlag(false);
            player.FieldManagement();
        }
    }

    public void EndTurn(){
        GameInfo.instance.ChangePlayer();
    }
}
