using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Melee : MonoBehaviour
{
    // related scripts
    [Header("Related Scripts")] 
    [SerializeField] internal Enemy_Controller enemyController;
    
    // ai states
    [Header("AI States")]
    [SerializeField] internal State state;
    internal enum State
    {
        Idle,
        Patroll,
        Chase,
        Return,
        Attack,
        RunAway
    }

    private bool isReturning;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
        // state machine
        switch (state)
        {
            case State.Idle:
            {
                Idle();
                
                // when spotted player
                if (enemyController.enemyCollision.detected)
                    state = State.Chase;
                
                break;
            }

            case State.Patroll:
            {
                Patroll();
                
                // when spotted player, while patrolling
                break;
            }
            
            case State.Chase:
            {
                Chase();
                
                // when player is no longer detected
                if (enemyController.enemyCollision.detected == false)
                    state = State.Return;

                // when player is in attack range
                if (enemyController.enemyCollision.detected)
                {
                    if (Vector2.Distance(transform.position, enemyController.enemyCollision.player.transform.position) < 0.4f)
                        state = State.Attack;
                }
                
                break;
            }
            
            case State.Return:
            {
                Return();
                
                // when spotted player, while returning
                if (isReturning)
                {
                    if (enemyController.enemyCollision.detected == true)
                        state = State.Chase;
                }
                
                // when returned to starting position
                if (transform.position == enemyController.enemyCollision.startingPosition)
                    state = State.Idle;

                break;
            }
            
            case State.Attack:
            {
                Attack();

                // when player is out of attack range
                if (Vector2.Distance(transform.position, enemyController.enemyCollision.player.transform.position) >= 0.4f)
                    state = State.Chase;
                
                // when player is no longer detected
                if (enemyController.enemyCollision.detected == false)
                    state = State.Return;

                break;
            }
            
            case State.RunAway:
            {
                RunAway();
                
                
                
                break;
            }

            default:
            {
                state = State.Idle;
                break;
            }
        }
    }

    private void Idle()
    {
        enemyController.enemyCollision.Undetected();
    }

    private void Patroll()
    {
        enemyController.enemyCollision.Undetected();
    }

    private void Chase()
    {
        isReturning = false;
        
        enemyController.enemyCollision.Detected();

        Vector3 playerPosition = enemyController.enemyCollision.player.transform.position;

        enemyController.enemyCollision.RotateTo(playerPosition);
        enemyController.enemyCollision.GoTowards(playerPosition, enemyController.chaseSpeed);
    }

    private void Return()
    {
        isReturning = true;
        
        enemyController.enemyCollision.Undetected();
        
        enemyController.enemyCollision.ReturnToStartingPosition(enemyController.enemyCollision.startingPosition);
    }

    private void Attack()
    {
        enemyController.enemyCollision.Detected();
        
        Debug.Log("attack");
        //TODO : implement attacking
    }

    private void RunAway() // when (0 < hp < 10) => run away
    {
        //TODO : implement running away
    }
    
}
