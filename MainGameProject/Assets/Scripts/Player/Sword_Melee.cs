using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to check if the player clicks to attack
// And plays an animation (that we made)
// of the object being swung

public class Sword_Melee : MonoBehaviour
{
    Animator animation; // gets animation
    public AudioSource audio;
    public AudioClip swingSound;

    private void Start(){
        animation = GetComponent<Animator>(); // gets component
        audio = gameObject.GetComponent<AudioSource>() as AudioSource;
        audio.clip = swingSound;
    }

    // Update is called once per frame
    // Checks if button has been pressed
    private void Update(){
        if(Input.GetButtonDown("Fire1")){
            audio.Play(0);
            animation.SetBool("attacking", true);
        }
        else if(Input.GetButtonUp("Fire1")) // inverse carse, resets position
            animation.SetBool("attacking", false);
    }
}
