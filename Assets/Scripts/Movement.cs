using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;

    public GameObject rightController;
    public GameObject leftController;
    public GameObject avatarRig;
    public GameObject selectionHelper;
    public float c_speed = 1.0f;
    public float c_turnSpeed = 1.0f;
    SelectionManager1 sm;
    public bool isDriving = false;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;

    private float leftAngleY;
    private float rightAngleX;

    private void Awake()
    {

    }

    private void onEnable()
    {
        //Kinematic means NO FORCE applied.
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_Speed = 30f;

        m_Rigidbody = GetComponent<Rigidbody>();
        selectionHelper = GameObject.Find("SelectionHelper");
        sm = selectionHelper.GetComponent<SelectionManager1>();
        isDriving = sm.isDriving;


        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        isDriving = sm.isDriving;
        if (isDriving)
        {
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

            EngineAudio();
            //Camera.main.GetComponent<BoxCollider>().enabled = true;
        }
        //store the player's input and make sure the audio for the engine is playing 
    }

    private void EngineAudio()
    {
        if(Mathf.Abs(rightAngleX) < 1f && Mathf.Abs(leftAngleY) < 1f)
        //if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if(isDriving)
        {
            Turn();
            Move();
        }
    }

    private void Move()
    {
        leftAngleY = leftController.transform.rotation.eulerAngles.x - avatarRig.transform.rotation.eulerAngles.x;
        if (leftAngleY > 180) leftAngleY -= 360;
        if (leftAngleY < -180) leftAngleY += 360;
        leftAngleY = leftAngleY < 60f ? leftAngleY : 60f;
        leftAngleY = leftAngleY > -60f ? leftAngleY : -60f;
        Vector3 movement = transform.forward * leftAngleY * c_speed;



        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        m_Rigidbody.AddForce(movement);
        //Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

    }

    private void Turn()
    {
        rightAngleX = rightController.transform.rotation.eulerAngles.y - avatarRig.transform.rotation.eulerAngles.y;
        if (rightAngleX > 180) rightAngleX -= 360;
        if (rightAngleX < -180) rightAngleX += 360;
        rightAngleX = rightAngleX < 60f ? rightAngleX : 60f;
        rightAngleX = rightAngleX > -60f ? rightAngleX : -60f;
        Vector3 movementY = transform.right * rightAngleX * c_turnSpeed;
        //float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        //float turn = rightAngleX * 0.1f;
        //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        //m_Rigidbody.AddForce(movementY);
        m_Rigidbody.AddForceAtPosition(movementY, m_Rigidbody.position + m_Rigidbody.transform.forward);
        m_Rigidbody.AddForceAtPosition(movementY * -1, m_Rigidbody.position - m_Rigidbody.transform.forward);
        //m_Rigidbody.AddRelativeTorque(movementY);
        //float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        //Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
