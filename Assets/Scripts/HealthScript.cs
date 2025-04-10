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
        //the game manager.
        manager = GameObject.FindGameObjectWithTag("GameController");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //stupid way of checking if the player has fallen off the platforms.
        //FIXME: find a better way to check if player has fallen off.
        if (gameObject.transform.position.y < -50)
        {
            Health = 0;
        }

        //displays the player's health on the screen.
        //TODO: remove this
        text.text = Health.ToString();

        //odd way of displaying health.
        SpriteRenderer spriteRenderer = Sprite.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        //by default, the alpha of the green sprite (which reps the health) is 
        //set to 1 
        if (Health >= MaxHealth)
        {
            color.a = 1f;
        }else
        {
            /*
            if the health is lower than the max, it will set the alpha to 
            Health divided by MaxHealth, effectively giving us the effect that
            the green-ness of the player sprite shows how much health it has.
            */
            color.a = (float)Health/MaxHealth;           
        }
        spriteRenderer.color = color;

        //disables the player's movement and switches off it's constraints, making
        //it look dead.
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
        //dumb way to take DMG.
        //TODO: improve this.
        if (c.gameObject.CompareTag("Flying enemy"))
        {
            Health -= FlyingEnemyDMG;
        }
        //if the collided obj is the finish line, it stops the timer and
        //sets the player's state to "won".
        else if (c.gameObject.CompareTag("Finish"))
        {
            manager.GetComponent<GameManager>().TimerRunning = false;
            Won = true;
            WinScreen.SetActive(true);
        }
    }
}
