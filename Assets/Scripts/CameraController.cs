using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset = 1f;
    public Transform Cameratarget;

    private Transform activePlayer;

    void Start()
    {
        // Set initial active player
        activePlayer = Cameratarget;
    }

    void Update()
    {
        // Move Camera to active Player
        Vector3 newPos = new Vector3(activePlayer.position.x, activePlayer.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }

    // Set active Player
    public void SetActivePlayer(Transform playerTransform)
    {
        activePlayer = playerTransform;
    }
}

