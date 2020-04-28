using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateTorch : MonoBehaviour
{
    public GameObject torch;

    public GameObject selectionHelper;
    SelectionManager1 sm;
    public bool isDriving = false;

    private bool isEquiped = false;

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
            torch.SetActive(false);
        }
        else
        {
            if (!torch.activeSelf && Input.GetKeyDown(KeyCode.Alpha9))
            {
                isEquiped = true;
                torch.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                isEquiped = false;
                torch.SetActive(false);
            }
        }

    }

    public void SetEquipment(bool enable)
    {
        if(enable)
        {
            isEquiped = true;
            torch.SetActive(true);
        }
        else
        {
            isEquiped = false;
            torch.SetActive(false);
        }
    }
}
