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
        if (Input.GetKey(KeyCode.E))
        {
            TimeManager.instance.changeGameSpeed(TimeManager.GameSpeedType.FAST);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            TimeManager.instance.changeGameSpeed(TimeManager.GameSpeedType.SLOW);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            TimeManager.instance.changeGameSpeed(TimeManager.GameSpeedType.NORMAL);
        }
    }
}
