using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienSpawnAndMoveScript : MonoBehaviour {
   public GameObject enemy;   // Alien prefab for spawning
   public float spawnDelay = 3f;
   public float moveDelay = 0.05f;

   private GameObject player;
   private float camHalfHeight;
   private float camHalfWidth;

   void SpwanAlien()
   {
      if(player)
      {
         float alienX = Random.Range(-camHalfWidth, camHalfWidth);
         float alienY = camHalfHeight + 0.2f;

         Vector2 alienPosition = new Vector2(alienX, alienY);

         Instantiate (enemy, alienPosition, Quaternion.identity);
      }
   }

   void moveAliens()
   {
      if(player)
      {
         foreach(GameObject currentAlien in GameObject.FindGameObjectsWithTag("Enemy"))
         {
            Vector2 aimDirection = (Vector2) player.transform.position - (Vector2) currentAlien.transform.position;
            aimDirection.Normalize();
            Rigidbody2D alienRigidBody = currentAlien.GetComponent<Rigidbody2D>();
            Vector3 currentVelocity = alienRigidBody.velocity;
            Vector2 currentVelocity2D = new Vector2(currentVelocity.x, currentVelocity.y);
            Vector2 anglingVector;
            float angleOffset;
            float randomThrust = Random.Range(2f, 8f);

            // If we're moving more than 90 degrees off the target vector, damp movement before applying new force
            anglingVector = currentVelocity2D - aimDirection;
            angleOffset = Mathf.Atan2(anglingVector.x, anglingVector.y) * Mathf.Rad2Deg;;

            // Debug.Log(angleOffset);

            if(angleOffset < -135 || angleOffset > 135)
            {
               // alienRigidBody.AddForce(-currentVelocity2D * 4, ForceMode2D.Force);
               alienRigidBody.velocity = new Vector3(0, 0, 0);
            }

            // Damp max acceleration
            if(currentVelocity.x > 8)
            {
               currentVelocity.x = 8;
            }
            if(currentVelocity.x < -8)
            {
               currentVelocity.x = -8;
            }
            if(currentVelocity.y > 8)
            {
               currentVelocity.y = 8;
            }
            if(currentVelocity.y < -8)
            {
               currentVelocity.y = -8;
            }

            Vector3 alienPosition = new Vector3(currentAlien.transform.position.x, currentAlien.transform.position.y, -9);
            currentAlien.transform.position = alienPosition;
            alienRigidBody.AddForce(aimDirection * randomThrust, ForceMode2D.Force);
         }
      }
   }

   void Update()
   {
      if(!player)
      {
         player = GameObject.Find("PlayerSprite(Clone)");
      }
   }

   void Start ()
   {
      camHalfHeight = Camera.main.orthographicSize;
      camHalfWidth = Camera.main.aspect * camHalfHeight;
      player = GameObject.Find("PlayerSprite(Clone)");

      InvokeRepeating ("SpwanAlien", spawnDelay, spawnDelay);
      InvokeRepeating ("moveAliens", moveDelay, moveDelay);
   }
}
