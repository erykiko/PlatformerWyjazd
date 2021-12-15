using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpSpeed;

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smothPos = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.fixedDeltaTime);
        transform.position = smothPos;
    }
}
