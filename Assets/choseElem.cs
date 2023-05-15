using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choseElem : MonoBehaviour
{
    public float rotationSpeed = 10f; // La vitesse de rotation

    void Update()
    {
        // Si le bouton est appuyé, le GameObject tourne
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}