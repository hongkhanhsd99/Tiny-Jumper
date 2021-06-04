using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumeForce;
    public Vector2 jumeForceUp;
    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;

    [HideInInspector]
    public int lastPlattformId;

    bool isJump;
    bool powerSetted;

    Rigidbody2D rb;
    Animator anim;

     float m_curPowerBarVal;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Ins.IsGameStarted)
        {
            SetPower();
            if (Input.GetMouseButtonDown(0))
            {
                SetPower(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SetPower(false);
            }
        }
    }

    void SetPower()
    {
        if (!powerSetted || isJump)
        {
            return;
        }
        jumeForce.x += jumeForceUp.x * Time.deltaTime;
        jumeForce.y += jumeForceUp.y * Time.deltaTime;

        jumeForce.x = Mathf.Clamp(jumeForce.x, minForceX, maxForceX);
        jumeForce.y = Mathf.Clamp(jumeForce.y, minForceY, maxForceY);
        m_curPowerBarVal += GameManager.Ins.powerBarUp * Time.deltaTime;
        GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
    }

    public void SetPower(bool isHoldingMouse)
    {
        powerSetted = isHoldingMouse;
        if (powerSetted || isJump)
        {
            return;
        }
        Jump();
    }

    void Jump()
    {
        if (!rb || jumeForce.x <= 0 || jumeForce.y <= 0)
            return;
        rb.velocity = jumeForce;
        isJump = true;
        if (anim)
        {
            anim.SetBool("isJump", true);
        }
        AudioController.Ins.PlaySound(AudioController.Ins.jump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConst.GROUND))
        {
            Plattform p = collision.transform.root.GetComponent<Plattform>();
            if (isJump)
            {
                isJump = false;
            }
            if (anim)
            {
                anim.SetBool("isJump", false);
            }
            if (rb)
            {
                rb.velocity = Vector2.zero;
            }
            jumeForce = Vector2.zero;

            if (p && p.id != lastPlattformId)
            {
                GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
                lastPlattformId = p.id;

                GameManager.Ins.AddScore();
            }

            m_curPowerBarVal = 0;
            GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
        }
        if (collision.CompareTag(TagConst.DEAD_ZONE))
        {
            Destroy(gameObject);
            GameGUIManager.Ins.ShowGameOverDialog();
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
        }
    }
}
