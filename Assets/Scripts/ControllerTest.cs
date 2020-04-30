using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{

    //public GameObject leftMesh;
    public GameObject rightMesh;
    public GameObject rightController;
    public GameObject avartRig;
    // Start is called before the first frame update
    void Start()
    {
        //leftMesh = GetComponent<GameObject>();
        //rightMesh = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(rightController.transform.rotation.eulerAngles - avartRig.transform.rotation.eulerAngles);
        float leftAngleY = rightController.transform.rotation.eulerAngles.y - avartRig.transform.rotation.eulerAngles.y;
        if (leftAngleY > 180) leftAngleY -= 360;
        if (leftAngleY < -180) leftAngleY += 360;
        leftAngleY = leftAngleY < 60f ? leftAngleY : 60f;
        leftAngleY = leftAngleY > -60f ? leftAngleY : -60f;
        //Debug.Log(leftAngleY);
    }
}
