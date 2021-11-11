using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
