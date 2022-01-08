using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int ran = 5;

    void Awake(){
        if (instance != null){
            Debug.Log("zFix Singleton");
        }
        instance = this;
    }

    public int Decrease(){
        ran -= 1;
        return ran;
    }
   
}
