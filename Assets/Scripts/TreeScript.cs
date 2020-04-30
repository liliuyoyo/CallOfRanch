using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    //Variables
    public Transform logs;
    GameObject thisTree;
    Rigidbody rb;
    public int treeHealth = 5;

    private void Start()
    {
        thisTree = this.gameObject;
        rb = thisTree.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = true;
        rb.mass = 100;
        //rb.drag = 5;
    }

    private void Update()
    {
        if(treeHealth <= 0)
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward, ForceMode.Impulse);
            StartCoroutine(destroyTree());
        }
    }

    private IEnumerator destroyTree()
    {
        yield return new WaitForSeconds(4);
        Destroy(thisTree);

        Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        Instantiate(logs, thisTree.transform.position + new Vector3(0, 0, 0) + position, Quaternion.Euler(270f, 0f, 0f));
        Instantiate(logs, thisTree.transform.position + new Vector3(2, 2, 0) + position, Quaternion.Euler(270f, 10f, 0f));
        Instantiate(logs, thisTree.transform.position + new Vector3(5, 5, 0) + position, Quaternion.Euler(270f, 70f, 0f));

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Viking_Axe"))
        {
            treeHealth--;
        }
    }
}
