using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapSpriteFacingScript : MonoBehaviour {
    public Sprite leftIdle;
    public Sprite leftUp;
    public Sprite leftDown;
    public Sprite leftGoing;
    public Sprite rightIdle;
    public Sprite rightUp;
    public Sprite rightDown;
    public Sprite rightGoing;

    private SpriteRenderer spriteRenderer;
    public bool facingRight = true;

    void Start ()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = rightIdle;
    }

    void Update ()
    {
        // Movement is being applied - ensure correct movement sprite set
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            updatePlayerSprite();
        }
        else if (facingRight)
        {
            spriteRenderer.sprite = rightIdle;
        }
        else
        {
            spriteRenderer.sprite = leftIdle;
        }
    }

    void updatePlayerSprite ()
    {
        if (Input.GetAxis("Horizontal") > 0)    // Moving rightward
        {
            facingRight = true;

            if (Input.GetAxis("Vertical") < 0)  // Moving downward
            {
                spriteRenderer.sprite = rightDown;
            }
            else if (Input.GetAxis("Vertical") > 0)  // Moving upward
            {
                spriteRenderer.sprite = rightUp;
            }
            else    // Steady in vertical axis, moving right only
            {
                spriteRenderer.sprite = rightGoing;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)    // Moving leftward
        {
            facingRight = false;

            if (Input.GetAxis("Vertical") < 0)  // Moving downward
            {
                spriteRenderer.sprite = leftDown;
            }
            else if (Input.GetAxis("Vertical") > 0)  // Moving upward
            {
                spriteRenderer.sprite = leftUp;
            }
            else    // Steady in vertical axis, moving left only
            {
                spriteRenderer.sprite = leftGoing;
            }
        }
        else if (facingRight)    // Steady in horizontal axis, stored direction says we're facing right
        {
            if (Input.GetAxis("Vertical") < 0)  // Moving downward
            {
                spriteRenderer.sprite = rightDown;
            }
            else if (Input.GetAxis("Vertical") > 0)  // Moving upward
            {
                spriteRenderer.sprite = rightUp;
            }
        }
        else    // Steady in horizontal axis, stored direction says we're facing left
        {
            if (Input.GetAxis("Vertical") < 0)  // Moving downward
            {
                spriteRenderer.sprite = leftDown;
            }
            else if (Input.GetAxis("Vertical") > 0)  // Moving upward
            {
                spriteRenderer.sprite = leftUp;
            }
        }
    }
}
