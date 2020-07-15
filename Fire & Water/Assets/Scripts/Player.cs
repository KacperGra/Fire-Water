using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    private Vector2 movementInput;
    public string playerName;

    [Header(header: "Movement")]

    // Variables used to separate a movement for both players
    private string horizontalMoveName;
    private string vericalMoveName;

    public float moveSpeed;

    void Start()
    {
        Init();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movementInput = new Vector2(Input.GetAxis(horizontalMoveName), Input.GetAxis(vericalMoveName));   

        Animate();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movementInput.x * Time.fixedDeltaTime * moveSpeed, movementInput.y * Time.fixedDeltaTime * moveSpeed);  
    }

    void Init()
    {
        if (playerName == "Fire")
        {
            horizontalMoveName = "P1_Horizontal";
            vericalMoveName = "P1_Vertical";
        }
        else if (playerName == "Water")
        {
            horizontalMoveName = "P2_Horizontal";
            vericalMoveName = "P2_Vertical";
        }
    }

    private void Animate()
    {
        if(movementInput.x != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(movementInput.x));
            if(movementInput.x > 0)
            {
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            else if (movementInput.x < 0)
            {
                transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
            }
        }
        else if(movementInput.y != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(movementInput.y));
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

    }


}
