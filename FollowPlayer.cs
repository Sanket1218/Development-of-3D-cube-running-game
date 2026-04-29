using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float offSet;

    void Update()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerTransform.position.z + offSet;
        transform.position = cameraPos;
    }
}
