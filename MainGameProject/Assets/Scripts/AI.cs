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
    //private bool inCombat;
    [SerializeField]
    private Transform attackPoint;
    private float shootForce = 100.0f;
    private float wait;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
    }

    // Update is called once per frame
    void Update(){
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        Move();
        if (distance > 20){
            //Shoot();
        }
    }

    void OnTriggerEnter(Collider other){
        
       // force is how forcefully we will push the player away from the enemy.
        float force = 20;
    
        // If the object we hit is the enemy
        if (other.tag == "Melee"){
            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.gameObject.transform.position - this.transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir*force, ForceMode.Impulse);
        }
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
