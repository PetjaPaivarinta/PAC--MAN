using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    public AudioSource coinSound;
    

    public AudioSource deathSound;

    public Score scoreScript;

    private Rigidbody2D rb;

    public bool isPowerUpActive = false;
    private float powerUpDuration = 10f;
    private float powerUpTimer = 0f;

    private float ghostDeath = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (isPowerUpActive)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                DeactivatePowerUp();
            }
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        if (movement.x != 0 && movement.y != 0)
        {
            if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
            {
                movement.y = 0;
            }
            else
            {
                movement.x = 0;
            }
        }
        else if (movement == Vector2.zero)
        {
            movement = rb.velocity.normalized;
        }

        

        rb.velocity = movement * speed;

        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (isPowerUpActive && ghostDeath < 2)
            {
                ghostDeath++;
                collision.gameObject.SetActive(false);
            }
            else
            {
                deathSound.Play();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        if (collision.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            coinSound.Play();
            scoreScript.AddPoint(); // Increase the score
        }
        else if (collision.CompareTag("Cherry"))
        {
            ghostDeath = 0;
            collision.gameObject.SetActive(false);
            coinSound.Play();
            ActivatePowerUp();
        }
    }




    private void ActivatePowerUp()
    {
        isPowerUpActive = true;
        powerUpTimer = powerUpDuration;
    }

    private void DeactivatePowerUp()
    {
        isPowerUpActive = false;
    }
}