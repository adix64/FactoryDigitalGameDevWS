using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speedMultiplier = 1f; // multiplicatorul vitezei de deplasare a personajului
    public Transform cameraTransform; // referinta catre componenta Transform a camerei (luata cu drag & drop...
                                      // ... din ierarhie in slotul None)
   
    // functia apelata o singura data, la initializare
    void Start()
    {
        // cod de initializare variabile
    }

    // apelata de N ori pe secunda, preferabil N >= 60 FPS
    void Update()
    {
        float dx = Input.GetAxis("Horizontal"); // -1 pentru tasta A, 1 pentru tasta D, 0 altfel
        float dz = Input.GetAxis("Vertical"); // -1 pentru tasta S, 1 pentru tasta W, 0 altfel

        //calculam directia de deplasare, ca un vector normalizat (are lungime 1)
        //suma axelor camerei suprapuse(inmultite) cu controalele(dx, dz)
        Vector3 dir = (cameraTransform.right * dx + cameraTransform.forward * dz).normalized;
        dir.y = 0f; // deplasare in planul orizontal (XoZ)
        dir = dir.normalized; // lungime 1 pentru directii!
        Vector3 offset = dir * Time.deltaTime * speedMultiplier; //calculam deplasamentul per frame
        //Time.deltaTime ne ofera framerate independece: daca deplasamentul de la frame la frame nu este proportional
        //cu timpul scurs intre 2 frameuri(deltaTime), viteza de miscare devine dependenta de FPS
        transform.position += offset;//finally, adunam deplasamentul la pozitia personajului
    }
}
