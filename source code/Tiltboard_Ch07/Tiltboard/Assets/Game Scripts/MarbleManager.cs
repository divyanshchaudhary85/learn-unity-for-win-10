﻿using UnityEngine;
using System.Collections;

public class MarbleManager : MonoBehaviour {
	
	public Transform[] location; // drop point locations
	Transform currentStartLocation; // this could vary depending on level
	Renderer theRenderer; // the Renderer component
	Rigidbody theRigidbody; // the Rigidbody component 
	internal bool repressInput = false; // allow player input at start-up
	public BoardManager boardManager; // the Board Group's BoardManager script

	// Use this for initialization
	void Start () {
		theRenderer = gameObject.GetComponent<Renderer>();
		theRigidbody = gameObject.GetComponent<Rigidbody>();
		currentStartLocation = location[0]; // assign the current start location
		SetToStart (); // move the object to the current start location
	}
	
	// Update is called once per frame
	void Update () {
		if(repressInput) Input.ResetInputAxes(); // blocks user input
	}

	public void SetToStart () { 
		// set the new position
		Vector3 tempPos = new Vector3(transform.position.x,transform.position.y,
			transform.position.z); 
		tempPos = currentStartLocation.position;
		transform.position = tempPos;
	}

	public void StartDelay (float pause) {
		theRenderer.enabled = false; // turn off rendering
		theRigidbody.isKinematic = true; // suspend physics calculations
		repressInput = true;
		boardManager.StartBoardReset(pause); // start the board reset, send the pause time
		StartCoroutine(DelayReset(pause)); 
	}

	IEnumerator DelayReset (float pause) {
		// pause before reset
		yield return new WaitForSeconds(pause); // this starts the delay
		// add some FX here
		// reset
		theRenderer.enabled = true; // turn on rendering
		theRigidbody.isKinematic = false; // restart physics calculations
		repressInput = false;
		boardManager.resetBoard = false; // toggle off the board's reset flag
		SetToStart (); // call the original relocation function
	}

	public void PortalJump (float pause, Transform destination) {
		theRenderer.enabled = false;
		theRigidbody.isKinematic = true;
		StartCoroutine(DelayPortalJump(pause, destination)); 
	}


	IEnumerator DelayPortalJump (float pause, Transform destination) {
		// pause before reset
		yield return new WaitForSeconds(pause); 
		// add some FX here
		// reset
		theRenderer.enabled = true; // turn on rendering
		theRigidbody.isKinematic = false; // restart physics calculations
		MoveToLocation (destination);
	}

	public void MoveToLocation (Transform destination) { 
		// set the new position
		Vector3 tempPos = new Vector3(transform.position.x,transform.position.y, transform.position.z); 
		tempPos = destination.position;
		transform.position = tempPos;
	}

}