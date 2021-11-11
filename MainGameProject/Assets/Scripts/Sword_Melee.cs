using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Melee : MonoBehaviour
{

    Animator animation;

    private void Start()
    {
        animation = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            animation.SetBool("attacking", true);
        else if(Input.GetButtonUp("Fire1"))
            animation.SetBool("attacking", false);
    }
}
