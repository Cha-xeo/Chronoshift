using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

    public float speed = 1f;
     
    void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.position -= Vector3.left * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.S)) {
            transform.position -= Vector3.up * Time.deltaTime * speed;
        }
    }

}