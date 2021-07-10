using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            TimeManager.instance.changeGameSpeed(TimeManager.GameSpeedType.SLOW);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            TimeManager.instance.changeGameSpeed(TimeManager.GameSpeedType.NORMAL);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            TimeManager.instance.changeGameSpeed(TimeManager.GameSpeedType.FAST);
        }
    }
}
