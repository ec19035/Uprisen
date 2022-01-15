using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public Vector3 lastCheckPoint;
    public float health; // used to keep track of player health 
    public Image HealthBar;

    CharacterController body; // used to control player body

    // Start is called before the first frame update
    void Start(){
        body = GetComponent<CharacterController>(); // used to get character controller applied to player
        lastCheckPoint = this.transform.position;
        health = PlayerStats.instance.playerHealth;
        HealthBar.fillAmount = health/100.0f;
    }

    void Update(){
        health = PlayerStats.instance.playerHealth;
        HealthBar.fillAmount = health/100.0f;
    }

    // checks if the player collides with an object ment for combat "Melee"
    // if the player is attacked by the enemy weapon
    // if health is zero scene is reset
    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Melee"){
            health = PlayerStats.instance.DecreasePlayerHealth(5.0f);
            HealthBar.fillAmount = health/100.0f;
            if(health <= 0.0f){
                health = PlayerStats.instance.IncreasePlayerHealth(100.0f);
                body.enabled = false;
                this.transform.position = lastCheckPoint;
                body.enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "CheckPoint"){
            lastCheckPoint = transform.position;
        } else if (other.gameObject.tag == "Boundary"){
            body.enabled = false;
            this.transform.position = lastCheckPoint;
            body.enabled = true;
        }
    }

}
