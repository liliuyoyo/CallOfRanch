using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cropping : MonoBehaviour
{
    //Variables
    public GameObject axe;

    public GameObject selectionHelper;
    SelectionManager1 sm;
    public bool isDriving = false;

    private bool isEquiped = false;
    private int rayCastLength = 10;

    private void Start()
    {
        selectionHelper = GameObject.Find("SelectionHelper");
        sm = selectionHelper.GetComponent<SelectionManager1>();
    }

    private void Update()
    {
        isDriving = sm.isDriving;
        if(isDriving)
        {
            isEquiped = false;
            axe.SetActive(false);
        }
        else
        {
            if (!axe.activeSelf && Input.GetKeyDown(KeyCode.Alpha0))
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

            if (Physics.Raycast(transform.position, fwd, out hit, rayCastLength))
            {
                if (hit.collider.tag == "tree" && Input.GetButtonDown("Crop") && isEquiped == true)
                {
                    TreeScript treeScript = hit.collider.gameObject.GetComponent<TreeScript>();
                    treeScript.treeHealth--;
                }

            }
        }

    }

    public void SetEquipment(bool enable)
    {
        if(enable)
        {
            isEquiped = true;
            axe.SetActive(true);
        }
        else
        {
            isEquiped = false;
            axe.SetActive(false);
        }
    }
}
