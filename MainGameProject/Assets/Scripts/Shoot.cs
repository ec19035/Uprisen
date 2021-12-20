using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Used to shoot magic
public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public Transform attackPoint;
    public float wait;

    // Update is called once per frame
    void Update(){
        wait -= Time.deltaTime;
        if (Input.GetButton("Fire1") && 0 > wait && PauseMenu.gamePaused == false){
            wait = 2.0f; // delay between shots

            Ray ray = new Ray(attackPoint.position, attackPoint.forward); // create scope
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75);

            Vector3 direction = targetPoint - attackPoint.position; // scope

            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // creates new bullets
            currentBullet.transform.forward = direction.normalized; 
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); // FIRE!

            Destroy(currentBullet, 1f); // removed from scene after !f
        }

    }

}
