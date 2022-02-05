using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MainCharacter : MonoBehaviour
{
    public GameObject camera;
    public Transform checkPoint;
    public GameObject health;
    public Rigidbody2D rb;
    public Image healthBar;
    public Canvas canv;
    
    private int speed = 9;
    private float _jumpForce = 1490;
    private bool _allowJump;
    private int _deathCount = 0;
    private int _hp = 100;
    private float _currentHp;
    private Vector3 _startingPosition;

    private void Start()
    {
        _startingPosition = checkPoint.position;
        Spawn();
    }

    void Update()
    {
        Movements();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DoorPortal")
        {
            if (other.GetComponent<Transform>() != null && other.GetComponent<SpriteRenderer>() != null)
            {
                _startingPosition = other.GetComponent<Transform>().position;
                other.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            
        }

        if (other.gameObject.tag == "TrapFaller")
        {
            if (other.GetComponent<TrapFaller>() != null)
            {
                other.GetComponent<TrapFaller>().Drop();
            }
        }
        
        EnemyTouch(other, null);
        
        CheckHealth();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (other.gameObject.tag == "PoisonedArea")
        {
            if (other.GetComponent<PoisonArea>() != null)
            {
                
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DeskAction(other, true, 300);
        
        EnemyTouch(null, other);
        
        CheckHealth();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        GroundAction(other, true, Color.green);
        
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        GroundAction(other, false, Color.white);
        
        DeskAction(other, false, -300);
    }



    //my functions
    
    private void EnemyTouch(Collider2D other1, Collision2D other2)
    {
        if (other1 != null)
        {
            if (other1.gameObject.tag == "Respawn")
            {
                if (other1.GetComponent<Enemies>() != null)
                {
                    _currentHp -= other1.GetComponent<Enemies>().damage;
                    RefreshHealthBar();
                }
            }
        }
        else if (other2 != null)
        {
            if (other2.gameObject.tag == "Respawn")
            {
                if (other2.gameObject.GetComponent<Enemies>() != null)
                {
                    _currentHp -= other2.gameObject.GetComponent<Enemies>().damage;
                    RefreshHealthBar();
                }
            }
        }
    }
    
    private void DeskAction(Collision2D toucher, bool allowJump, int jumpForce)
    {
        if (toucher.gameObject.tag == "Desk")
        {
            _jumpForce += jumpForce;
            _allowJump = allowJump;
        }
    }

    private void GroundAction(Collision2D toucher, bool allowJump, Color color)
    {
        if (toucher.gameObject.tag == "Ground")
        {
            _allowJump = allowJump;

            SpriteRenderer spr = toucher.gameObject.GetComponent<SpriteRenderer>();
            if (spr != null)
            {
                spr.color = color;
            }
        }
    }

    private void Movements()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SideMovements(-1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            SideMovements(1);
            
        }

        if (Input.GetKeyDown(KeyCode.W) && _allowJump == true )
        {
            rb.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void SideMovements(int direction)
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed * direction );
        if (transform.localScale.x * direction < 0)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z);
        }
    }

    private void RefreshHealthBar()
    {
        float currentHp = _currentHp;
        float hp = _hp;
        healthBar.fillAmount = currentHp / hp;
        
    }
    private void Spawn()
    {
        transform.position = _startingPosition;
        _currentHp = _hp;
        RefreshHealthBar();
    }

    private void Loose()
    {
        RefreshHealthBar();
        transform.position = new Vector3(-100, 2, 0);
        transform.localScale = new Vector3(2, 2, 0);

        canv.gameObject.SetActive(false);
        camera.GetComponent<CameraFollower>().transform.position = new Vector3(-100, 2, -100);
        camera.GetComponent<CameraFollower>().smoothing = 0;
        _currentHp = 100;
    }

    private void CheckHealth()
    {
        if (_currentHp <= 0)
        {
            _deathCount++;
            RefreshHealthBar();

            GameObject cup = health;
            if (cup != null)
            {
                GameObject tmp = cup.GetComponent<Cup>()._segments[cup.GetComponent<Cup>()._segments.Count - _deathCount].gameObject;
                if (tmp != null) Destroy(tmp);
            }

            if (_deathCount < 3)
            {
                Spawn();
            }
            else if (_deathCount >= 3)
            {
                Loose();
            }
        }
    }
}




