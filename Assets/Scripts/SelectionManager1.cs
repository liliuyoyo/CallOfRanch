using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager1 : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string drivibleTag = "isVehicle";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    public bool isDriving = false;
    

    private Transform _selection;

    public float rotateSpeed = 2.0f;
    private float yaw = 0.0f;
    public GameObject avatarRig;
    public GameObject vehicle;
    //public GameObject camera;

    private string m_transform = "Transform";
    private string m_rotate = "Rotate";
    private string m_scale = "Scale";
    private string m_hold = "Hold";

    // Start is called before the first frame update
    void Start()
    {
        //avatarRig = GameObject.Find("SelectionHelper");
        //vehicle = GetComponent<GameObject>();
        //isDriving = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isDriving)
        {
            Debug.Log("isDriving is true");
            if(Input.GetKeyDown(KeyCode.E))
            {
                isDriving = false;
                Camera.main.GetComponent<BoxCollider>().enabled = true;
                avatarRig.transform.position = avatarRig.transform.position - avatarRig.transform.right * 8;


            }
            else
            {
                Vector3 newPos = vehicle.transform.position;
                newPos.y += 5.0f;
                avatarRig.transform.position = newPos;

                Debug.Log("avatarRig: " + avatarRig.transform.position + ", vehicle: " + avatarRig.transform.position);
            }
        }
        else
        {
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }

            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //Debug.Log("ray's direction: " + ray.direction + ". ray's origin: " + ray.origin);
            Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20))
            {
                var selection = hit.transform;

                //Debug.Log("Object caught");

                if (selection.gameObject.CompareTag(selectableTag))
                {
                    //Debug.Log("Object is selectable");
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = highlightMaterial;
                    }
                    _selection = selection;

                    //Tranform
                    Vector3 force = Camera.main.transform.position - hit.transform.position;
                    force.x = force.x * 0.003f;
                    force.y = 0;
                    force.z = force.z * 0.003f;
                    Debug.Log(force);
                    hit.transform.position -= force * Input.GetAxis(m_transform);

                    //Rotate
                    yaw += rotateSpeed * Input.GetAxis(m_rotate);
                    hit.transform.eulerAngles = new Vector3(0, yaw, 0);
                    Debug.Log(hit.transform.eulerAngles);

                    //Scale 
                    Vector3 boost = new Vector3(0.01f, 0.01f, 0.01f);
                    hit.transform.localScale += boost * Input.GetAxis(m_scale);
                }
                else if(selection.gameObject.CompareTag(drivibleTag))
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        isDriving = true;
                        Vector3 newPos = vehicle.transform.position;
                        newPos.y += 5.0f;
                        avatarRig.transform.position = newPos;
                        Camera.main.GetComponent<BoxCollider>().enabled = false;
                    }
                }
            }
            else
            {
                //Debug.Log("Object not caught");
            }
        }
    }
}
