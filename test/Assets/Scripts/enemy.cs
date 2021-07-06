using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [Header("Floats")]
    public float moveSpeed;
    public float patrolTime;

    [Header("Patrol Booleans")]
    public bool boxPatrol;

    [Header("References")]
    public Vector3 direction;
    public CharacterController myController;
    
    // Start is called before the first frame update
    void Start()
    {
       myController = GetComponent<CharacterController>();
       direction = Vector3.left;
       StartCoroutine(xRoutine());
    }
    
    // Update is called once per frame
    void Update()
    {
        myController.Move(direction * Time.deltaTime * moveSpeed);
    }
    IEnumerator xRoutine()
    {
        while(boxPatrol)
        {
            direction = Vector3.left;
            yield return new WaitForSeconds(patrolTime);

            direction = Vector3.forward;
            yield return new WaitForSeconds(patrolTime);

            direction = Vector3.right;
            yield return new WaitForSeconds(patrolTime);
            
            direction = Vector3.back;
            yield return new WaitForSeconds(patrolTime);
        }
    }
}