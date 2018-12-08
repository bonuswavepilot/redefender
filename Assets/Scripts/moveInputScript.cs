using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveInputScript : MonoBehaviour {
   public float speed = 4f;
   public float bgspeed = 0.25f;
   public float fgmultiplier = 1.6f;
   public float alienSpeedMultiplier = 16f;
   public swapSpriteFacingScript facingScript;
   public Rigidbody2D LaserRight;
   public Rigidbody2D LaserLeft;
   public AudioClip laserSound;

   private GameObject quadBGGameObject;
   private GameObject quadFGGameObject;
   private GameObject gameAudioObject;

   // private renderer renderer;
   private Vector2 savedBGOffset;
   private Vector2 savedFGOffset;
   private Renderer quadBGRenderer;
   private Renderer quadFGRenderer;
   private float fgspeed;
   private AudioSource gameAudio;

   private float camHalfHeight;
   private float camHalfWidth;

	// Use this for initialization
	void Start () {
      fgspeed = bgspeed * fgmultiplier;
      quadBGGameObject = GameObject.Find("BackgroundQuad");
      quadFGGameObject = GameObject.Find("ForegroundQuad");
      gameAudioObject = GameObject.Find("GameAudio");

      camHalfHeight = Camera.main.orthographicSize;
      camHalfWidth = Camera.main.aspect * camHalfHeight;

      quadBGRenderer = quadBGGameObject.GetComponent<Renderer>();
      quadFGRenderer = quadFGGameObject.GetComponent<Renderer>();
      gameAudio = gameAudioObject.GetComponent<AudioSource>();

      savedBGOffset = quadBGRenderer.material.mainTextureOffset;
      savedFGOffset = quadFGRenderer.material.mainTextureOffset;
	}

	// Update is called once per frame
   void Update() {
      fgspeed = bgspeed * fgmultiplier;

      bool firing = Input.GetKeyDown(KeyCode.RightControl);
      float horizontalComponent;
      float verticalComponent;

      // float tempHorizontal;

      savedBGOffset = quadBGRenderer.material.mainTextureOffset;
      savedFGOffset = quadFGRenderer.material.mainTextureOffset;

      if(firing)
      {
         gameAudio.PlayOneShot(laserSound);

         if(facingScript.facingRight) {
            Vector3 shipPosition = transform.position;
            shipPosition.x += 0.5f;
            Rigidbody2D projectile = Instantiate(LaserRight, shipPosition, transform.rotation) as Rigidbody2D;
            projectile.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);
         }
         else
         {
            Vector3 shipPosition = transform.position;
            shipPosition.x += -0.5f;
            Rigidbody2D projectile = Instantiate(LaserLeft, shipPosition, transform.rotation) as Rigidbody2D;
            projectile.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
         }
      }

      // On attempting to move past horizontal bounds, clamp position, but scroll backgrounds
      if(Input.GetAxis("Horizontal") > 0 && transform.position.x > (camHalfWidth * 0.67))
      {
         horizontalComponent = 0;
         float foregroundOffset = Input.GetAxis("Horizontal") * Time.deltaTime * fgspeed;
         float backgroundOffset = Input.GetAxis("Horizontal") * Time.deltaTime * bgspeed;


         quadBGRenderer.material.mainTextureOffset = savedBGOffset += new Vector2(backgroundOffset, 0);
         quadFGRenderer.material.mainTextureOffset = savedFGOffset += new Vector2(foregroundOffset, 0);

         foreach(GameObject currentAlien in GameObject.FindGameObjectsWithTag("Enemy"))
         {
            Vector3 alienPosition = currentAlien.transform.position;
            alienPosition.x += -backgroundOffset * alienSpeedMultiplier;   //  += new Vector3(-backgroundOffset * alienSpeedMultiplier, 0, 0);

            // if(alienPosition.x < -camHalfWidth)
            // {
            //    alienPosition.x = camHalfWidth - (alienPosition.x % camHalfWidth);
            // }
            currentAlien.transform.position = alienPosition;

         }
      }
      else if(Input.GetAxis("Horizontal") < 0 && transform.position.x < -(camHalfWidth * 0.67) )
      {
         horizontalComponent = 0;
         float foregroundOffset = Input.GetAxis("Horizontal") * Time.deltaTime * fgspeed;
         float backgroundOffset = Input.GetAxis("Horizontal") * Time.deltaTime * bgspeed;

         quadBGRenderer.material.mainTextureOffset = savedBGOffset += new Vector2(backgroundOffset, 0);
         quadFGRenderer.material.mainTextureOffset = savedFGOffset += new Vector2(foregroundOffset, 0);

         foreach(GameObject currentAlien in GameObject.FindGameObjectsWithTag("Enemy"))
         {
            Vector3 alienPosition = currentAlien.transform.position;
            alienPosition.x += -backgroundOffset * alienSpeedMultiplier;   //  += new Vector3(-backgroundOffset * alienSpeedMultiplier, 0, 0);

            // if(alienPosition.x > camHalfWidth)
            // {
            //    alienPosition.x = -camHalfWidth + (alienPosition.x % camHalfWidth);
            // }
            currentAlien.transform.position = alienPosition;
         }
      }
      else
      {
         horizontalComponent = Input.GetAxis("Horizontal");
      }

      // On attempting to move past vertical bounds, halt vertical movement of player
      if( (Input.GetAxis("Vertical") > 0 && transform.position.y > (camHalfHeight - 0.1) ) ||
          (Input.GetAxis("Vertical") < 0 && transform.position.y < -(camHalfHeight - 0.2) ) )
      {
         verticalComponent = 0;
      }
      else
      {
         verticalComponent = Input.GetAxis("Vertical");
      }

      Vector3 move = new Vector3(horizontalComponent, verticalComponent, 0);
      transform.position += move * speed * Time.deltaTime;
   }
}
