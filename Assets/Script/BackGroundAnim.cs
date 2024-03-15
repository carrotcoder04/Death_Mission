using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAnim : MonoBehaviour
{
    [SerializeField] private Transform cam;

    // Update is called once per frame
    void Update()
    {
        if(cam.transform.position.y > 3.6f)
        {
            cam.transform.position = new Vector3(-20,0,-10);
        }
        else
        {
            cam.transform.position += new Vector3(0,Time.deltaTime,0);
        }
    }
}
