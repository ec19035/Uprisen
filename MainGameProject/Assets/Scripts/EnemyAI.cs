using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// Applied To: Boss, Turret, Eye enemy prefabs.
// Purpose: use unitys AI and nav mesh to model enemy AI

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement

    // Health params
    private float health;
    private float maxHealth;
    public Slider HealthBar;

    // Shooting params
    private float shootForce = 100.0f;
    private float wait;
    private float meleeWait;

    // Reference to objects 
    public GameObject bullet;
    private Transform attackPoint;
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.tag != "Turret"){
            agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
            agent.baseOffset = 1; // makes eye enemy float
        } 
        health = EnemyStats.instance.GetHealth(this.gameObject);
        maxHealth = health;
        playerObj = GameObject.Find("Player");
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
        if (other.gameObject.tag == "Melee"){
            health -= PlayerStats.instance.playerStrength;
            HealthBar.value = health/maxHealth;
            if (health <= 0.0f){
                Destroy(this.gameObject);

                if(this.gameObject.tag == "Boss"){
                    int checkFinished = EnemyStats.instance.increaseBosses();
                    if (checkFinished == 4){
                        Difficulty.CutToCredits();
                    }
                }
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
            wait = 2.0f; // cooldown
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // creates new bullets
            Vector3 direction = playerObj.transform.position - attackPoint.position; // direction
            currentBullet.transform.forward = direction.normalized; 
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); // FIRE!
            Destroy(currentBullet, 1f); // removed from scene after !f
        }
    }

}
