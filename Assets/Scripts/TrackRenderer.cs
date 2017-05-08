using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class TrackRenderer : MonoBehaviour {

  public Transform tree1, tree2, tree3, tree4;
  public Transform building1, building2, building3, building4;
  public Transform butterfly;

  void Awake() {

    Track track = StartScreen.tracks [StartScreen.trackNumber];

    for (int i = 0; i < track.nodes.Length; i++) {
      Node node = track.nodes [i];
      GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Quad);
      plane.transform.position += node.position + new Vector3 (0, 5, 0);
      plane.transform.localScale = new Vector3 (node.width * 2, 10, 1);
      plane.transform.Rotate (0, -node.theta, 0);
      plane.GetComponent <MeshCollider> ().convex = true;
      plane.GetComponent <MeshCollider> ().isTrigger = true;
      Destroy (plane.GetComponent <MeshRenderer> ());
      CheckPointTrigger trig = plane.AddComponent <CheckPointTrigger> ();
      trig.gateid = i;

      /*
      GameObject po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
      po.transform.position = node.position;

      // */
    }

    switch (StartScreen.trackType) {
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

		 //Use in case of "fillBuilding":
		 // Vector3 dl = left-left1;
		 // float angle = (float)((Math.Atan((dl.z)/(dl.x))*180/Math.PI));

		  fillTree(left, left1);
		  fillTree(right, right1);
		  if (i%6 == 0) {Instantiate(butterfly);}
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

		  Destroy (lc.GetComponent <BoxCollider> ());
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

          Destroy (lc.GetComponent <BoxCollider> ());

          po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
          po.transform.position = n1.rightSide + new Vector3 (0, -0.5f, 0);
          po.GetComponent<Renderer> ().material.color = blue;

		  float angle = (float)((Math.Atan((dl.z)/(dl.x))*180/Math.PI));

		  //Copying the right/left sides of n1/2 multiplying distance from central node by a factor of 1.4 so that the objects framing the track are offest away from the track and do not intrude onto it to prevent interference with cars on the track.
		  Vector3 newL1 = new Vector3 ((float)(n1.position.x - 1.4f*n1.width * Math.Cos(Math.PI*n1.theta/180)), 0 , (float)(n1.position.z - 1.4f*n1.width * Math.Sin(Math.PI*n1.theta/180)));

		  Vector3 newL2 = new Vector3 ((float)(n2.position.x - 1.4f*n2.width * Math.Cos(Math.PI*n2.theta/180)), 0 , (float)(n2.position.z - 1.4f*n2.width * Math.Sin(Math.PI*n2.theta/180)));

		  Vector3 newR1 = new Vector3 ((float)(n1.position.x + 1.4f*n1.width * Math.Cos(Math.PI*n1.theta/180)), 0 , (float)(n1.position.z + 1.4f*n1.width * Math.Sin(Math.PI*n1.theta/180)));

		  Vector3 newR2 = new Vector3 ((float)(n2.position.x + 1.4f*n2.width * Math.Cos(Math.PI*n2.theta/180)), 0 , (float)(n2.position.z + 1.4f*n2.width * Math.Sin(Math.PI*n2.theta/180)));

		  fillAny(newL1,newL2,angle);
		  fillAny(newR1, newR2, angle);

		  if (x%6 == 0) {Instantiate(butterfly);}

        }
        break;
      case 2:
        Color white = new Color (0.6f, 1, 0.6f);
        for (int x = 0; x < track.nodes.Length; x++) {
          Node n1 = track.nodes [x];
          Node n2 = track.nodes [(x + 1) % track.nodes.Length];
          Vector3 dl = n2.leftSide - n1.leftSide;
          GameObject lc = GameObject.CreatePrimitive (PrimitiveType.Cube);
          lc.transform.localScale += new Vector3 (dl.magnitude - 1, 0, 0);
          lc.transform.position = n1.leftSide + (dl / 2.0f);
          lc.transform.LookAt (n2.leftSide);
          lc.transform.Rotate (0, 90, 0);
          lc.transform.position += new Vector3 (0, -0.499f, 0);
          lc.GetComponent<Renderer> ().material.color = white;

          Destroy (lc.GetComponent <BoxCollider> ());

          GameObject po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
          po.transform.position = n1.leftSide + new Vector3 (0, -0.5f, 0);
          po.GetComponent<Renderer> ().material.color = white;

          po.transform.localScale += new Vector3 (0, 5, 0);

          Vector3 dr = n2.rightSide - n1.rightSide;
          lc = GameObject.CreatePrimitive (PrimitiveType.Cube);
          lc.transform.localScale += new Vector3 (dr.magnitude - 1, 0, 0);
          lc.transform.position = n1.rightSide + (dr / 2.0f);
          lc.transform.LookAt (n2.rightSide);
          lc.transform.Rotate (0, 90, 0);
          lc.transform.position += new Vector3 (0, -0.499f, 0);
          lc.GetComponent<Renderer> ().material.color = white;


          Destroy (lc.GetComponent <BoxCollider> ());

          po = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
          po.transform.position = n1.rightSide + new Vector3 (0, -0.5f, 0);
          po.GetComponent<Renderer> ().material.color = white;

          po.transform.localScale += new Vector3 (0, 5, 0);

        }
        break;
    }
  }

  private void fillTree(Vector3 a, Vector3 b) {
	Vector3 c = (a+b)/2;
    if ((a-b).magnitude>8) {
	    putTree(c);
		fillTree(a,c); fillTree(c,b);
	}
  }

  private void fillBuilding(Vector3 a, Vector3 b, float angle) {
	Vector3 c = (a+b)/2;
    if ((a-b).magnitude>8) {
		putBuilding(c, angle);
		fillBuilding(a,c, angle); fillBuilding(c,b, angle);
	}
  }

  private void fillAny(Vector3 a, Vector3 b, float angle) {
	Vector3 c = (a+b)/2;
    if ((a-b).magnitude>8) {
		putAny(c, angle);
		fillAny(a,c, angle); fillAny(c,b, angle);
	}
  }

  private void putTree(Vector3 c) {
	int ttype = UnityEngine.Random.Range(1,5);
	switch (ttype) {
		 case 1: Instantiate(tree1, c, Quaternion.identity); break;
		 case 2: Instantiate(tree2, c, Quaternion.identity); break;
		 case 3: Instantiate(tree3, c, Quaternion.identity); break;
		 case 4: Instantiate(tree4, c, Quaternion.identity); break;
	}
  }

  private void putBuilding(Vector3 c, float angle) {
	int btype = UnityEngine.Random.Range(1,5);
	switch (btype) {
		 case 1: Instantiate(building1, c, Quaternion.Euler(0,-angle,0)); break;
		 case 2: Instantiate(building2, c, Quaternion.Euler(0,-angle,0)); break;
		 case 3: Instantiate(building3, c, Quaternion.Euler(0,-angle,0)); break;
		 case 4: Instantiate(building4, c, Quaternion.Euler(0,-angle,0)); break;
	}
  }

  private void putAny(Vector3 c, float angle) {
	int bt = UnityEngine.Random.Range(1,3);
	switch (bt) {
		case 1: putTree(c); break;
		case 2: putBuilding(c, angle); break;
	}
  }

}
