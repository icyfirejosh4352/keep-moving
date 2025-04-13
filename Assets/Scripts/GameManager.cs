using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D PlayerRB;
    public HealthScript PlayerHealth;
    public float DMGMultiplier = 1f;
    public float DMGdegenRate = 0.4f;
    public float DMGmultMax = 10f;
    public TMP_Text text;
    public TMP_Text TimeText;
    public RectTransform bar;
    public float elapsedTime = 0f;
    public bool TimerRunning = false;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerRB = Player.gameObject.GetComponent<Rigidbody2D>();
        PlayerHealth = Player.gameObject.GetComponent<HealthScript>();
    }
    void FixedUpdate()
    {
        //-------------------------------timer--------------------------------//
        //this section is purely AI.
        //yes. I am dissappointed in myself.
        if(TimerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
        TimeText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
        //-------------------------------------------------------------------//


        //----------------------------DMG Multiplier-------------------------//
        /*
        simply uses the magnitude of the player's movement to decide how much the multiplier
        should increase.
        */
        if(PlayerRB.linearVelocity.magnitude > 0)
        {
            if(DMGMultiplier <= DMGmultMax)
            {
                DMGMultiplier += 0.4f;
            }else if (DMGMultiplier > DMGmultMax)
            {
                DMGMultiplier = DMGmultMax;
            }
        }
        else
        {
            if(DMGMultiplier > 1)
            {
                DMGMultiplier -= DMGdegenRate;
            }else if(DMGMultiplier < 1)
            {
                DMGMultiplier = 1;
            }else if(DMGMultiplier > DMGmultMax)
            {
                DMGMultiplier = DMGmultMax;
            }
        }
        text.text = DMGMultiplier.ToString("F2");
        //-------------------------------------------------------------------//

        //-------------------------respawn n stuff---------------------------//
        if(PlayerHealth.Health <= 0 && Input.GetKey(KeyCode.R))
        {
            // Get the active scene's name
            string sceneName = SceneManager.GetActiveScene().name;

            // Reload the active scene
            SceneManager.LoadScene(sceneName);
        }
        if(PlayerHealth.Won && Input.GetKey(KeyCode.R))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
        //-------------------------------------------------------------------//
    }
}
