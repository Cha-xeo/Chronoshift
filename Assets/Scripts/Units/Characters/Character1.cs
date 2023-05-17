using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : BaseChar
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Camera.main.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
