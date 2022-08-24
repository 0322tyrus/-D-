using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    //public float speed = 0.01f;
    //public GameObject damageeffect;
    //Vector3 SlimePos;
    //Rigidbody rigid;

    void Start()
    {
        // SlimePos = transform.position;
        // rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = target.position;

        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 0.3f);

        //  transform.position += transform.forward * this.speed;


        // rigid.MovePosition(transform.position + transform.forward * Time.deltaTime);
        // transform.rotation = Quaternion.LookRotation(transform.position - SlimePos);

        //  SlimePos = transform.position;
        //transform.rotation = Quaternion.LookRotation(new Vector3(0, 90, 0));
    }
}