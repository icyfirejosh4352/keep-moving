using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data;

public class HealthScript : MonoBehaviour
{
    public int Health = 100;
    public int MaxHealth = 100;
    public int MaxExtraHealth = 150;
    public int FlyingEnemyDMG = 10;
    public TMP_Text text;
    public GameObject Sprite;
    public MovementScript mov;
    public GameObject weapons;
    public GameObject DeathScreen;
    public GameObject WinScreen;
    public Rigidbody2D rb;
    public GameObject manager;
    public bool Won = false;

    void Awake()
    {
        Health = MaxHealth;
        manager = GameObject.FindGameObjectWithTag("GameController");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y < -50)
        {
            Health = 0;
        }
        text.text = Health.ToString();
        SpriteRenderer spriteRenderer = Sprite.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        if (Health >= MaxHealth)
        {
            color.a = 1f;
        }else
        {
            color.a = (float)Health/MaxHealth;           
        }
        spriteRenderer.color = color;

        if(Health <= 0)
        {
            mov.enabled = false;
            weapons.SetActive(false);
            DeathScreen.SetActive(true);
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("collided with " + c.gameObject.name);
        // if (c.gameObject.layer == 7)
        // {
        //     if (c.gameObject.CompareTag("Flying enemy"))
        //     {
        //         Health -= FlyingEnemyDMG;
        //     }
        // }
        if (c.gameObject.CompareTag("Flying enemy"))
        {
            Health -= FlyingEnemyDMG;
        }else if (c.gameObject.CompareTag("Finish"))
        {
            manager.GetComponent<GameManager>().TimerRunning = false;
            Won = true;
            WinScreen.SetActive(true);
        }
    }
}
