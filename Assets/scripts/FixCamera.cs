using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    private const int ResolutionX = 9;
    private const int ResolutionY = 16;

    private void Start()
    {
        
        var screenRatio = Screen.width*1f / Screen.height;
        const float bestRatio = ResolutionX*1f / ResolutionY;
        if (screenRatio <= bestRatio)
        {
            GetComponent<Camera>().rect = new Rect(0,(1f- screenRatio / bestRatio)/2f, 1, screenRatio / bestRatio);
        }else if(screenRatio > bestRatio)
        {
            GetComponent<Camera>().rect = new Rect((1f- bestRatio / screenRatio) /2f, 0, bestRatio / screenRatio, 1);
        }
    }
}
