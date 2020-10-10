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
    public float minPitch = -10f, maxPitch = 45f;
    
    public float sensitivityX = 1f;
    public float sensitivityY = 2f; //se misca de 2 ori mai repede mouse-ul pe verticala
    
    Vector3 cameraOffset;
    public Vector3 overTheShoulder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {



        yaw += Input.GetAxis("Mouse X") * sensitivityX; // adunam deplasamentul orizontal al mouselui
        pitch -= Input.GetAxis("Mouse Y") * sensitivityY; // adunam deplasamentul vertical al mouselui(non invert Y)

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f); // rotatia cu axele lumii

        //calculam offset over the shoulder look pentru aiming
        if (Input.GetKey(KeyCode.LeftShift))
            cameraOffset = Vector3.Lerp(cameraOffset, overTheShoulder, 0.25f);
        else
            cameraOffset = Vector3.Lerp(cameraOffset, Vector3.zero, 0.25f);
        //pozitionarea camerei in jurul personajului:
        transform.position = playerTransform.position - transform.forward * distToTarget + transform.TransformDirection(cameraOffset);
            // de la pozitia personajului, ne deplasam in spate distToTarget unitati
    }
}
