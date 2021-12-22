using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent; // Used to model AI movement
    [SerializeField]
    private GameObject player; // ges refernece of player to claculate distance from AI
    private float health;
    [SerializeField]
    private Slider displayHealth;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private Animator animation;
    private float shootForce = 100.0f;
    private float wait;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>(); // gets nav mesh agent
        animation = GetComponent<Animator>();
        agent.autoBraking = true; // slows down near nodes   
        health = 100.0f;
        displayHealth.value = 1.0f;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Melee"){
            health = health - 5.0f;
            if(health <= 0.0f){
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update(){
        displayHealth.value = health/100.0f;
        transform.LookAt(player.transform.position);
        if(Vector3.Distance(this.transform.position, player.transform.position) <= 1f){
            Retreat();
        } else if (Vector3.Distance(this.transform.position, player.transform.position) <= 10f){
            agent.destination = player.transform.position + new Vector3(2,2,2);
            Melee();
        } else if (Vector3.Distance(this.transform.position, player.transform.position) <= 20f){
            agent.destination = player.transform.position + new Vector3(2,2,2);
        } else if (Vector3.Distance(this.transform.position, player.transform.position) > 20f){
            Shoot();
        } 
        
    }

    void Retreat(){
        Debug.Log("Retreat");
        
    }

    void Melee(){
        Debug.Log("Melee");
        animation.SetBool("attacking", true);
    }

    void Shoot(){
        wait -= Time.deltaTime;
        animation.SetBool("attacking", false);
        if (wait < 0){
            wait = 2.0f;
            Debug.Log("Shoot");
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); // creates new bullets
            Vector3 direction = player.transform.position - attackPoint.position;
            currentBullet.transform.forward = direction.normalized; 
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse); // FIRE!

            Destroy(currentBullet, 1f); // removed from scene after !f
        }
    }
}
