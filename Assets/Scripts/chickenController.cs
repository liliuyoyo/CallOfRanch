using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenController : MonoBehaviour
{
    public Animator anim;

    private Transform _trans;

    //idling-1, walking-2, running-3
    private int randomNum;
    //rotateCL-1~23, rotateReCL-24~39,transform-40~99
    private int randomType;
    //right-1, left-2
    private float randomTime;
    private float deltaTime;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _trans = this.transform;
       
        randomNum = 0;
        randomTime = 0.0f;
        deltaTime = 0.0f;
        breath(randomNum);

        randomTime = Random.Range(3, 8);
        randomNum = Random.Range(0, 4);
        randomType = Random.Range(1, 100);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("deltaTime: " + deltaTime + ", randomTime: " + randomTime + ", randomNum: " + randomNum + ", randomType: " + randomType);
        if(deltaTime < randomTime)
        {
            deltaTime += Time.deltaTime;
            executeMovement(randomNum);
            //execute
        }
        else
        {
            deltaTime = 0.0f;
            randomTime = Random.Range(3, 8);
            randomNum = Random.Range(0, 4);
            randomType = Random.Range(1, 100);
            breath(randomNum);
        }

    }

    void breath(int randomNum)
    {
        anim.SetBool("isIdling", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        switch (randomNum)
        {
            case 1:
                anim.SetBool("isIdling", true);
                break;
            case 2:
                anim.SetBool("isWalking", true);
                break;
            case 3:
                anim.SetBool("isRunning", true);
                break;
            default:
                break;
        }
        return;
    }

    void executeMovement(int randomNum)
    {
        var ray = new Ray(_trans.position + _trans.up, _trans.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 2f, Color.yellow);
        RaycastHit hit;
        switch(randomNum)
        {
            case 1:
                break;
            case 2:
                if(Physics.Raycast(ray, out hit, 2f))
                {
                    //_trans.transform.eulerAngles += new Vector3(0, 1, 0) * 0.1f;
                    _trans.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    if (randomType < 24)
                    {
                        _trans.transform.Rotate(new Vector3(0, 1, 0));
                    }
                    else if (randomType < 40)
                    {
                        _trans.transform.Rotate(new Vector3(0, -1, 0));
                    }
                    else
                    {
                        _trans.position += _trans.forward * 0.006f;
                    }

                }
                break;
            case 3:
                if (Physics.Raycast(ray, out hit, 2f))
                {
                    //_trans.transform.eulerAngles += new Vector3(0, 1, 0) * 0.1f;
                    _trans.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    if (randomType < 24)
                    {
                        _trans.transform.Rotate(new Vector3(0, 1, 0));
                    }
                    else if (randomType < 40)
                    {
                        _trans.transform.Rotate(new Vector3(0, -1, 0));
                    }
                    else
                    {
                        _trans.position += _trans.forward * 0.015f;
                    }

                }
                break;
            default:
                break;
        }
    }
}
