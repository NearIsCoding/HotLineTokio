using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5f;
    public float mouseInfluence = 3f;

    void LateUpdate()
    {
        // Posici�n del jugador en mundo
        Vector3 playerPos = player.position;

        // Posici�n del mouse en mundo
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        // Direcci�n del jugador al mouse
        Vector3 offset = mouseWorldPos - playerPos;

        // Limita la influencia del mouse
        offset = Vector3.ClampMagnitude(offset, mouseInfluence);

        // Nueva posici�n deseada
        Vector3 targetPos = playerPos + offset;
        targetPos.z = -10f;

        // Movimiento suave
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}