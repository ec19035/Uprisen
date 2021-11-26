using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public Transform attackPoint;
    public float wait;

    // Update is called once per frame
    void Update(){
        wait -= Time.deltaTime;
        if (Input.GetButton("Fire1") && 0 > wait){
            wait = 2.0f;
            Ray ray = new Ray(attackPoint.position, attackPoint.forward);
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75);

            Vector3 direction = targetPoint - attackPoint.position;

            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            currentBullet.transform.forward = direction.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

            Destroy(currentBullet, 1f);
        }

    }

}
