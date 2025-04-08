using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public int DMG = 2;
    public float DMGMultiplier = 1;
    void Awake()
    {
        DMGMultiplier = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().DMGMultiplier;
    }
    void FixedUpdate()
    {
        transform.position += transform.right * bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("" + c.gameObject.name);
        if (!c.gameObject.CompareTag("Player") || c.gameObject.layer != 6)
        {
            if(c.gameObject.layer == 7)
            {
                c.gameObject.GetComponent<EnemyScript>().Health -= DMG * (int)DMGMultiplier;
            }
            GameObject.Destroy(gameObject);
        }
    }
}
