using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Variables
    private Animator animator;
    [Header(header: "Details")]
    public string playerName;
    public float moveSpeed;
    [HideInInspector] public int health;
    private readonly int maxHealth = 6;
    public Bar manaBar;
    [HideInInspector] public float mana;
    private readonly float maxMana = 100f;
    private readonly float manaRegenValue = 0.5f;
    private readonly float timeManaRegen = 0.25f;
    private float currentTimeManaRegen = 0f;
    public Text manaText;

    [Header(header: "Shooting")]    
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public Bar shootBar;
    bool isShooting = false; // Variable for holding shoot button and then shoot
    private readonly float timeToShoot = 2.5f;
    private float currentTimeToShoot;

    // Input
    private Vector2 movementInput;
    private string horizontalMoveName;
    private string vericalMoveName;
    private KeyCode shootKey;

    // Skills ->
    public Skill[] skill = new Skill[0]; 
    private KeyCode Q_Skill;
    
    #endregion

    #region Functions
    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        manaBar.SetMaxValue(maxMana);

        animator = GetComponent<Animator>();
        currentTimeToShoot = timeToShoot; // Player need to have ready shot at start of the game 
        shootBar.SetMaxValue(timeToShoot);
        Init();
    }

    void Update()
    {
        if (isShooting.Equals(false))
        {
            currentTimeToShoot += Time.deltaTime;
        }
        shootBar.SetValue(currentTimeToShoot);

        for(int i = 0; i < skill.Length; ++i)
        {
            if(skill[i].IsBought.Equals(true))
            {
                if (skill[i].IsReady.Equals(false))
                {
                    skill[i].Update();
                }
                if(skill[i].currentCooldown < 0)
                {
                    skill[i].IsReady = true;
                }
            }
        }

        ManaScript();

        PlayerInput();

        movementInput = new Vector2(Input.GetAxis(horizontalMoveName), Input.GetAxis(vericalMoveName));  
        Animate();
        gameObject.GetComponent<Rigidbody2D>().velocity *= new Vector2(0f, 0f);
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movementInput.x * Time.fixedDeltaTime * moveSpeed, movementInput.y * Time.fixedDeltaTime * moveSpeed);  
    }

    void ManaScript()
    {
        manaBar.SetValue(mana);

        var roundedMana = Mathf.Round(mana);
        manaText.text = roundedMana.ToString() + '/' + maxMana.ToString();
        if (mana < maxMana)
        {
            currentTimeManaRegen += Time.deltaTime;
            if (currentTimeManaRegen > timeManaRegen)
            {
                mana += manaRegenValue;
                currentTimeManaRegen = 0;
            }
        }
    }

    public void TakeDamage()
    {
        --health;
        if(health <= 0)
        {
            var gameMaster = FindObjectOfType<GameMaster>();
            gameMaster.GameOver();
        }
    }

    void Init()
    {
        if (playerName.Equals("Fire"))
        {
            horizontalMoveName = "P1_Horizontal";
            vericalMoveName = "P1_Vertical";
            shootKey = KeyCode.G;
            Q_Skill = KeyCode.Q;
        }
        else if (playerName.Equals("Water"))
        {
            horizontalMoveName = "P2_Horizontal";
            vericalMoveName = "P2_Vertical";
            shootKey = KeyCode.Keypad1;
            Q_Skill = KeyCode.Keypad2;
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        // Skill Input section ->
        var multiShoot = skill[(int)SkillsIndex.MULTI_SHOOT];
        if(Input.GetKeyUp(Q_Skill) && multiShoot.IsReady.Equals(true))
        {
            MultiShoot(multiShoot.manaCost);
            skill[(int)SkillsIndex.MULTI_SHOOT].IsReady = false;
            skill[(int)SkillsIndex.MULTI_SHOOT].currentCooldown = multiShoot.cooldown;
        }
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = transform.rotation;
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

    void MultiShoot(float _manaCost)
    {
        if (mana - _manaCost >= 0)
        {
            mana -= _manaCost;
            GameObject[] bullet = new GameObject[3];
            for (int i = 0; i < 3; ++i)
            {
                bullet[i] = Instantiate(bulletPrefab) as GameObject;
                bullet[i].transform.position = shootPoint.position;
                bullet[i].transform.rotation = transform.rotation;
            }
            var rotationValue = 10f;
            if (transform.rotation.y == 0) // if right
            {
                bullet[1].transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, rotationValue));
                bullet[2].transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, -rotationValue));
            }
            else if (transform.rotation.y == 1) // if left
            {
                bullet[1].transform.rotation = Quaternion.Euler(new Vector3(0, -180f, rotationValue));
                bullet[2].transform.rotation = Quaternion.Euler(new Vector3(0, -180f, -rotationValue));
            }
        }  
    }

    #endregion
}

[CreateAssetMenu(menuName = "Create Skill")]
public class Skill : ScriptableObject
{
    public float cooldown;
    public float currentCooldown;
    public float manaCost;
    public bool IsReady;
    public bool IsBought;

    public void Update()
    {
        if(IsReady.Equals(false))
        {
            currentCooldown -= Time.deltaTime;
            if(currentCooldown < 0)
            {
                IsReady = true;
            }
        }
    }
}
