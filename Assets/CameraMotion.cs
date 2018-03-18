using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        oldPosition = this.transform.position;
	}

    private Vector3 oldPosition;

	// Update is called once per frame
	void Update () {
        CheckIfCameraMoved();
	}

    private void CheckIfCameraMoved()
    {
        if (oldPosition != this.transform.position)
        {
            oldPosition = this.transform.position;

            HexComponent[] hexes = GameObject.FindObjectsOfType<HexComponent>();

            foreach (var hex in hexes)
            {
                hex.UpdatePosition();
            }
        }
    }
}
