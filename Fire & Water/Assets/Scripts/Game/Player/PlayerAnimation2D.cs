using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation2D : MonoBehaviour
{
    public Player player;
    public string animatorSpeedVariable;

    private void Update()
    {
        var leftRotation = new Quaternion(0f, 180f, 0f, 0f);
        var rightRotation = new Quaternion(0f, 0f, 0f, 0f);

        if (player.movementInput.x != 0)
        {
            player.animator.SetFloat(animatorSpeedVariable, Mathf.Abs(player.movementInput.x));
            if (player.movementInput.x > 0)
            {
                transform.rotation = rightRotation;

            }
            else if (player.movementInput.x < 0)
            {
                transform.rotation = leftRotation;
            }
        }
        else if (player.movementInput.y != 0)
        {
            player.animator.SetFloat(animatorSpeedVariable, Mathf.Abs(player.movementInput.y));
        }
        else
        {
            player.animator.SetFloat(animatorSpeedVariable, 0f);
        }
    }
}
