using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitStopwatch : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("timeSinceLastHit", animator.GetFloat("timeSinceLastHit") + Time.deltaTime);
    }
}
