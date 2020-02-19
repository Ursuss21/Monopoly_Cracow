using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject pawnPrefab;

    public static ObjectSpawner instance { get; set; }

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        SpawnPawns();
    }

    public void SpawnPawns(){
        Vector3 spawnPoint = new Vector3(0, 0, 0);
        int pawnCount = GameInfo.instance.GetPawnCount();
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
            GameInfo.instance.AddPawn(Instantiate(pawnPrefab, spawnPoint, Quaternion.identity), i);
            SetPawnProperties(i);
        }
    }
    
    private void SetPawnProperties(int i){
        SetPawnName(i);
        SetPawnColor(GameInfo.instance.GetPawn(i), i);
        SetPlayerOrder(i);
    }

    private void SetPawnName(int i){
        GameInfo.instance.GetPawn(i).name = "Pawn"+i;
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
        GameInfo.instance.SetPlayerOrder(i);
    }
}
