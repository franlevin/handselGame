using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform CamTransform;
    public Transform Player;
    float followspeed;

    void Start()
    {
        //CamTransform = Camera.main.transform;
    }

    void Update()
    {

        Vector2 targetPosition = new Vector2(Player.position.x, CamTransform.position.y);

        CamTransform.position = Vector2.Lerp(CamTransform.position, targetPosition, Time.deltaTime * followspeed);
    }
}
