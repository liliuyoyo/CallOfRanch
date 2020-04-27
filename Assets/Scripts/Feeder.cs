using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeder : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject fencePos1;
    //public GameObject fencePos2;
    public GameObject horse1;
    //public GameObject horse2;
    public GameObject fullFeeder;
    private GameObject _emptyFeeder;
    private MeshRenderer _fullFeederRender;
    private MeshRenderer _emptyFeederRender;

    

    void Start()
    {
        //fencePos1 = GetComponent<GameObject>();
        //fencePos2 = GetComponent<GameObject>();
        //horse1 = GetComponent<GameObject>();
        horse1 = GameObject.Find("HorseAnimations-1");
        //horse2 = GetComponent<GameObject>();
        //fullFeeder = GetComponent<GameObject>();
        fullFeeder = GameObject.Find("Feeder_2_w_Hay");
        _emptyFeeder = gameObject;
        _fullFeederRender = fullFeeder.GetComponent<MeshRenderer>();
        _emptyFeederRender = fullFeeder.GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Horses should eat hay now");
        //if(_fullFeederRender == null)
        //{
        //    Debug.Log("fullFeederRender is null");
        //}
        Debug.Log(fullFeeder.gameObject.name);
        _fullFeederRender.enabled = true;
        //_emptyFeederRender.enabled = false;
        fullFeeder.SetActive(true);

        HorseController horseController1 = horse1.GetComponent<HorseController>();
        horseController1.UpdateFoodState(true);

        Debug.Log(collision.gameObject.name);
        collision.gameObject.SetActive(false);

        //HorseController horseController2 = horse2.GetComponent<HorseController>();
        //horseController1.SetFeederPos();
    }

    
}
