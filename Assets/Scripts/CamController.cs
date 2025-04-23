using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    public Transform target;
    public float followSpeed = 5f;
    public float mouseInfluence = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraPosition();
    }

    public void CameraPosition()
    {
        transform.position = new Vector3(target.transform.position.x, target.position.y, (target.position.z - 10));
    }
}
