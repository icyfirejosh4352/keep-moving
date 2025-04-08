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
        if(TimerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
        TimeText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";

        if(PlayerRB.linearVelocity.magnitude > 0)
        {
            if(DMGMultiplier <= 10)
            {
                DMGMultiplier += 0.1f;
            }
        }
        else
        {
            if(DMGMultiplier > 1)
            {
                DMGMultiplier -= 0.4f;
            }else if(DMGMultiplier < 1)
            {
                DMGMultiplier = 1;
            }else if(DMGMultiplier > 10)
            {
                DMGMultiplier = 10;
            }
        }
        text.text = DMGMultiplier.ToString("F2");

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
    }
}
