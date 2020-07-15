using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public new Rigidbody2D rigidbody;
    [HideInInspector]
    public Sprite image;

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.right * moveSpeed;
    }
}
