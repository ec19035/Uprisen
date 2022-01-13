using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement

    private float health;

    private float shootForce = 100.0f;
    private float wait;
    private float meleeWait;

    public GameObject bullet;
    private Transform attackPoint;

    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag != "Turret"){
            agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
            agent.baseOffset = 1;
        } 
        health = EnemyStats.instance.GetHealth(this.gameObject);
        playerObj = GameObject.Find("Player");
        bullet = (GameObject)Resources.Load("bullet", typeof(GameObject));
        attackPoint = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, playerObj.transform.position);
        if (this.gameObject.tag != "Turret" && distance < 20f){
            Move();
        } else if (distance < 30f){
            Shoot();
        }
    }

    void OnCollisionEnter(Collision other){
        if(this.gameObject.tag == "Boss" && other.gameObject.tag == "Melee"){
            Destroy(this.gameObject);
            int checkFinished = EnemyStats.instance.increaseBosses();
            if (checkFinished == 4){
                Difficulty.CutToCredits();
            }
        }
    }

    void Move(){
        //Positions AI in front of player
        //With unity world axis, AI will go to different sides of the player - creating unpredictable movement
        agent.destination = playerObj.transform.position + new Vector3(0,2,2); 
    }

    void Shoot(){
        wait -= Time.deltaTime;
        if (wait < 0){
            wait = 2.0f;
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // creates new bullets
            Vector3 direction = playerObj.transform.position - attackPoint.position;
            currentBullet.transform.forward = direction.normalized; 
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); // FIRE!
            Destroy(currentBullet, 1f); // removed from scene after !f
        }
    }

}
