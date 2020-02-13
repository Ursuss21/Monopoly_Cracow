using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyController : MonoBehaviour
{
    public static PropertyController instance { get; set; }

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void ManageProperties(Player player){
        int propertyNumber = player.GetCurrentField();
        if(GameObject.Find(""+propertyNumber).GetComponent<Property>() != null){
            if(GameObject.Find(""+propertyNumber).GetComponent<Property>().GetOwner() == -1){
                UIController.instance.EnablePropertyPanel();
            }
        }
    }
}
