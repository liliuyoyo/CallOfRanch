using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    public enum eInteractiveState
    {
        Active,
        InActive,
    }

    private eInteractiveState state;
    // Start is called before the first frame update
    void Start()
    {
        state = eInteractiveState.InActive;
    }

    public void TriggerInteraction()
    {
        if (!GetComponent<Animation>().isPlaying)
        {
            switch (state)
            {
                case eInteractiveState.Active:
                    GetComponent<Animation>().Play("GateClose");
                    state = eInteractiveState.InActive;
                    break;
                case eInteractiveState.InActive:
                    GetComponent<Animation>().Play("GateOpen");
                    state = eInteractiveState.Active;
                    break;
                default:
                    break;
            }
        }
    }
}
