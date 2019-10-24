using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    private int count;

    public Text winText;

    public Text countText;

    public Text lives;

    private int life;

    public Text looseText;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        life = 3;
        winText.text = "";
        SetCountText();
        SetLifeText();
        looseText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
            if (count == 4)
            {
                transform.position = new Vector2(.0f, 95.0f);
                life = 3;
                SetLifeText();
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            life = life - 1;
            SetLifeText();
        }
       
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You win, Game Created By Tennyson";
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
    }
    void SetLifeText()
    {
        lives.text = "Lives: " + life.ToString();
        if (life <= 0)
        {
            looseText.text = "Loooooser";
            Destroy(this);
        }
    }
}