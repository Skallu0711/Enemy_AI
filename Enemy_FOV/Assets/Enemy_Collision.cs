using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collision : MonoBehaviour
{
    // related scripts
    [Header("Related Scripts")]
    [SerializeField] internal Enemy_Controller enemyController;
    
    [Header("Player Target")]
    [SerializeField] internal GameObject player;
    
    internal bool detected;
    
    internal Vector3 startingPosition;
    
    // debug line distances
    private float distance = 8f;
    private float diagonalDistance;

    void Start()
    {
        startingPosition = transform.position;
        diagonalDistance = distance * 1.44f;
        
        detected = false;
    }

    internal void Detected()
    {
        Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.red);
        Debug.DrawLine(transform.position,  transform.position + (transform.up + transform.right).normalized * diagonalDistance , Color.red);
        Debug.DrawLine(transform.position,  transform.position +  (-1f * transform.up + transform.right).normalized * diagonalDistance, Color.red);
    }
    
    internal void Undetected()
    {
        Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        Debug.DrawLine(transform.position,  transform.position + (transform.up + transform.right).normalized * diagonalDistance , Color.green);
        Debug.DrawLine(transform.position,  transform.position +  (-1f * transform.up + transform.right).normalized * diagonalDistance, Color.green);
    }
    

    internal void RotateTo(Vector3 target)
    {
        Vector2 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * enemyController.rotationSpeed);
    }

    internal void GoTowards(Vector3 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
    }

    internal void ReturnToStartingPosition(Vector3 startingPos)
    {
         RotateTo(startingPos);
         GoTowards(startingPos, enemyController.movementSpeed);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            detected = true;

            Vector3 dir = col.transform.position - transform.position;
            Debug.DrawLine(transform.position, col.transform.position, Color.red);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        detected = false;
    }
    
    
}
