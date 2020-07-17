using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header(header: "Shooting")]
    private KeyCode shootKey;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public ShootBar shootBar;

    bool isShooting = false; // Variable for holding shoot button and then shoot
    private readonly float timeToShoot = 1.2f;
    private float currentTimeToShoot;

    void Start()
    {
        currentTimeToShoot = timeToShoot; // Player need to have ready shot at start of the game 
        shootBar.setMaxTime(timeToShoot);
        Init();
        animator = GetComponent<Animator>();   
    }

    void Update()
    {
        currentTimeToShoot += Time.deltaTime;
        shootBar.setTime(currentTimeToShoot);
        PlayerInput();
        movementInput = new Vector2(Input.GetAxis(horizontalMoveName), Input.GetAxis(vericalMoveName));   

        Animate();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movementInput.x * Time.fixedDeltaTime * moveSpeed, movementInput.y * Time.fixedDeltaTime * moveSpeed);  
    }

    void Init()
    {
        if (playerName.Equals("Fire"))
        {
            horizontalMoveName = "P1_Horizontal";
            vericalMoveName = "P1_Vertical";
            shootKey = KeyCode.G;
        }
        else if (playerName.Equals("Water"))
        {
            horizontalMoveName = "P2_Horizontal";
            vericalMoveName = "P2_Vertical";
            shootKey = KeyCode.Keypad1;
        }
    }

    void PlayerInput()
    {       
        if(currentTimeToShoot > timeToShoot)
        {
            if (Input.GetKeyDown(shootKey) && isShooting.Equals(false))
            {
                isShooting = true;
            }
        }
        if(Input.GetKeyUp(shootKey))
        {
            if(isShooting.Equals(true))
            {
                Shoot();
                isShooting = false;
                currentTimeToShoot = 0f;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }

    private void Animate()
    {
        if (movementInput.x != 0)
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
