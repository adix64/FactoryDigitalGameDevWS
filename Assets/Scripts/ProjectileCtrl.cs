using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCtrl : MonoBehaviour
{
    public Vector3 dir;
    public float speedMultiplier = 10f;
    Rigidbody rigidbody;
    public string compareTo;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(AutodestroyProjectile(5f));
    }

    IEnumerator AutodestroyProjectile(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, dir) * transform.rotation;
        rigidbody.velocity = dir * speedMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.gameObject.layer == LayerMask.NameToLayer(compareTo))
        {
            Animator opponetAnimator = collision.other.GetComponentInParent<Animator>();
            opponetAnimator.SetTrigger("TakeHit");
            opponetAnimator.SetFloat("HP", opponetAnimator.GetFloat("HP") - 10f);
        }
    }
}
