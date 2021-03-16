using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    // related scripts
    [Header("Related Scripts")]
    [SerializeField] internal Enemy_Collision enemyCollision;
    [SerializeField] internal Enemy_AI_Melee enemyAiMelee;
    
    // speed modifiers
    [Header("Speed Modifiers")]
    [SerializeField] internal float rotationSpeed = 100f;
    [SerializeField] internal float movementSpeed = 4f;
    [SerializeField] internal float chaseSpeed = 7f;
    
    // enemy type
    [Header("Enemy Type")]
    [SerializeField] internal EnemyType enemyType;
    internal enum EnemyType
    {
        Melee,
        Ranged
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
