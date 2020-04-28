using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlIgnite : MonoBehaviour
{
    public Transform sparkle;
    //public Transform sparkle2;
    // Start is called before the first frame update
    void Start()
    {
        sparkle.GetComponent<ParticleSystem>().enableEmission = false;
        //sparkle2.GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  void OnCollisionEnter(Collision col)
    // {
    //     if (col.gameObject.name == "Torch")
    //     {
    //         sparkle1.GetComponent<ParticleSystem>().enableEmission = true;
    //     }
    // }

    void OnTriggerEnter() 
    {
        sparkle.GetComponent<ParticleSystem>().enableEmission = true;
        //sparkle2.GetComponent<ParticleSystem>().enableEmission = true;
    }
}
