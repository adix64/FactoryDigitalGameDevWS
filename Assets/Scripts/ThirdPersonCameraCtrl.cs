using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraCtrl : MonoBehaviour
{
    // https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Flight_dynamics_with_text_ortho.svg/1200px-Flight_dynamics_with_text_ortho.svg.png
    float yaw = 0f; // unghiul cu axa verticala a lumii
    float pitch = 0f; // unghiul cu axa orizontala a lumii
    public Transform playerTransform; // referinta la target
    public float distToTarget = 5f; // distanta pana la target
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X"); // adunam deplasamentul orizontal al mouselui
        pitch += Input.GetAxis("Mouse Y"); // adunam deplasamentul vertical al mouselui

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f); // rotatia cu axele lumii
        //pozitionarea camerei in jurul personajului:
        transform.position = playerTransform.position - transform.forward * distToTarget;
            // de la pozitia personajului, ne deplasam in spate distToTarget unitati
    }
}
