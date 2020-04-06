using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    public GameObject _sphere;
    protected GameObject _holdObject;
    private Transform _selection;

    public float rotateSpeed = 2.0f;
    private float yaw = 0.0f;

    private string m_transform = "Transform";
    private string m_rotate = "Rotate";
    private string m_scale = "Scale";
    private string m_hold = "Hold";

    private Vector3 _disVec = new Vector3(1, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(_selection != null)
        //{
        //    var selectionRenderer = _selection.GetComponent<Renderer>();
        //    selectionRenderer.material = defaultMaterial;
        //    _selection = null;
        //}

        //var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        ////Debug.Log("ray's direction: " + ray.direction + ". ray's origin: " + ray.origin);
        ////Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 59))
        //{
        //    var selection = hit.transform;
            
        //    Debug.Log("Object caught");
            
        //    if (selection.gameObject.CompareTag(selectableTag))
        //    {
        //        Debug.Log("Object is selectable");
        //        var selectionRenderer = selection.GetComponent<Renderer>();
        //        if (selectionRenderer != null)
        //        {
        //            selectionRenderer.material = highlightMaterial;
        //        }

        //        _selection = selection;

        //        //Tranform
        //        Vector3 force = Camera.main.transform.position - hit.transform.position;
        //        force.x = force.x * 0.003f;
        //        force.y = 0;
        //        force.z = force.z * 0.003f;
        //        Debug.Log(force);
        //        hit.transform.position -= force * Input.GetAxis(m_transform);

        //        //Rotate
        //        yaw += rotateSpeed * Input.GetAxis(m_rotate);
        //        hit.transform.eulerAngles = new Vector3(0, yaw, 0);
        //        Debug.Log(hit.transform.eulerAngles);

        //        //Scale 
        //        Vector3 boost = new Vector3(0.01f, 0.01f, 0.01f);
        //        hit.transform.localScale += boost * Input.GetAxis(m_scale);
        //    }

        //}

        if(_holdObject != null)
        {
            if(Input.GetAxis(m_hold) < 0)
            {

                //var selectionRenderer = _selection.GetComponent<Renderer>();
                var selectionRenderer = _holdObject.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                Vector3 releasePos = Camera.main.transform.position + _disVec;
                releasePos.y = 0.0f;
                _holdObject.transform.position = releasePos;
                _holdObject = null;
                _selection = null;

            }
            else
            {
                _holdObject.transform.position = Camera.main.transform.position + _disVec;

                //Tranform
                Vector3 force = Camera.main.transform.position - _holdObject.transform.position;
                force.x = force.x * 0.003f;
                force.y = 0;
                force.z = force.z * 0.003f;
                Debug.Log(force);
                _holdObject.transform.position -= force * Input.GetAxis(m_transform);

                //Rotate
                yaw += rotateSpeed * Input.GetAxis(m_rotate);
                _holdObject.transform.eulerAngles = new Vector3(0, yaw, 0);
                Debug.Log(_holdObject.transform.eulerAngles);

                //Scale 
                Vector3 boost = new Vector3(0.01f, 0.01f, 0.01f);
                _holdObject.transform.localScale += boost * Input.GetAxis(m_scale);

                _disVec = _holdObject.transform.position - Camera.main.transform.position;
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
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 59))
            {
                var selection = hit.transform;
                if(selection.gameObject.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if(selectionRenderer != null)
                    {
                        selectionRenderer.material = highlightMaterial; 
                    }
                    _selection = selection;

                    if (_holdObject == null)
                    {
                        if (Input.GetAxis(m_hold) > 0)
                        {
                            _holdObject = _selection.gameObject;
                            _disVec = _holdObject.transform.position - Camera.main.transform.position;
                        }
                    }
                }
            }
        }
    }
}
