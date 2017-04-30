using UnityEngine;
using System.Collections;

public class TankAI : MonoBehaviour {

    public GameObject explosion;
    private NavMeshAgent myAgent;
    

	// Use this for initialization
	void Awake()
    {

        myAgent = GetComponent<NavMeshAgent>();
        
	}

    public void SetTarget (Vector3 target) //Enemy spawner calls this when it randomly select a target.
    {
        myAgent.SetDestination(target);
    }

    public void Die()
    {

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, .15f);

    }

}
