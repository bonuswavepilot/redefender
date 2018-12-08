using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBoundCheckScript : MonoBehaviour {

   void Update() {
      float camHalfHeight = Camera.main.orthographicSize;
      float camHalfWidth = Camera.main.aspect * camHalfHeight;

      if(transform.position.x > camHalfWidth || transform.position.x < -camHalfWidth)
      {
         Destroy(gameObject);
      }
   }

}
