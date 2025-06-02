using NUnit.Framework.Internal;
using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerMovements : MonoBehaviour
{
    public float sideSPD;
    public float jumpSPD;
    public float speed_movelane;

    bool melompat;
    public int coin_counter;
    public int Quiz_threshold;
    public bool loseState;
    public Animator animator;

    private Vector2 startTouchPos;
    private Vector2 EndTouchPos;
    public int num = 2;

    private Vector3 updatePos;

    public bool bataskanan;
    public bool bataskiri;
    public bool isGrounded;
    public bool ISstanding;

    private bool canSwipe = true;
    public float swipeCooldown = 0.5f;

    public bool BUFFactive;

    // boolen tombol
    public bool tombol_movementSwipe;
    public bool tombol_movementKeyboard;

    //popup quiz
    public GameObject Q1;

    private GameObject player;

    //keperluan untuk move pesawat ke kanan dan ke kiri
    private Quaternion keKiri;
    private Quaternion keKanan;
    private Quaternion normal;

    public float rotationSpeed = 2f;

 Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //state movements
        melompat = false;
        loseState = false;
        updatePos = new Vector3(transform.position.x, transform.position.y, -11);

        player = this.gameObject;

        //pembatas
        bataskanan = false;
        bataskiri = false;
        ISstanding = true;

        //buff
        BUFFactive = false;

        //setup rotasi
        Vector3 rotate_kanan = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -35f);
        Vector3 rotate_kiri = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 35f);
        Vector3 rotate_normal = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);

        keKanan = quaternion.Euler(rotate_kanan);
        keKiri = quaternion.Euler(rotate_kiri);
        normal = quaternion.Euler(rotate_normal);


        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tombol_movementSwipe && !tombol_movementKeyboard)
        {
            movement_via_touch();
        }

        if (tombol_movementKeyboard && !tombol_movementSwipe)
        {
            movement_via_keyboard();
        }
       
        transform.position = Vector3.Lerp(transform.position, updatePos, speed_movelane * Time.deltaTime);

        if (BUFFactive)
        {
            buffEFFECT();
        }
        else
        {
            rb.useGravity = true;
        }

        if (Quiz_threshold == 5)
        {
            Quiz_threshold = 0;
            Time.timeScale = 0;
            Q1.SetActive(true);
        }

        if (canSwipe)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, normal, Time.deltaTime * rotationSpeed);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("coins"))
        {
            Destroy(collision.gameObject);
            coin_counter += 1;
            Quiz_threshold += 1;
            return;
        }

        if (collision.gameObject.CompareTag("obstacle"))
        {
            loseState = true;
        }

        if (collision.gameObject.CompareTag("palang") && ISstanding == true)
        {
            loseState = true;
        }

        if (collision.gameObject.CompareTag("levels"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("buff"))
        {
            StartCoroutine(buffCD());
            Destroy(collision.gameObject);
        }
    }


    void movement_via_touch()
    {
        if (!canSwipe) return; // Cegah input jika masih dalam cooldown

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            EndTouchPos = Input.GetTouch(0).position;

            float deltaX = EndTouchPos.x - startTouchPos.x;
            float deltaY = EndTouchPos.y - startTouchPos.y;

            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if (deltaX > 0 && !bataskanan)
                {
                    left();
                    StartCoroutine(SwipeCooldown());
                }
                else if (deltaX < 0 && !bataskiri)
                {
                    right();
                    StartCoroutine(SwipeCooldown());
                }
            }
            else
            {
                if (deltaY > 0)
                {
                    jump();
                }
                else
                {
                    StartCoroutine(slide());
                }
            }
        }
    }

    void movement_via_keyboard()
    {
        if (Input.GetKeyDown(KeyCode.D) && !bataskanan && canSwipe)
        {
            left();
            StartCoroutine(SwipeCooldown());
            transform.rotation = Quaternion.Lerp(transform.rotation, keKiri, Time.deltaTime * rotationSpeed);
        }

        if (Input.GetKeyDown(KeyCode.A) && !bataskiri && canSwipe)
        {
            right();
            StartCoroutine(SwipeCooldown());
            transform.rotation = Quaternion.Lerp(transform.rotation, keKanan, Time.deltaTime * rotationSpeed);
        }
    }

    void right()
    {
        //transform.Translate(-num * Time.deltaTime, 0, 0);

        updatePos = new Vector3(transform.position.x - num, transform.position.y, -11);
    }

    void left()
    {
        //transform.Translate(num * Time.deltaTime, 0, 0);
        updatePos = new Vector3(transform.position.x + num, transform.position.y, -11);
    }

    void jump()
    {
        rb.AddForce(Vector3.up * jumpSPD, ForceMode.Impulse);
        isGrounded = false;
    }

    void buffEFFECT()
    {
        transform.position = new Vector3(transform.position.x, (float)1.2, transform.position.z);
        rb.useGravity = false;
        GetComponent<MagnetObject>().ActivateMagnet(10f);
    }

    IEnumerator slide()
    {
        ISstanding = false;
        Debug.Log("sedang rolling");

        yield return new WaitForSeconds(2f);

        ISstanding = true;
        Debug.Log("selesai rolling");
    }

    IEnumerator SwipeCooldown()
    {
        canSwipe = false;
        yield return new WaitForSeconds(swipeCooldown);
        canSwipe = true;
        
    }

    IEnumerator buffCD()
    {
        BUFFactive = true;
        yield return new WaitForSeconds(10f);
        BUFFactive = false;
    }
}