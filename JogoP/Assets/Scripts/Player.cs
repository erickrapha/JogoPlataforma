using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int heath = 3;
    public float velocidade;
    public float forçaPulo;
    public GameObject Bow;
    public Transform firePoint;
    private bool isJumping;
    private bool doubleJump;
    private bool isFire;
    private float movimente;
    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameController.instance.UpdateLives(heath);
    }

    // Update is called once per frame
    void Update()
    {
        Pulo();
        BowFire();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
    
        movimente = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movimente * velocidade, rig.velocity.y);

        //Leste
        if(movimente > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("Transição", 1);
            }
                
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //Oeste
        if(movimente < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("Transição", 1);
            }
             
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (movimente == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("Transição", 0);
        }
    }
    void Pulo()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("Transição", 2);
                rig.AddForce(new Vector2(0, forçaPulo), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("Transição", 2);
                    rig.AddForce(new Vector2(0, forçaPulo * 2), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }            
        }
    }
    void BowFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFire = true;
            anim.SetInteger("transition", 3);
            GameObject bow = Instantiate(Bow, firePoint.position, firePoint.rotation);

            if (transform.rotation.y == 0)
            {
                bow.GetComponent<Bow>().isRight = true;
            }

            if (transform.rotation.y == 180)
            {
                bow.GetComponent<Bow>().isRight = false;
            }

            yield return new WaitForSeconds(0.2f);
            isFire = false;
            anim.SetInteger("transition", 0);
        }
    }
    public void Damage(int dmg)
    {
        heath -= dmg;
        GameController.instance.UpdateLives(heath);
        anim.SetTrigger("hit");

        if (transform.rotation.y == 0)
        {
            transform.position = new Vector3(-0.5f, 0, 0);
        }

        if (transform.rotation.y == 180)
        {
            transform.position = new Vector3(0.5f, 0, 0);
        }

        if (heath <= 0)
        {
            GameController.instance.GameOver();
        }
    }
    public void IncreaseLife(int value)
    {
        heath += value;
        GameController.instance.UpdateLives(heath);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.layer == 7)
        {
            isJumping = false;
        }
        if (coll.gameObject.layer == 6)
        {
            GameController.instance.GameOver();
        }
    }
}
