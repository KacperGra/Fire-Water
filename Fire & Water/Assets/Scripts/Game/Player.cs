using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    [Header(header:"Android")]
    public GameObject androidUI;
    public Joystick joystick;
    public Text skill_1Text;
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
    private readonly float timeToShoot = 2.5f;
    private float currentTimeToShoot;

    // Input
    private Vector2 movementInput;
    private string horizontalMoveName;
    private string vericalMoveName;
    private KeyCode shootKey;

    // Skills ->
    public List<Skill> skills = new List<Skill>();

    private PlayerSkills playerSkills;
    private KeyCode Q_Skill;  


    void Start()
    {
        playerSkills = new PlayerSkills();
        playerSkills.UnlockSkill(PlayerSkills.SkillType.MULTI_SHOOT);


        if(FindObjectOfType<GameMaster>().androidMode.Equals(false))
        {
            androidUI.SetActive(false);
        }

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
        var cooldownValue = Mathf.Round(skills[0].currentCooldown * 10f) / 10f;


        if (skills[0].currentCooldown >= 0)
        {
            skills[0].currentCooldown -= Time.deltaTime;
            skill_1Text.text = cooldownValue.ToString() + '|' + skills[0].cooldown;

        }
        else
        {
            skill_1Text.text = "READY!"; 
        }


        if(currentTimeToShoot < timeToShoot)
        {
            currentTimeToShoot += Time.deltaTime;
        }
        shootBar.SetValue(currentTimeToShoot);
        

        ManaScript();

        if(FindObjectOfType<GameMaster>().androidMode.Equals(false))
        {
            PC_PlayerInput();
            Animate();
        } 
        
        if(FindObjectOfType<GameMaster>().androidMode.Equals(false))
        {
            movementInput = new Vector2(Input.GetAxis(horizontalMoveName), Input.GetAxis(vericalMoveName));
        }
        else
        {
            movementInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        
        
        gameObject.GetComponent<Rigidbody2D>().velocity *= new Vector2(0f, 0f);
    }

    private void FixedUpdate()
    {
        if (FindObjectOfType<GameMaster>().androidMode.Equals(true))
        {
            Android_PlayerInput();
            Animate();    
        }  
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

    void PC_PlayerInput()
    {
        if(Input.GetKeyUp(shootKey))
        {
            Shoot();    
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene((int)ScenesIndex.MAIN_MENU);
        }


        if(Input.GetKeyUp(Q_Skill))
        {
            UseSkill(PlayerSkills.SkillType.MULTI_SHOOT);
        }
    }

    void Android_PlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene((int)ScenesIndex.MAIN_MENU);
        }
    }

    public void Shoot()
    {
        if(currentTimeToShoot >= timeToShoot)
        {
            currentTimeToShoot = 0f;
            var bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = transform.rotation;
        }    
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

    public void AndroidInput(string _skillName)
    {
        switch(_skillName)
        {
            case "Multi Shoot":
                UseSkill(PlayerSkills.SkillType.MULTI_SHOOT);
                break;
        }
    }

    void UseSkill(PlayerSkills.SkillType _skillType)
    {
        if(playerSkills.IsSkillUnlocked(_skillType))
        {
            if(skills[(int)_skillType].IsReady.Equals(true))
            {
                if(mana - skills[(int)_skillType].manaCost >= 0)
                {
                    if(skills[(int)_skillType].currentCooldown <= 0)
                    {
                        mana -= skills[(int)_skillType].manaCost;
                        skills[(int)_skillType].currentCooldown = skills[(int)_skillType].cooldown;
                        MultiShoot();
                    }
                    
                }
            }
        }
    }

    public void MultiShoot()
    {
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
