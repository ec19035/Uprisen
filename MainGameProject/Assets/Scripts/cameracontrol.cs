using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private bool zoomed;
    private Vector3 offset;
    private Vector3 finalOffset;
    // Start is called before the first frame update
    void Start(){
        offset = transform.position - player.transform.position;
        finalOffset = offset;
        zoomed = false;
    }

    void Update(){
        zoom();
    }

    void zoom(){
        if (Input.GetMouseButtonDown(1)){
            if (zoomed){
                zoomed = false;
                finalOffset.y = offset.y;
                finalOffset.z = offset.z;
            } else {
                zoomed = true;
                finalOffset.y = offset.y / 2;
                finalOffset.z = offset.z / 2;
            }
        }
    }

    void LateUpdate(){
        rotate();
        transform.position = Vector3.Lerp(transform.position, (player.transform.position + finalOffset), 0.23f);
        transform.LookAt(player.transform.position);
    }

    void rotate(){
        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4.0f, Vector3.up) * finalOffset;
    }
}
