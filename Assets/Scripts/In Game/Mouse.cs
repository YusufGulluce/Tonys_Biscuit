using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    public static int type = 0;

    [SerializeField]
    private StageController stageController;

    [SerializeField]
    private float invincibleCooldown = 1f;
    private float invincibleTimer = 0f;

    [SerializeField]
    private float damagedTime = 0.1f;
    private float damagedTimer = 0f;

    [SerializeField]
    private float damagePush = 1f;

    private Vector2 damageVector = Vector2.zero;

    [SerializeField]
    private float dashDamage = 1f;
    [SerializeField]
    private float jumpDamage = 1f;



    [SerializeField]
    private float HP = 100;
    private float maxHP;

    [SerializeField]
    private float dps = 1f;
    private float hungerTimer = 0f;



    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private DashBar dashBar;



    [SerializeField]
    private float runForce = 1;
    [SerializeField]
    private float jumpForce = 1;

    [SerializeField]
    private int maxJumpCount = 1;
    private int jumpCount;

    private Vector2 speed;

    private float oldYSpeed = 0f;

    private Vector2 dashVector;
    private Vector2 dash;
    [SerializeField]
    private float dashMaxTimer = 1f;
    [SerializeField]
    private float dashCoolDown = 1f;
    [SerializeField]
    private float dashForce = 2;
    private float dashTimer = 0f;
    private float dashCoolDownTimer = 0f;

    private bool facingRight = false;

    private float slowTimer;
    private float slowMult;
    private bool inSlow;

    public Vector2 wind;

    private SpriteRenderer sr;
    private Color color;

    private bool died = false;


    private bool running = false;
    private bool jumped = false;
    private bool inAir = false;
    private bool dashed = false;

    [Header("Sprite and Frame")]
    [SerializeField]
    private Sprite[] runSprites0;
    [SerializeField]
    private Sprite[] runSprites1;
    [SerializeField]
    private Sprite[] runSprites2;

    [SerializeField]
    private Sprite[] idleSprites0;
    [SerializeField]
    private Sprite[] idleSprites1;
    [SerializeField]
    private Sprite[] idleSprites2;

    [SerializeField]
    private Sprite[] jumpSprites0;
    [SerializeField]
    private Sprite[] jumpSprites1;
    [SerializeField]
    private Sprite[] jumpSprites2;

    [SerializeField]
    private Sprite[]   inAirSprite;

    [SerializeField]
    private Sprite[]   dashSprite;

    [SerializeField]
    private float frameConstant;
    [SerializeField]
    private float runFrameConstant;
    private int frameIndex = 0;
    private float frameTimer = 0f;

    [Header("Audio")]
    [SerializeField]
    private float volume;
    [SerializeField]
    private AudioClip[] audios; // 0 - jump, 1 - dash, 2 - water


    private Rigidbody2D rb;

    public void Set()
    {
        switch(type)
        {
            case 0:
                HP = 200f;
                runForce = 40f;
                jumpForce = 50f;
                invincibleCooldown = 1f;
                damagedTime = 0.04f;
                damagePush = 100f;
                maxJumpCount = 1;
                dashMaxTimer = .13f;
                dashCoolDown = 1.7f;
                dashForce = 150f;
                dps = 1f;

                dashDamage = 3f;
                jumpDamage = 1f;
                break;
            case 1:
                HP = 140f;
                runForce = 50f;
                jumpForce = 49f;
                invincibleCooldown = .8f;
                damagedTime = 0.04f;
                damagePush = 150f;
                maxJumpCount = 2;
                dashMaxTimer = .13f;
                dashCoolDown = 1.5f;
                dashForce = 150f;
                dps = 1f;

                dashDamage = 2f;
                jumpDamage = .8f;
                break;
            case 2:
                HP = 290f;
                runForce = 35f;
                jumpForce = 40f;
                invincibleCooldown = 1.5f;
                damagedTime = 0.04f;
                damagePush = 60f;
                maxJumpCount = 1;
                dashMaxTimer = .14f;
                dashCoolDown = 2f;
                dashForce = 110f;
                dps = .6f;

                dashDamage = 4.2f;
                jumpDamage = 1.4f;
                break;
            default:
                break;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dash = Vector2.right;
        jumpCount = maxJumpCount;
        speed = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        color = sr.color;

        slowMult = 1f;
        slowTimer = 0f;

        wind = Vector2.zero;

        Set();
        maxHP = HP;
        healthBar.Set(maxHP);
        dashBar.Set(dashCoolDown);
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        ApplyToPhysics();        
    }

    private void FixedUpdate()
    {
        rb.linearVelocity += wind;
    }

    private void ApplyToPhysics()
    {

        if (slowTimer > 0f || inSlow)
        {
            slowTimer -= Time.deltaTime;

            speed.x *= slowMult;
            if(slowTimer <= 0f && !inSlow)
                slowMult = 1f;            
        }
        rb.linearVelocity = new Vector2(speed.x, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && dashCoolDownTimer <= 0f)
        {
            if (type == 0)
                invincibleTimer = 99f;
            oldYSpeed = rb.linearVelocity.y;
            dashTimer = dashMaxTimer;
            dashCoolDownTimer = dashCoolDown;
            dashBar.Trigger();

            GetDamaged(dashDamage);

            transform.right = dash;

            dashed = true;
            ResetAnimation();

            AudioSource.PlayClipAtPoint(audios[1], transform.position - Vector3.forward * 90f);
        }
        if(dashTimer > 0f)
        {
            rb.linearVelocity = dash;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                dashed = false;
                rb.linearVelocity = Vector2.zero;
                if(type == 0)
                    invincibleTimer = 0f;
            }

        }
        if(dashCoolDownTimer > 0f)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }

        if(damagedTimer > 0f)
        {
            rb.linearVelocity = damageVector;
            damagedTimer -= Time.deltaTime;
            if(damagedTimer <= 0f)
            {
                sr.color = color;

                damagedTimer = 0f;
                damageVector = Vector2.zero;
                rb.linearVelocity = Vector2.zero;
            }
        }

        if(invincibleTimer > 0f)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0f)
                invincibleTimer = 0f;
        }

        hungerTimer -= Time.deltaTime;
        if(hungerTimer <= 0f)
        {
            hungerTimer = 1f;
            GetDamaged(dps);
        }

        if (!running && speed.x != 0)
        {
            running = true;
            ResetAnimation();
        }
        else if(running && speed.x == 0)
        {
            running = false;
            ResetAnimation();
        }




        frameTimer -= Time.deltaTime;
        if(frameTimer <= 0)
        {
            Animate();
        }


    }

    private void GetInputs()
    {
        speed = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed.x -= runForce;
            if (facingRight)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed.x += runForce;
            if (!facingRight)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = true;
        }

        if(dashTimer <= 0f)
            DashInputs();
    }

    private void Jump()
    {
        if(jumpCount > 0)
        {
            AudioSource.PlayClipAtPoint(audios[0], transform.position - Vector3.forward * 90f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount--;

            GetDamaged(jumpDamage);

            jumped = true;
            ResetAnimation();
        }
    }

    private void DashInputs()
    {

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            dashVector = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
                dashVector.x = -1;

            if (Input.GetKey(KeyCode.RightArrow))
                dashVector.x = 1;

            if (Input.GetKey(KeyCode.DownArrow))
                dashVector.y = -1f;

            if (Input.GetKey(KeyCode.UpArrow))
                dashVector.y = 1f;
        }

        dash = dashVector.normalized * dashForce;

    }

    private void GetDamaged(float damage)
    {
        if(!died)
        { 
            HP -= damage;

            if (HP < 0f)
            {
                stageController.DieScreen();
                died = true;
            }
            else if (HP > maxHP)
                HP = maxHP;


            healthBar.UpdateHP(HP);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.transform.gameObject.layer == 4)
            AudioSource.PlayClipAtPoint(audios[2], transform.position - Vector3.forward * 90f);

        if (collision.transform.gameObject.layer == 16 && invincibleTimer <= 0f) //harmfull obstacles
            HitHarmfull(collision.transform.gameObject.GetComponent<HarmfullObjects>());
        else if (collision.transform.gameObject.layer == 14)
        {
            Destroy(collision.gameObject);
            GetDamaged(-30f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == 15)
        {
            Vector2 point = collision.contacts[0].point;
            float angleRad = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad + 90;
            transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        }


        if (collision.transform.gameObject.layer == 16 && invincibleTimer <= 0f) //harmfull obstacles
            HitHarmfull(collision.transform.gameObject.GetComponent<HarmfullObjects>());
        
        else if (collision.transform.gameObject.layer == 15 || collision.gameObject.layer == 17)   // Touch ground
            jumpCount = maxJumpCount;

    }

    private void HitHarmfull(HarmfullObjects trap)
    {
        sr.color = Color.red;
        oldYSpeed = rb.linearVelocity.y;
        damageVector = transform.up * damagePush;
        invincibleTimer = invincibleCooldown;
        damagedTimer = damagedTime;

        Camera.main.GetComponent<CameraFollow>().Focus(.05f, trap.damage * .05f, trap.damage * -.2f);
        GetDamaged(trap.damage);
        if(trap.slowTime > 0f)
        {
            slowMult = trap.slowMult;
            slowTimer = trap.slowTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Vector2 point = collision.contacts[0].point;

        //float angleRad = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x);
        //float angleDeg = (180 / Mathf.PI) * angleRad + 90;
        //transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);

        if (collision.transform.gameObject.layer == 15 || collision.gameObject.layer == 17)
            if (inAir)
                inAir = false;

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == 15 || collision.gameObject.layer == 17)   // Touch ground
        {
            //rb.gravityScale = 5f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (!inAir)
                inAir = true;
        }
    }

    public void Slow(float slowMult)
    {
        this.slowMult = slowMult;
        inSlow = true;
    }

    public void UnSlow()
    {
        inSlow = false;
    }



    private void ResetAnimation()
    {
        frameIndex = 0;
        Animate();
    }

    private void Animate()
    {
        frameTimer = frameConstant;
        if(dashed)
        {
            sr.sprite = dashSprite[type];
        }
        else if (jumped)
        {
            Sprite[] idleSprites;
            switch(type)
            {
                case 0:
                    idleSprites = jumpSprites0;
                    break;
                case 1:
                    idleSprites = jumpSprites1;
                    break;
                case 2:
                    idleSprites = jumpSprites2;
                    break;
                default:
                    idleSprites = jumpSprites0;
                    break;
            }
            frameIndex %= idleSprites.Length;
            sr.sprite = idleSprites[frameIndex];
            frameIndex++;
            if (frameIndex >= idleSprites.Length)
            {
                frameIndex = 0;
                jumped = false;
            }
        }
        else if(inAir)
        {
            sr.sprite = inAirSprite[type];
        }
        else if(running)
        {
            Sprite[] idleSprites;
            switch (type)
            {
                case 0:
                    idleSprites = runSprites0;
                    break;
                case 1:
                    idleSprites = runSprites1;
                    break;
                case 2:
                    idleSprites = runSprites2;
                    break;
                default:
                    idleSprites = runSprites0;
                    break;
            }
            frameIndex %= idleSprites.Length;
            sr.sprite = idleSprites[frameIndex];
            frameIndex++;

            frameTimer = runFrameConstant / Mathf.Abs(speed.x);
        }
        else
        {
            frameTimer *= 5f;
            Sprite[] idleSprites;
            switch (type)
            {
                case 0:
                    idleSprites = idleSprites0;
                    break;
                case 1:
                    idleSprites = idleSprites1;
                    break;
                case 2:
                    idleSprites = idleSprites2;
                    break;
                default:
                    idleSprites = idleSprites0;
                    break;
            }
            frameIndex %= idleSprites.Length;
            sr.sprite = idleSprites[frameIndex];
            frameIndex++;
        }   //idle
    }





}
