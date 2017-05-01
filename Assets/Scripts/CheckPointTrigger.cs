using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CheckPointTrigger : MonoBehaviour {
  public int gateid;

  void OnTriggerEnter(Collider other) {
    if(other.gameObject.CompareTag ("CarBody")) {
      RaceEnvironment.instance.callback (other.gameObject.GetComponent <RacerData> ().id, gateid);
    }
  }
}
