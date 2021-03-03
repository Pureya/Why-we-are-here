using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour {

    public NavMeshAgent navAgent;
    private Transform currentTarget;

	virtual public void Start()
	{
        navAgent = GetComponent<NavMeshAgent>();
	}

    virtual public void Update()
    {
        if(currentTarget != null)
        {
            navAgent.destination = currentTarget.position;
        }
    }

    public void MoveUnit(Vector3 dest)
    {
        Debug.Log(dest + " " + navAgent);
        currentTarget = null;
        navAgent.destination = dest;
    }

    public void SetSelected(bool isSelected)
    {
        transform.Find("Highlight").gameObject.SetActive(isSelected);
    }

    public void SetNewTarget(Transform enemy)
    {
        currentTarget = enemy;
    }
}
