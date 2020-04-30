using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public Animator anim;
    public GameObject self;
    public GameObject feeder;
    public GameObject testCube;
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

    private bool _step1_rotation = false;
    private bool _step1_position = false;
    private bool _step2_rotation = false;
    private bool _step2_position = false;
    //private float[] _slotsPosX = { -4.900635f, -5.150635f };
    //private float[] _slotsPosY = { -8.715881f, -31.67587f };
    //private Vector3[] _slotsPos = { new Vector3(-1.75f, 0f, -8.64f), new Vector3(-1.83f, 0, -32.08f) };
    private Vector3 _slotPos;
    //private int _objNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        anim = GetComponent<Animator>();
        //feeder = GetComponent<GameObject>();
        feeder = GameObject.Find("Feeder_2");
        //testCube = GetComponent<GameObject>();
        testCube = GameObject.Find("Cube");
        _slotPos = feeder.transform.position;
        _trans = this.transform;

        _randomNum = 0;
        _randomTime = 0.0f;
        _deltaTime = 0.0f;
        _foodDistance = 200.0f;
        _hasFood = false;
        breath(_randomNum);

        _randomTime = Random.Range(3, 8);
        _randomNum = Random.Range(0, 3);
        _randomType = Random.Range(1, 100);
        //Debug.Log(self.name);
        //_objNumber = int.Parse(self.name.Split('-')[1]);
        _foodDistance = Vector3.Distance(_slotPos, self.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //self = this.gameObject;
        //Debug.Log("deltaTime: " + _deltaTime + ", randomTime: " + _randomTime + ", randomNum: " + _randomNum + ", randomType: " + _randomType);

        if (_hasFood)
        {
            //Debug.Log("Horse: has food is true");
            //execute seeking food method.
            //_foodDistance = Vector3.Distance(_slotPos, self.transform.position);
            anim.SetFloat("foodDistance", _foodDistance);
            Vector3 destination1 = new Vector3(_slotPos.x, self.transform.position.y, self.transform.position.z);
            Vector3 direction1 = destination1 - self.transform.position;
            Vector3 destination2 = feeder.transform.position;
            Vector3 direction2 = destination2 - self.transform.position;
            //Vector3 destination1 = new Vector3(self.transform.position.x, self.transform.position.y, _slotPos.z);
            if (!_step1_rotation)
            {

                float angle = Vector3.Angle(direction1, self.transform.forward);
                //Debug.Log(angle);
                if (angle > 3)
                {
                    self.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    _step1_rotation = true;
                }
            }
            else if(!_step1_position)
            {
                float distance = Vector3.Distance(destination1, self.transform.position);
                //Debug.Log(distance);
                if(distance > 1)
                {
                    self.transform.position += self.transform.forward * 0.15f;
                }
                else
                {
                    _step1_position = true;
                }
            }
            else if(!_step2_rotation)
            {
                float angle = Vector3.Angle(direction2, self.transform.forward);
                //Debug.Log(angle);
                if (angle > 2)
                {
                    self.transform.Rotate(new Vector3(0, 1, 0));
                }
                else
                {
                    _step2_rotation = true;
                }
            }
            else if(!_step2_position)
            {
                float distance = self.transform.position.z - destination2.z;
                _foodDistance = distance;
                //Debug.Log(distance);
                if (distance > 4.5)
                {
                    self.transform.position += self.transform.forward * 0.15f;
                }
                else
                {
                    _step2_position = true;
                    anim.SetBool("seekFood", false);
                    anim.SetBool("isIdling", false);
                    anim.SetBool("isWalking", false);
                    anim.SetFloat("foodDistance", distance);

                }
            }
            else
            {
                float distance = self.transform.position.z - destination2.z;
                anim.SetFloat("foodDistance", distance);
            }
            //ture and move

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
        if(_hasFood)
        {
            Debug.Log("breath after _hasFood is true");
        }
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
        Debug.DrawRay(ray.origin, ray.direction * 4f, Color.yellow);
        RaycastHit hit;
        switch(randomNum)
        {
            case 1:
                break;
            case 2:
                if(Physics.Raycast(ray, out hit, 4.5f))
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
                        _trans.position += _trans.forward * 0.06f;
                    }

                }
                break;
            case 3:
                if (Physics.Raycast(ray, out hit, 4.5f))
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
                        _trans.position += _trans.forward * 0.15f;
                    }

                }
                break;
            default:
                break;
        }
    }

    public void UpdateFoodState(bool hasFood)
    {
        _hasFood = true;
        if (_randomNum == 0)
        {
            breath(3);

        }
        anim.SetBool("hasFood", true);
        anim.SetBool("seekFood", true);
        
        Vector3 destination1 = new Vector3(_slotPos.x, self.transform.position.y, self.transform.position.z) + new Vector3(0, 4.6f, 0);
        //testCube.transform.position = destination1;
    }

}
