using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackRenderer : MonoBehaviour {

  void Awake() {

    Track track = MainMenu.tracks [MainMenu.trackNumber];

    for (int i = 0; i < track.nodes.Length; i++) {
      Node node = track.nodes [i];
      GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Quad);
      plane.transform.position += node.position + new Vector3 (0, 5, 0);
      plane.transform.localScale = new Vector3 (node.width * 2, 10, 1);
      plane.transform.Rotate (0, -node.theta, 0);
      plane.GetComponent <MeshCollider> ().convex = true;
      plane.GetComponent <MeshCollider> ().isTrigger = true;
      //Destroy (plane.GetComponent <MeshRenderer> ());
      CheckPointTrigger trig = plane.AddComponent <CheckPointTrigger> ();
      trig.gateid = i;

      /*
      GameObject po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
      po.transform.position = node.position;

      // */
    }

    switch (MainMenu.trackType) {
      case 0:
        for (int i = 0; i < track.nodes.Length; i += 1) {
          Vector3 left = track.nodes [i].leftSide;
          Vector3 right = track.nodes [i].rightSide;
          Vector3 left1 = track.nodes [(i + 1) % (track.nodes.Length)].leftSide;
          Vector3 right1 = track.nodes [(i + 1) % (track.nodes.Length)].rightSide;
          float lheight = left.z - left1.z;
          float lwidth = left.x - left1.x;
          float rheight = right.z - right1.z;
          float rwidth = right.x - right1.x;

          float ldiff = lwidth == 0 ? 0 : lheight / lwidth;
          float rdiff = rwidth == 0 ? 0 : rheight / rwidth;
          float ldiffrev = lheight == 0 ? 0 : lwidth / lheight;
          float rdiffrev = rheight == 0 ? 0 : rwidth / rheight;

          Color red = new Color (1, 0, 0);

          for (float h = 0; h < 3; h += 1) {
            for (float j = left.x; j <= left1.x; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 (j, h, (left.z + ((j - left.x) * ldiff)));
              cube.tag = "leftside";
              // TODO find smallest then return?
            }
            for (float j = left1.x; j <= left.x; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 (j, h, (left1.z + ((j - left1.x) * ldiff)));
              cube.tag = "leftside";
            }
            for (float j = left.z; j <= left1.z; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 ((left.x + ((j - left.z) * ldiffrev)), h, (float)j);
              cube.tag = "leftside";
            }
            for (float j = left1.z; j <= left.z; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 ((left1.x + ((j - left1.z) * ldiffrev)), h, (float)j);
              cube.tag = "leftside";
            }
            for (float j = right.x; j <= right1.x; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 (j, h, (float)(right.z + ((j - right.x) * rdiff)));
              cube.tag = "rightside";
            }
            for (float j = right1.x; j <= right.x; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 (j, h, (float)(right1.z + ((j - right1.x) * rdiff)));
              cube.tag = "rightside";
            }
            for (float j = right.z; j <= right1.z; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 ((right.x + ((j - right.z) * rdiffrev)), h, (float)j);
              cube.tag = "rightside";
            }
            for (float j = right1.z; j <= right.z; j += 1) {
              GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
              cube.GetComponent<Renderer> ().material.color = red;
              cube.transform.position = new Vector3 ((right1.x + ((j - right1.z) * rdiffrev)), h, (float)j);
              cube.tag = "rightside";
            }
          }
        }
        break;
      case 1:

        Color blue = new Color (0, 0, 1);
        for (int x = 0; x < track.nodes.Length; x++) {
          Node n1 = track.nodes [x];
          Node n2 = track.nodes [(x + 1) % track.nodes.Length];
          Vector3 dl = n2.leftSide - n1.leftSide;
          GameObject lc = GameObject.CreatePrimitive (PrimitiveType.Cube);
          lc.transform.localScale += new Vector3 (dl.magnitude - 1, 0, 0);
          lc.transform.position = n1.leftSide + (dl / 2.0f);
          lc.transform.LookAt (n2.leftSide);
          lc.transform.Rotate (0, 90, 0);
          lc.GetComponent<Renderer> ().material.color = blue;

          GameObject po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
          po.transform.position = n1.leftSide + new Vector3 (0, -0.5f, 0);
          po.GetComponent<Renderer> ().material.color = blue;

          Vector3 dr = n2.rightSide - n1.rightSide;
          lc = GameObject.CreatePrimitive (PrimitiveType.Cube);
          lc.transform.localScale += new Vector3 (dr.magnitude - 1, 0, 0);
          lc.transform.position = n1.rightSide + (dr / 2.0f);
          lc.transform.LookAt (n2.rightSide);
          lc.transform.Rotate (0, 90, 0);
          lc.GetComponent<Renderer> ().material.color = blue;

          po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
          po.transform.position = n1.rightSide + new Vector3 (0, -0.5f, 0);
          po.GetComponent<Renderer> ().material.color = blue;
        }

        break;
    }
  }
}
