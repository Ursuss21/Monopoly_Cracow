using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public void MovePawn(int rollValue, int currentField){
        SetPawnPosition(rollValue, currentField);
    }

    public Vector3 GetNewPawnPosition(int currentField, int nextField){
        Vector3 position = new Vector3(0, 0, 0);
        for(int i = currentField; i < nextField; ++i){
            if(i%40 > 0 && i%40 < 11){
                position += new Vector3(0, 0, 3.1f);
            }
            else if(i%40 >= 11 && i%40 < 21){
                position += new Vector3(3.1f, 0, 0);
            }
            else if(i%40 >=21 && i%40 < 31){
                position += new Vector3(0, 0, -3.1f);
            }
            else if((i%40 >= 31 && i%40 < 41) || i%40 == 0){
                position += new Vector3(-3.1f ,0, 0);
            }
        }
        return position;
    }

    public void SetPawnPosition(int rollValue, int currentField){
        int currentPlayer = GameInfo.instance.GetCurrentPlayer();
        GameInfo.instance.GetPawn(currentPlayer).transform.position += GetNewPawnPosition(currentField, currentField + rollValue);
    }

    public void SetPrisonPosition(){
        int currentPlayer = GameInfo.instance.GetCurrentPlayer();
        Vector3 position = new Vector3(0, 0, 0);
        switch(currentPlayer){
            case 0:
                position = new Vector3(0.5f, 0, 31.5f);
                break;
            case 1:
                position = new Vector3(-0.5f, 0, 31.5f);
                break;
            case 2:
                position = new Vector3(0.5f, 0, 30.5f);
                break;
            case 3:
                position = new Vector3(-0.5f, 0, 30.5f);
                break;
        }
        GameInfo.instance.GetPawn(currentPlayer).transform.position = position;
    }
}
