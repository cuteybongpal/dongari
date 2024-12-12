using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;

    // Update is called once per frame
    void Update()
    {
        cameraMovement.SetTargetByEntityId(name);
    }
}
