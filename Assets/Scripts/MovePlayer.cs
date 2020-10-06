﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speedMultiplier = 1f; // multiplicatorul vitezei de deplasare a personajului
    public Transform cameraTransform; // referinta catre componenta Transform a camerei (luata cu drag & drop...
                                      // ... din ierarhie in slotul None)
    Animator animator;
    Rigidbody rigidbody;
    CapsuleCollider capsule;
    Vector3 initPos;
    // functia apelata o singura data, la initializare
    void Start()
    {
        // cod de initializare variabile
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        initPos = transform.position;
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

        // transform.position += offset;//finally, adunam deplasamentul la pozitia personajului

        HandleRootMotion();

        ApplyRootRotation(dir);

        SetAnimatorMovementParameters(dir);

        HandleJump(dir);
    }

    private void ApplyRootRotation(Vector3 dir)
    {
        if ((transform.forward + dir).magnitude > 0.001f &&
                    (transform.forward - dir).magnitude > 0.001f)
        {
            float theta = Mathf.Acos(Vector3.Dot(transform.forward, dir)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(transform.forward, dir).normalized;
            transform.rotation = Quaternion.AngleAxis(theta / 16f, axis) * transform.rotation;
        }
    }

    private void SetAnimatorMovementParameters(Vector3 dir)
    {
        Vector3 characterSpaceDir = transform.InverseTransformDirection(dir); // trecem din spatiul lume in spatiul personaj
        //ca sa obtinem componentele axelor orizontala si verticala:
        float h = characterSpaceDir.x;
        float v = characterSpaceDir.z;
        //pe care le transmitem la animator, cu damping
        animator.SetFloat("Horizontal", h, 0.25f, Time.deltaTime);
        animator.SetFloat("Vertical", v, 0.25f, Time.deltaTime);
    }

    private void HandleRootMotion()
    {
        float velY = rigidbody.velocity.y; // pastram viteza corpului rigid(simulat de motorul de fizica)pe axa verticala
        rigidbody.velocity = animator.deltaPosition / Time.deltaTime; // ROOT MOTION: distanta intre frameuri impartit la timp
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, velY, rigidbody.velocity.z); // reasignam viteza de cadrul anterior pe Y
    }

    void HandleJump(Vector3 dir)
    {
        if (transform.position.y < -50f) // daca ai cazut in gol, reset la pozitia initiala
            transform.position = initPos;

        if (Input.GetKeyDown(KeyCode.Space)) // daca apasam space, imprima forta in sus pentru saritura
            rigidbody.AddForce((Vector3.up + dir)* 300f);
        
        //raza care ne verifica daca personajul este pe sol sau in aer
        Ray ray = new Ray(transform.position + Vector3.up * 0.3f, Vector3.down); 
        // distanta maxima este mai mare decat distanta dintre originea razei si presupusul sol
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.5f))
        {//pe sol
            animator.SetBool("Jump", false);
        }
        else
        {//in aer
            animator.SetBool("Jump", true);
        }
    }
}
