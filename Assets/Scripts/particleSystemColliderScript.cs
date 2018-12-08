using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemColliderScript : MonoBehaviour {
   public void setPosition(float x, float y) {
      Vector3 tmpPosition = transform.position;

      tmpPosition.x = x;
      tmpPosition.y = y;
      transform.position = tmpPosition;
   }
}
