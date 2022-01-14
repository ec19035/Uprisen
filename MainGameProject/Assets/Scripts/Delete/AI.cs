using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Applied to: Enemies
// Purpose: Generate AI movement
public class AI : MonoBehaviour{

    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement
    [SerializeField]
    private GameObject player; // gets refernece of player to calculate distance from AI
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private Animator animation;
    public GameObject bullet;

    private float shootForce = 100.0f;
    private float wait;
    private float meleeWait;

    private float health;
    private float strength;
    
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
        animation = GetComponent<Animator>();
        meleeWait = 3.0f;
        //health = EnemyStats.instance.GetHealth(this.gameObject);
        //strength = EnemyStats.instance.GetStrength(this.gameObject);
    }

    // Update is called once per frame
    void Update(){
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        Move();
        transform.LookAt(player.transform.position);
        if (distance <= 10){
            meleeWait -= Time.deltaTime;
            if (meleeWait < 0){
                Melee();
                meleeWait = 3.0f;
            }
        } else if (distance <= 20){
            Choose();
        } else if (distance > 20){
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Melee"){
            health = health - 5.0f;
            if(health <= 0.0f){
                Destroy(this.gameObject);
            }
        }
    }

    void Melee(){
        Debug.Log("Melee");
        animation.SetBool("attacking", true);
    }

    void Choose(){
        Debug.Log("Choose");
    }

    void Move(){
        //Positions AI in front of player
        //With unity world axis, AI will go to different sides of the player - creating unpredictable movement
        agent.destination = player.transform.position + new Vector3(0,2,2); 
    }

    void Shoot(){
        wait -= Time.deltaTime;
        if (wait < 0){
            wait = 2.0f;
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // creates new bullets
            Vector3 direction = player.transform.position - attackPoint.position;
            currentBullet.transform.forward = direction.normalized; 
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); // FIRE!
            Destroy(currentBullet, 1f); // removed from scene after !f
        }
    }
}
