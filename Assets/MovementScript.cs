using Unity.Mathematics;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float MovementSpeed = 1f;   //speed of player.
    public float SlidingSpeed = 2f;    //speed of player while sliding.
    public float JumpForce = 10f;      //force applied on jump.
    public float SlamForce = 10f;      //force applied on groundslam.
    public Rigidbody2D rb;             //player's rigidbody for physics.
    public GameObject graphics;        //the player sprite
    public Transform FloorCheck;       //empty gameobject to check for ground and shi- stuff.
    public Transform CeilingCheck;     //empty gameobject to check for ceilings n shtuff.
    public Transform ForwardCheck;     //empty gameobject to check for stuff in front.
    public float CheckRadius = 5f;     //radius of the OverlapCircle for ceiling and ground check.
    public float horizontalForce = 0;  //horizontal input.
    public float verticalForce = 0;    //vertical input.    
    public bool IsGrounded = true;     //grounded check.
    public bool CeilingAbove = false;  //ceiling check.
    public bool IsBlocked = false;     //obstacle check.
    public bool IsSliding = false;     //sliding check.
    public bool IsFacingRight = true;  //facing direction check.

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        horizontalForce = Input.GetAxisRaw("Horizontal");
        verticalForce = Input.GetAxisRaw("Vertical");

        //----------------grounded and ceiling check----------------//
        //by default the player isnt grounded nor has a ceiling above him
        IsGrounded = false;
        CeilingAbove = false;
        IsBlocked = false;

        //check for all colliders within radius.
        Collider2D[] CeilingColliders = Physics2D.OverlapCircleAll(CeilingCheck.position, CheckRadius);
        Collider2D[] FloorColliders = Physics2D.OverlapCircleAll(FloorCheck.position, CheckRadius);
        Collider2D[] ForwardColliders = Physics2D.OverlapCircleAll(ForwardCheck.position, CheckRadius);

        //loop to check for colliders
        foreach (Collider2D c in CeilingColliders)
        {
            if (c.gameObject.layer == 3)
            {
                CeilingAbove = true;
            }
        }        

        //loop to check for colliders
        foreach (Collider2D c in FloorColliders)
        {
            //if the gameobject is ground, the player is grounded.
            if (c.gameObject.layer == 3)
            {
                IsGrounded = true;
            }
        }

        //loop to check for colliders
        foreach (Collider2D c in ForwardColliders)
        {
            //if the gameobject is ground, the player is grounded.
            if (c.gameObject.layer == 3)
            {
                IsBlocked = true;
            }
        }
        //----------------------------------------------------------//

        //----------------------velocity reset----------------------//
        //if the player is doing absolutely nothing, then set velocity to 0.
        //will be changed once I get a better movement system.
        // if (!IsSliding && horizontalForce == 0 && verticalForce == 0 && IsGrounded)
        // {
        //     rb.velocity = Vector3.zero;
        // }
        //----------------------------------------------------------//

        //------------------------facing----------------------------//
        //flip the player's graphics.
        graphics.transform.localScale = new Vector3(IsFacingRight ? 1 : -1, graphics.transform.localScale.y, 1);

        if(horizontalForce > 0 && !IsFacingRight && !IsSliding)
        {
            IsFacingRight = true;
        }else if(horizontalForce < 0 && IsFacingRight && !IsSliding)
        {
            IsFacingRight = false;
        }
        //----------------------------------------------------------//

        //-------------mainly vertical stuff here-------------------//
        //jumping. FIXME: the distance of the the jump is real wonky. might have something to do with grounding?
        if (Input.GetKey(KeyCode.Space) && IsGrounded && !IsSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            IsGrounded = false;
        }else if(verticalForce > 0 && IsGrounded && !IsSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            IsGrounded = false;          
        }
        SlideAndSlam();
        //----------------------------------------------------------//

        //-----------------------horizontal movement----------------//
        if (!IsSliding)
        {
            rb.linearVelocity = new Vector2(horizontalForce * MovementSpeed, rb.linearVelocity.y);
        }    
        //----------------------------------------------------------//

        // if(!IsSliding)
        // {
        //     transform.position += new Vector3(MovementSpeed * horizontalForce * Time.deltaTime,0);
        // }

        if(Input.GetKey(KeyCode.F))
        {
            transform.position = Vector2.zero;
        }
    }

   /* void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == 3)
        {
            IsGrounded = true;
        }
    }
*/
    /// <summary>
    /// Handles sliding and groundslamming. there's a better way to do 90% of this but I'm dumb.
    /// </summary>
    void SlideAndSlam()
    {
        //this var uses IsFacingRight to determine direction of slide. 
        int x = IsFacingRight ? 1 : -1;

        if (verticalForce < 0)
        {
            //rotates the player depending on the direction. 
            //! I should probably use anims, but
            //! I'm bad at art and I'm lazy.
            //TODO: art ig
            if (IsGrounded && !IsSliding && !IsBlocked)
            {
                transform.Rotate(0,0, IsFacingRight ? 60 : -60);
                IsSliding = true;
            }else if (!IsGrounded)
            {
                //groundslam.
                rb.AddForce(new Vector2(0, -SlamForce), ForceMode2D.Impulse);
            }
        }else if (verticalForce >= 0 && IsSliding && !CeilingAbove)
        {
                //resets player rotation and sets sliding to false when no input is received.
                IsSliding = false;
                transform.rotation = quaternion.identity;
            
        }
        //make the player go fast.
        if(IsSliding)
        {
            rb.linearVelocity = new Vector2(SlidingSpeed * x, rb.linearVelocity.y);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CeilingCheck.position, CheckRadius);
        Gizmos.DrawWireSphere(FloorCheck.position, CheckRadius);
        Gizmos.DrawWireSphere(ForwardCheck.position, CheckRadius);
    }
}
