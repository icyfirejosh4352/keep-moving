using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int DMG = 2;
    public float DMGMultiplier = 1;
    void Awake()
    {
        //gets the muliplier from the game manager.
        DMGMultiplier = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DMGMultiplier;
    }
    void FixedUpdate()
    {
        //continuously moves the bullet forwards.
        //TODO: make this physics based.
        transform.position += transform.right * bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.gameObject.name);
        //checks if the collided obj is the player or another bullet
        if (!c.gameObject.CompareTag("Player") || c.gameObject.layer != 6)
        {
            //if not, it checks if the collided obj is of the Layer "Enemy" (7)
            if(c.gameObject.layer == 7)
            {
                //subtracts the bullet DMG multiplied by the multiplier from the enemy's health.
                c.gameObject.GetComponent<EnemyScript>().Health -= DMG * (int)DMGMultiplier;
            }
            //if the collided obj isn't the player or another bullet, it destroys itself on contact. 
            GameObject.Destroy(gameObject);
        }
    }
}
