using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 0.1f;
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    float maxJumpLenght = 0.3f;
    [SerializeField]
    float maxJumpPower = 0.1f;

    [SerializeField]
    AnimationCurve jumpPowerCurve;


    [SerializeField]
    Image blackHoleDangerImage;

    [SerializeField]
    Vector2 groundCheckExtents = new Vector2(0.6f, 0.2f);

    [SerializeField]
    DeathPanel deathPanel;

    [SerializeField]
    LayerMask isGround;

    float blackHoleDanger = 1;

    bool jump;
    Vector2 moveInput;

    float jumpStarted = -int.MaxValue;

    bool onGround = false;

    InputReader inputReader;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    AreaEffector2D areaEffector;
    Transform flipper;
    Transform suckParent;

    float areaEffectorForce;

    bool suck;


    bool dead = false;

    Vector3 lastCheckpoint;

    private void Start()
    {
        flipper = transform.Find("Flipper");
        suckParent = transform.Find("Flipper/SuckParent");

        inputReader = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputReader>();


        inputReader.jumpStartEvent += OnStartJump;
        inputReader.jumpEndEvent += OnEndJump;
        inputReader.suckStartEvent += OnSuckStart;
        inputReader.suckEndEvent += OnSuckEnd;
        inputReader.restartEvent += OnRestart;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        areaEffector = GetComponentInChildren<AreaEffector2D>();

        areaEffectorForce = areaEffector.forceMagnitude;

        lastCheckpoint = transform.position;

        suckParent.gameObject.SetActive(false);
        deathPanel.gameObject.SetActive(false);
    }

    private void OnRestart()
    {
        Respawn();
    }

    private void OnSuckStart()
    {
        suck = true;
        suckParent.gameObject.SetActive(true);
        anim.SetBool("suck", true);
    }
    private void OnSuckEnd()
    {
        suck = false;
        suckParent.gameObject.SetActive(false);
        anim.SetBool("suck", false);
    }

    

    private void OnStartJump()
    {
        jump = true;
    }
    private void OnEndJump()
    {
        jump = false;
    }

    private void Update()
    {
        moveInput = inputReader.moveDirection;
    }

    private void FixedUpdate()
    {
        if (dead)
            return;

        //onGround = Physics2D.OverlapPoint(groundCheck.position) != null;
        onGround = Physics2D.OverlapBox(groundCheck.position, groundCheckExtents, 0, isGround) != null;

        Vector2 move = rb.velocity;

        move.x = moveInput.x * speed;



        if (jumpStarted > Time.time - maxJumpLenght)
        {
            if (jump)
                move.y += jumpPowerCurve.Evaluate((jumpStarted + maxJumpLenght - Time.time) / maxJumpLenght) * maxJumpPower;
            else
            {
                jump = false;
                jumpStarted = int.MinValue;
            }
        }
        else
        {
            if (jump && onGround && rb.velocity.y < 0.05f)
            {
                jumpStarted = Time.time;
                move.y = 0;
            }
        }

        rb.velocity = move;





        anim.SetFloat("speed", Mathf.Abs(move.x));
        anim.SetFloat("velocityY", rb.velocity.y);

        if (move.x > 0.1f)
        {
            flipper.localScale = new Vector3(-1, 1, 1);
            areaEffector.forceMagnitude = -areaEffectorForce;
            sr.flipX = true;
        }
        else if (move.x < -0.1f)
        {
            flipper.localScale = new Vector3(1, 1, 1);
            areaEffector.forceMagnitude = areaEffectorForce;
            sr.flipX = false;
        }

        if (suck)
        {
            blackHoleDanger -= Time.deltaTime * 0.1f;
        }

        blackHoleDangerImage.fillAmount = blackHoleDanger;

        if (blackHoleDanger < 0)
        {
            Die();
        }
    }

    public void RepairBlackHole()
    {
        blackHoleDanger = 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckExtents);
    }

    public void Die()
    {
        dead = true;

        deathPanel.gameObject.SetActive(true);
    }

    

    public void Respawn()
    {
        dead = false;
        transform.position = lastCheckpoint;
        deathPanel.gameObject.SetActive(false);
        RepairBlackHole();
    }

    public void SetCheckpoint()
    {
        lastCheckpoint = transform.position;
    }
}
