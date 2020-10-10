using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCtrl : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    Animator animator;
    float timeSinceDeath = 0f;
    int dirMode = 0; // 0 - direct catre oponent, 1 - il inconjoara prin stanga, 2 - il inconjoara prin dreapta, 3 - se da in spate
    Vector3 surroundDir;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(SeedDirection(2f));
    }

    IEnumerator SeedDirection(float t)
    {
        yield return new WaitForSeconds(t);
        dirMode = Random.Range(0, 4);
        switch (dirMode)
        {
            case 0:
                surroundDir = Vector3.zero;
                break;
            case 1:
                surroundDir = -transform.right;
                break;
            case 2:
                surroundDir = transform.right;
                break;
            case 3:
                surroundDir = -transform.forward * 5f;
                break;
        }
        yield return StartCoroutine(SeedDirection(Random.Range(2f, 4f)));
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position + surroundDir);

        SetAnimatorDirParameters();

        RotateTowardsEnemy();

        Attack();

        VerifyAlive();
    }

    void VerifyAlive()
    {
        if (animator.GetFloat("HP") < 0)
        {
            agent.SetDestination(transform.position);
            timeSinceDeath += Time.deltaTime;
            if (timeSinceDeath > 5f)
            {
                Destroy(gameObject);
            }
        }

    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < 0.7f)
        {// loveste ori cu stanga, ori cu dreapta, aleator
            animator.SetTrigger("Attack" + ((int)UnityEngine.Random.Range(0, 2) == 0 ? "L" : "R"));
        }
    }

    private void RotateTowardsEnemy()
    {//aliniem oponentul cu fata la player
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        dir = dir.normalized;

        if ((transform.forward + dir).magnitude > 0.001f &&
                    (transform.forward - dir).magnitude > 0.001f)
        {
            float theta = Mathf.Acos(Vector3.Dot(transform.forward, dir)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(transform.forward, dir).normalized;
            transform.rotation = Quaternion.AngleAxis(theta / 16f, axis) * transform.rotation;
        }
    }

    private void SetAnimatorDirParameters()
    {
        Vector3 localSpaceVelocity = transform.InverseTransformVector(agent.velocity);
        animator.SetFloat("Horizontal", localSpaceVelocity.x);
        animator.SetFloat("Vertical", localSpaceVelocity.z);
    }
}
