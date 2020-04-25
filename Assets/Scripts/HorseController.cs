using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public Animator anim;
    public GameObject self;
    private Transform _trans;

    //idling-1, walking-2, running-3
    private int _randomNum;
    //rotateCL-1~23, rotateReCL-24~39,transform-40~99
    private int _randomType;
    //right-1, left-2
    private float _randomTime;
    private float _deltaTime;
    private bool _hasFood;
    private float _foodDistance;

    //private float[] _slotsPosX = { -4.900635f, -5.150635f };
    //private float[] _slotsPosY = { -8.715881f, -31.67587f };
    private Vector3[] _slotsPos = { new Vector3(-1.75f, 0f, -8.64f), new Vector3(-1.83f, 0, -32.08f) };
    private int _objNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        _trans = this.transform;

        _randomNum = 0;
        _randomTime = 0.0f;
        _deltaTime = 0.0f;
        _foodDistance = 200.0f;
        _hasFood = false;
        breath(_randomNum);

        _randomTime = Random.Range(3, 8);
        _randomNum = Random.Range(0, 4);
        _randomType = Random.Range(1, 100);

        _objNumber = int.Parse(self.name.Split('-')[1]);
        _foodDistance = Vector3.Distance(_slotsPos[_objNumber], self.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("deltaTime: " + deltaTime + ", randomTime: " + randomTime + ", randomNum: " + randomNum + ", randomType: " + randomType);

        if(_hasFood)
        {
            //execute seeking food method.
            _foodDistance = Vector3.Distance(_slotsPos[_objNumber], self.transform.position);
            anim.SetFloat("foodDistance", _foodDistance);

            Vector3 destination1 = new Vector3(self.transform.position.x, self.transform.position.y, _slotsPos[_objNumber].z);

        }
        else
        {
            if (_deltaTime < _randomTime)
            {
                _deltaTime += Time.deltaTime;
                executeMovement(_randomNum);
                //execute
            }
            else
            {
                _deltaTime = 0.0f;
                _randomTime = Random.Range(3, 8);
                _randomNum = Random.Range(0, 4);
                _randomType = Random.Range(1, 100);
                breath(_randomNum);
            }

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
        if(_trans.rotation.x != 0 || _trans.rotation.z != 0)
        {

        }
        var ray = new Ray(_trans.position + _trans.up, _trans.forward);
        //Debug.DrawRay(ray.origin, ray.direction * 4f, Color.yellow);
        RaycastHit hit;
        switch(randomNum)
        {
            case 1:
                break;
            case 2:
                if(Physics.Raycast(ray, out hit, 4f))
                {
                    //_trans.transform.eulerAngles += new Vector3(0, 1, 0) * 0.1f;
                    _trans.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    if (_randomType < 24)
                    {
                        _trans.transform.Rotate(new Vector3(0, 1, 0));
                    }
                    else if (_randomType < 40)
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
                if (Physics.Raycast(ray, out hit, 4f))
                {
                    //_trans.transform.eulerAngles += new Vector3(0, 1, 0) * 0.1f;
                    _trans.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    if (_randomType < 24)
                    {
                        _trans.transform.Rotate(new Vector3(0, 1, 0));
                    }
                    else if (_randomType < 40)
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

    public void UpdateFoodState(bool hasFood)
    {
        _hasFood = hasFood;
    }
}
