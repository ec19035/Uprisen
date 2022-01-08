using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScript{
    
    public int health;
    public int ran;
    public float[] position;

    public PlayerScript(){

        position = new float[3];
    }


}
