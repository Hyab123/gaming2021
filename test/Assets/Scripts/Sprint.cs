using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    public float sprintBonus;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {  
            player.moveSpeed += sprintBonus;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.moveSpeed -= sprintBonus;
        }

    }
}