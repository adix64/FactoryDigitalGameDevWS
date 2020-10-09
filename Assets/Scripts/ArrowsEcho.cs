using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsEcho : MonoBehaviour
{// script atasat containerului de sageti
    Transform L, F, R, B; //sagetile

    // Start is called before the first frame update
    void Start()
    {// referentiate in ordinea in care apar in ierarhie, pentru le obtinem dupa indexul copilului
        L = transform.GetChild(0);
        F = transform.GetChild(1);
        R = transform.GetChild(2);
        B = transform.GetChild(3);
    }

    // Update is called once per frame
    void Update()
    {// se activeaza/dezactiveaza fiecara sageata in functie de tasta apasata sau de joystick
        L.gameObject.SetActive(Input.GetAxis("Horizontal") < 0f);
        F.gameObject.SetActive(Input.GetAxis("Vertical") > 0f);
        R.gameObject.SetActive(Input.GetAxis("Horizontal") > 0f);
        B.gameObject.SetActive(Input.GetAxis("Vertical") < 0f);
    }
}
