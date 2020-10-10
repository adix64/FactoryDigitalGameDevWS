using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public string compareTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Animator opponetAnimator = other.GetComponentInParent<Animator>();
        if (opponetAnimator.GetFloat("timeSinceLastHit") > 0.5f &&
            other.gameObject.layer == LayerMask.NameToLayer(compareTo))
        {
            opponetAnimator.SetTrigger("TakeHit");
            opponetAnimator.SetFloat("HP", opponetAnimator.GetFloat("HP") - 10f);

        }
    }
}
