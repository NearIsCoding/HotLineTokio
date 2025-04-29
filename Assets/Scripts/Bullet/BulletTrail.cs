using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float progressBetweenPoints; // Moved space from startPosition to targetPosition

    [SerializeField] private float speed = 40f;

    void Start()
    {
        
    }

    void Update()
    {
        progressBetweenPoints += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, targetPosition, progressBetweenPoints);
    }

    public void SetTargetPosition(Vector3 newTargetPosition)
    {
        startPosition = transform.position;
        targetPosition = newTargetPosition.WithAxis(Axis.Z, -1) ;
        progressBetweenPoints = 0f;
    }

}
