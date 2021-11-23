using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public Transform attackPoint;

    // Update is called once per frame
    void Update(){
        if (Input.GetKey(KeyCode.Mouse0)){
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
        }
    }
}
