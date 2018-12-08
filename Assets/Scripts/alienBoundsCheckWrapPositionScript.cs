using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienBoundsCheckWrapPositionScript : MonoBehaviour {
   private float camHalfHeight;
   private float camHalfWidth;

   void Start() {
      camHalfHeight = Camera.main.orthographicSize;
      camHalfWidth = Camera.main.aspect * camHalfHeight;
   }

   void Update() {
      Vector3 alienPosition = transform.position;

      if(alienPosition.x < -camHalfWidth)
      {
         alienPosition.x = camHalfWidth - (alienPosition.x % camHalfWidth);
      }
      else if(alienPosition.x > camHalfWidth)
      {
         alienPosition.x = -camHalfWidth + (alienPosition.x % camHalfWidth);
      }

      if(alienPosition.y < -camHalfHeight)
      {
         alienPosition.y = -camHalfHeight;
      }
      transform.position = alienPosition;
   }

}
