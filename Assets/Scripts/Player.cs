using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    //state
    bool isAlive = true;
    float gravityScaleAtStart;

    //cached component references
    private Rigidbody2D myRigidBody2D;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider2D;
    private BoxCollider2D myFeetCollider2D;

	//messages then methods
	void Start () {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody2D.gravityScale;
    }
	
	void Update () {
        if ( ! isAlive) { return; }

        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump() {
        if (! myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody2D.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder() {

        if (! myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myAnimator.SetBool("Climbing", false);
            myRigidBody2D.gravityScale = gravityScaleAtStart;
            return;
        }

        
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * climbSpeed);
        myRigidBody2D.velocity = climbVelocity;
        myRigidBody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

        //if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
        //    float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        //    Vector2 playerVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * runSpeed);
        //    myRigidBody2D.velocity = playerVelocity;
        //
        //    bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
        //    myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
        //}
        //else { myAnimator.SetBool("Climbing", false); }
    }

    private void Die() {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            //float enemyDirection = Mathf.Sign(myBodyCollider2D.IsTouching());
            GetComponent<Rigidbody2D>().velocity = deathKick;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.localScale.x) * deathKick.x, deathKick.y);
            //Mathf.Sign(myRigidBody2D.velocity.x)
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite() {

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
                transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x),1f);
        }
    }
}
