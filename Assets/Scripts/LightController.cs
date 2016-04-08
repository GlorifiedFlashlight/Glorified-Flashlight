using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    public Light myLight;
    bool bigSmall;

    void Start()
    {
        bigSmall = false;
    }
	void Update ()
    {
        if (Input.GetKeyDown("e"))
        {
            bigSmall = !bigSmall;
            SetLight();
        }
	}
    void SetLight()
    {
        if (bigSmall)
        {
            myLight.range = 3;
            myLight.spotAngle = 60;
        }
        else
        {
            myLight.range = 1.5f;
            myLight.spotAngle = 135;
        }
    }
}
