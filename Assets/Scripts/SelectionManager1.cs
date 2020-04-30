using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager1 : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string drivibleTag = "isVehicle";
    [SerializeField] private string ignitableTag = "ignitable";
    [SerializeField] private string doorTag = "door";
    [SerializeField] private string hayTag = "hay";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    public bool isDriving = false;
    public bool isSelecting = false;
    

    private Transform _selection;

    public float rotateSpeed = 2.0f;
    private float yaw = 0.0f;
    public GameObject avatarRig;
    public GameObject vehicle;
    public GameObject leftController;
    public GameObject rightController;
    private GameObject _holdObject;
    //private Vector3 _disVec = new Vector3(1, 1, 1);
    private float _dis = 0.0f;
    //public GameObject camera;

    private string m_transform = "Transform";
    private string m_raise = "Raise";
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
            //Debug.Log("isDriving is true");
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

                
                //Debug.Log("avatarRig: " + avatarRig.transform.position + ", vehicle: " + avatarRig.transform.position);
            }
        }
        else if(isSelecting)
        {
            if(!_holdObject.activeSelf || Input.GetAxis(m_hold) < 0)
            {
                ResetRightController();
                isSelecting = false;
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                //Vector3 releasePos = avatarRig.transform.position + _disVec;
                ////releasePos.y = 0.0f;
                //_holdObject.transform.position = releasePos;
                _holdObject = null;
                _selection = null;
                
            }
            else
            {

                //_holdObject.transform.position = avatarRig.transform.position + _disVec;

                ////Tranform
                //Vector3 force = avatarRig.transform.position - _holdObject.transform.position;
                //force.x = force.x * 0.003f;
                //force.y = 0;
                //force.z = force.z * 0.003f;
                //Debug.Log(force);
                //_holdObject.transform.position -= force * Input.GetAxis(m_transform);

                ////Raise
                //force = new Vector3(0, -1, 0);
                //_holdObject.transform.position -= force * Input.GetAxis(m_raise);

                ////Rotate
                //yaw += rotateSpeed * Input.GetAxis(m_rotate);
                //_holdObject.transform.eulerAngles = new Vector3(0, yaw, 0);
                //Debug.Log(_holdObject.transform.eulerAngles);

                ////Scale 
                //Vector3 boost = new Vector3(0.01f, 0.01f, 0.01f);
                //_holdObject.transform.localScale += boost * Input.GetAxis(m_scale);

                //_disVec = _holdObject.transform.position - avatarRig.transform.position;
                
                
                float leftAngleY = leftController.transform.rotation.eulerAngles.y - avatarRig.transform.rotation.eulerAngles.y;
                if (leftAngleY > 180) leftAngleY -= 360;
                if (leftAngleY < -180) leftAngleY += 360;
                leftAngleY = leftAngleY < 60f ? leftAngleY : 60f;
                leftAngleY = leftAngleY > -60f ? leftAngleY : -60f;
                _dis += leftAngleY * 0.01f;
                Vector3 vz = avatarRig.transform.forward * _dis;


                float rightAngleY = rightController.transform.rotation.eulerAngles.y - avatarRig.transform.rotation.eulerAngles.y;
                if (rightAngleY > 180) rightAngleY -= 360;
                if (rightAngleY < -180) rightAngleY += 360;
                rightAngleY = rightAngleY < 60f ? rightAngleY : 60f;
                rightAngleY = rightAngleY > -60f ? rightAngleY : -60f;
                Vector3 vx = avatarRig.transform.right * (_dis * Mathf.Tan(rightAngleY * Mathf.PI / 180));

                float rightAngleX = rightController.transform.rotation.eulerAngles.x - avatarRig.transform.rotation.eulerAngles.x;
                if (rightAngleX > 180) rightAngleX -= 360;
                if (rightAngleX < -180) rightAngleX += 360;
                rightAngleX = rightAngleX < 60f ? rightAngleX : 60f;
                rightAngleX = rightAngleX > -60f ? rightAngleX : -60f;
                Vector3 vy = avatarRig.transform.up * (_dis * Mathf.Tan(rightAngleX * Mathf.PI / 180)) * -1f;
                //Debug.Log("Position of RightController: " + rightController.transform.position + ", Rotation of RightController: " + rightController.transform.rotation);
                //Debug.Log(rightController.transform.rotation.x + " " + rightController.transform.rotation.y + " " + rightController.transform.rotation.z + " " + rightController.transform.rotation.w);
                //Debug.Log(rightController.transform.rotation.eulerAngles - avatarRig.transform.rotation.eulerAngles);
                //Debug.Log(angleX + " " + angleY + " " + Mathf.Tan(angleX * Mathf.PI / 180) + " " + Mathf.Tan(angleY * Mathf.PI / 180));
                //Debug.Log("vx: " + vx + ", vy: " + vy);
                _holdObject.transform.position = vz + vy + vx + rightController.transform.position;
            }
        }
        else
        {
            if (_selection != null)
            {
                
                //Debug.Log("Lose selction");
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }

            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //Debug.Log("ray's direction: " + ray.direction + ". ray's origin: " + ray.origin);
            //Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20))
            {
                var selection = hit.transform;

                //Debug.Log("Object caught");

                if (selection.gameObject.CompareTag(selectableTag) || selection.gameObject.CompareTag(hayTag))
                {
                    //Debug.Log("Object is selectable");
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = highlightMaterial;
                    }
                    _selection = selection;
                    if (Input.GetAxis(m_hold) > 0)
                    {
                        ResetRightController();
                        Debug.Log("Select successfully");
                        _holdObject = _selection.gameObject;
                        //_disVec = _holdObject.transform.position - avatarRig.transform.position;
                        _dis = Vector3.Distance(_holdObject.transform.position, avatarRig.transform.position);
                        isSelecting = true;
                    }
                    
                }
                else if (selection.gameObject.CompareTag(drivibleTag))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        isDriving = true;
                        Vector3 newPos = vehicle.transform.position;
                        newPos.y += 5.0f;
                        avatarRig.transform.position = newPos;
                        Camera.main.GetComponent<BoxCollider>().enabled = false;
                    }
                }
                else if (selection.gameObject.CompareTag(doorTag))
                {
                    Debug.Log("find door!");
                    if (Input.GetMouseButton(0))
                    {
                        Debug.Log("Click!");

                        InteractiveDoor door = selection.GetComponent<InteractiveDoor>();
                        if (door)
                        {
                            Debug.Log("This is a interactive door!");

                            door.TriggerInteraction();
                        }
                    }
                }
                else if(selection.gameObject.CompareTag(ignitableTag))
                {
                    // TODO:
                }
            }
            else
            {
                //Debug.Log("Object not caught");
            }
        }
    }

    private void ResetRightController()
    {
        rightController.transform.position = leftController.transform.position + leftController.transform.right * 0.4f;
        rightController.transform.rotation = new Quaternion();
        leftController.transform.rotation = new Quaternion();
    }
}