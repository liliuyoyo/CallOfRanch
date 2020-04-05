using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cropping : MonoBehaviour
{
    //Variables
    public GameObject axe;
    private bool isEquiped = false;
    private int rayCastLength = 10;

    private void Update()
    {
        if(!axe.activeSelf && Input.GetKeyDown(KeyCode.Alpha0))
        {
            isEquiped = true;
            axe.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            isEquiped = false;
            axe.SetActive(false);
        }

        //Raycast
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, fwd, out hit, rayCastLength))
        {
            if(hit.collider.tag == "tree" && Input.GetButtonDown("Crop") && isEquiped == true)
            {
                TreeScript treeScript = hit.collider.gameObject.GetComponent<TreeScript>();
                treeScript.treeHealth--;
            }
            
        }
    }
}
