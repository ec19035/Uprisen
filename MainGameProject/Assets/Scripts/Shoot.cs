using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Used to shoot magic
public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce = 5.0f;
    public Transform attackPoint;
    public float wait;
    public Image image;
    public Sprite[] elements;
    public int pos = 0;
    public float mana; // used to keep track of player health 
    public Image ManaBar;

    void Start(){
        mana = PlayerStats.instance.playerMana;
        ManaBar.fillAmount = mana/100.0f;
    }


    // Update is called once per frame
    void Update(){
        wait -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J) && 0 > wait && PauseMenu.gamePaused == false && mana >= 10.0f){
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

            mana = PlayerStats.instance.DecreasePlayerMana(10.0f);
            ManaBar.fillAmount = mana/100.0f;
            Destroy(currentBullet, 0.5f); // removed from scene after !f
        }

        if (Input.GetKeyDown(KeyCode.E)){
            int limit = EnemyStats.instance.bosses;
            if (pos + 1 > limit){
                pos = 0;
            } else if (pos + 1 == limit){
                pos += 1;
            } else {
                pos += 1;
            }
            if (pos == 3){
                pos = 0;
            }
            image.sprite = elements[pos];
        }
    }

    void LateUpdate(){
        if (Input.GetKeyDown("3")){
            mana = PlayerStats.instance.playerMana;
            ManaBar.fillAmount = mana/100.0f;
        }
    }

}
