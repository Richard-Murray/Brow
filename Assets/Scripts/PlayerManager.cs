using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	[Header("Parts")]
	public GameObject FullBody;
	public GameObject Head;
	public GameObject Torso;
	public GameObject Legs;
	public GameObject HeadTorso;
	public GameObject TorsoLegs;

	Player FullBodyScript;
	Player HeadScript;
	Player TorsoScript;
	Player LegsScript;
	Player HeadTorsoScript;
	Player TorsoLegsScript;

	Player.BrowPart currentControlledPart = Player.BrowPart.Head; //Only switches between head, torso and legs
	Player.BrowPart previousControlledPart = Player.BrowPart.Head;
	float inputDelay = 0.4f;
	float timeSincePressed = 0;

	// Use this for initialization
	void Start () {
		FullBodyScript = FullBody.GetComponent<Player> ();
		HeadScript = Head.GetComponent<Player> ();
		TorsoScript = Torso.GetComponent<Player> ();
		LegsScript = Legs.GetComponent<Player> ();
		HeadTorsoScript = HeadTorso.GetComponent<Player> ();
		TorsoLegsScript = TorsoLegs.GetComponent<Player> ();

		FullBodyScript.Initialise ();
		HeadScript.Initialise ();
		TorsoScript.Initialise ();
		LegsScript.Initialise ();
		HeadTorsoScript.Initialise ();
		TorsoLegsScript.Initialise ();

		FullBody.SetActive (true);
		FullBodyScript.isInWorld = true;
		Head.SetActive (false);
		HeadScript.isInWorld = false;
		Torso.SetActive (false);
		TorsoScript.isInWorld = false;
		Legs.SetActive (false);
		LegsScript.isInWorld = false;
		HeadTorso.SetActive (false);
		HeadTorsoScript.isInWorld = false;
		TorsoLegs.SetActive (false);
		TorsoLegsScript.isInWorld = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (HeadScript.makingContactWithPart == Player.BrowPart.Torso) {
			CombineParts(Head, Torso);
		}
		if (TorsoScript.makingContactWithPart == Player.BrowPart.Legs) {
			CombineParts (Torso, Legs);
		}
		if (HeadTorsoScript.makingContactWithPart == Player.BrowPart.Legs) {
			CombineParts(HeadTorso, Legs);
		}
		if (HeadScript.makingContactWithPart == Player.BrowPart.TorsoLegs) {
			CombineParts(Head, TorsoLegs);
		}

		if(Input.GetKeyDown(KeyCode.R)){
			DebugResetParts();
		}

		if (Input.GetKeyDown (KeyCode.Alpha1) && timeSincePressed < inputDelay && previousControlledPart == Player.BrowPart.Head) {
			currentControlledPart = Player.BrowPart.Head;
			if(!HeadScript.isActiveAndEnabled)
			{
				if(FullBodyScript.isInWorld) {
					DetachParts(Head, FullBody);
				}
				if(HeadTorsoScript.isInWorld) {
					DetachParts(Head, HeadTorso);
				}
			}
			//DetachParts
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
			previousControlledPart = currentControlledPart;
			currentControlledPart = Player.BrowPart.Head;
			timeSincePressed = 0;
		}

		if (Input.GetKeyDown (KeyCode.Alpha2) && timeSincePressed < inputDelay && previousControlledPart == Player.BrowPart.Torso) {
			currentControlledPart = Player.BrowPart.Torso;
			if(!TorsoScript.isActiveAndEnabled)
			{
				if(FullBodyScript.isInWorld) {
					DetachParts(Torso, FullBody);
				}
				if(HeadTorsoScript.isInWorld) {
					DetachParts(Torso, HeadTorso);
				}
				if(TorsoLegsScript.isInWorld) {
					DetachParts(Torso, TorsoLegs);
				}
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			previousControlledPart = currentControlledPart;
			currentControlledPart = Player.BrowPart.Torso;
			timeSincePressed = 0;
		}

		if (Input.GetKeyDown (KeyCode.Alpha3) && timeSincePressed < inputDelay && previousControlledPart == Player.BrowPart.Legs) {
			currentControlledPart = Player.BrowPart.Legs;
			if(!LegsScript.isActiveAndEnabled)
			{
				if(FullBodyScript.isInWorld) {
					DetachParts(Legs, FullBody);
				}
				if(TorsoLegsScript.isInWorld) {
					DetachParts(Legs, TorsoLegs);
				}
			}
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			previousControlledPart = currentControlledPart;
			currentControlledPart = Player.BrowPart.Legs;
			timeSincePressed = 0;
		}

		timeSincePressed += Time.deltaTime;
		/*if (TorsoScript.makingContactWithPart == Player.BrowPart.Legs) {
			TorsoScript.makingContactWithPart = Player.BrowPart.None;
			TorsoLegs.SetActive(true);
			TorsoLegs.transform.position = Legs.transform.position;
			Torso.SetActive(false);
			Legs.SetActive(false);
		}*/


		SetActiveParts ();
	}

	void DebugResetParts(){
		FullBody.SetActive (true);
		FullBodyScript.isInWorld = true;
		Head.SetActive (false);
		HeadScript.isInWorld = false;
		Torso.SetActive (false);
		TorsoScript.isInWorld = false;
		Legs.SetActive (false);
		LegsScript.isInWorld = false;
		HeadTorso.SetActive (false);
		HeadTorsoScript.isInWorld = false;
		TorsoLegs.SetActive (false);
		TorsoLegsScript.isInWorld = false;

		FullBody.transform.position = new Vector3 (0, 2, 0);
	}

	void CombineParts (GameObject part1, GameObject part2) { //latter is the lower part
		Player part1Script = part1.GetComponent<Player> ();
		Player part2Script = part2.GetComponent<Player> ();
		part1Script.makingContactWithPart = Player.BrowPart.None;
		part2Script.makingContactWithPart = Player.BrowPart.None;

		
		if (part1Script.partID == Player.BrowPart.Head && part2Script.partID == Player.BrowPart.Torso) {
			HeadTorso.SetActive(true);
			HeadTorsoScript.isInWorld = true;
			HeadTorso.transform.position = part2.transform.position + HeadTorso.GetComponent<Player>().partOffset;
		}
		if (part1Script.partID == Player.BrowPart.Torso && part2Script.partID == Player.BrowPart.Legs) {
			TorsoLegs.SetActive(true);
			TorsoLegsScript.isInWorld = true;
			TorsoLegs.transform.position = part2.transform.position + TorsoLegs.GetComponent<Player>().partOffset;
		}
		if (part1Script.partID == Player.BrowPart.HeadTorso && part2Script.partID == Player.BrowPart.Legs) {
			FullBody.SetActive(true);
			FullBodyScript.isInWorld = true;
			FullBody.transform.position = part2.transform.position + FullBody.GetComponent<Player>().partOffset * 2;
		}
		if (part1Script.partID == Player.BrowPart.Head && part2Script.partID == Player.BrowPart.TorsoLegs) {
			FullBody.SetActive(true);
			FullBodyScript.isInWorld = true;
			FullBody.transform.position = part2.transform.position + FullBody.GetComponent<Player>().partOffset;
		}

		part1.SetActive (false);
		part1Script.isInWorld = false;
		part2.SetActive (false);
		part2Script.isInWorld = false;
	}

	//Detach object 1 from object2 (head from headtorso, NOT head from torso)
	void DetachParts(GameObject part1, GameObject part2){
		Player part1Script = part1.GetComponent<Player> ();
		Player part2Script = part2.GetComponent<Player> ();

		if (part1Script.partID == Player.BrowPart.Head) {
			if(part2Script.partID == Player.BrowPart.Fullbody)
			{
				TorsoLegs.SetActive(true);
				TorsoLegsScript.isInWorld = true;
				TorsoLegsScript.timeSinceDetached = 0;
				TorsoLegs.transform.position = FullBody.transform.position - TorsoLegsScript.partOffset;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = FullBody.transform.position + part1Script.partOffset * 2;
			}
			if(part2Script.partID == Player.BrowPart.HeadTorso)
			{
				Torso.SetActive(true);
				TorsoScript.isInWorld = true;
				TorsoScript.timeSinceDetached = 0;
				Torso.transform.position = HeadTorso.transform.position - TorsoScript.partOffset;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = HeadTorso.transform.position + part1Script.partOffset;
			}
		}

		if (part1Script.partID == Player.BrowPart.Torso) {
			if(part2Script.partID == Player.BrowPart.Fullbody)
			{
				Head.SetActive(true);
				HeadScript.isInWorld = true;
				HeadScript.timeSinceDetached = 0;
				Head.transform.position = FullBody.transform.position + TorsoLegsScript.partOffset * 2;
				Legs.SetActive(true);
				LegsScript.isInWorld = true;
				LegsScript.timeSinceDetached = 0;
				Legs.transform.position = FullBody.transform.position - TorsoLegsScript.partOffset * 2;
				//TEMPORARY
				//LegsScript.velocity = part2Script.velocity;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = FullBody.transform.position;
			}
			if(part2Script.partID == Player.BrowPart.HeadTorso)
			{
				Head.SetActive(true);
				HeadScript.isInWorld = true;
				HeadScript.timeSinceDetached = 0;
				Head.transform.position = HeadTorso.transform.position + TorsoScript.partOffset;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = HeadTorso.transform.position - part1Script.partOffset;
			}
			if(part2Script.partID == Player.BrowPart.TorsoLegs)
			{
				Legs.SetActive(true);
				LegsScript.isInWorld = true;
				LegsScript.timeSinceDetached = 0;
				Legs.transform.position = TorsoLegs.transform.position - TorsoScript.partOffset;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = TorsoLegs.transform.position + part1Script.partOffset;
			}

		}

		if (part1Script.partID == Player.BrowPart.Legs) {
			if(part2Script.partID == Player.BrowPart.Fullbody)
			{
				HeadTorso.SetActive(true);
				HeadTorsoScript.isInWorld = true;
				HeadTorsoScript.timeSinceDetached = 0;
				HeadTorso.transform.position = FullBody.transform.position + TorsoLegsScript.partOffset;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = FullBody.transform.position - part1Script.partOffset * 2;
			}
			if(part2Script.partID == Player.BrowPart.TorsoLegs)
			{
				Torso.SetActive(true);
				TorsoScript.isInWorld = true;
				TorsoScript.timeSinceDetached = 0;
				Torso.transform.position = TorsoLegs.transform.position + TorsoScript.partOffset;
				part1.SetActive (true);
				part1Script.isInWorld = true;
				part1Script.timeSinceDetached = 0;
				part1.transform.position = TorsoLegs.transform.position - part1Script.partOffset;
			}
		}

		//FullBodyScript.velocity = part2Script.velocity;
		//HeadScript.velocity = part2Script.velocity;
		//TorsoScript.velocity = part2Script.velocity;
		//LegsScript.velocity = part2Script.velocity;
		//HeadTorsoScript.velocity = part2Script.velocity;
		//TorsoLegsScript.velocity = part2Script.velocity;

		part1Script.velocity = part2Script.velocity;
		part2.SetActive (false);
		part2Script.isInWorld = false;

	}

	//This function tells all possible parts if their part type is selected, does NOT determine if the part is on screen or not
	void SetActiveParts()
	{
		FullBodyScript.isControlled = true;
		if (currentControlledPart == Player.BrowPart.Head) {
			HeadScript.isControlled = true;
			HeadTorsoScript.isControlled = true;
			TorsoScript.isControlled = false;
			LegsScript.isControlled = false;
			TorsoLegsScript.isControlled = false;
		}
		if (currentControlledPart == Player.BrowPart.Torso) {
			TorsoScript.isControlled = true;
			TorsoLegsScript.isControlled = true;
			HeadTorsoScript.isControlled = true;
			HeadScript.isControlled = false;
			LegsScript.isControlled = false;

		}
		if (currentControlledPart == Player.BrowPart.Legs) {
			LegsScript.isControlled = true;
			TorsoLegsScript.isControlled = true;
			HeadScript.isControlled = false;
			TorsoScript.isControlled = false;
			HeadTorsoScript.isControlled = false;
		}

	}

	public void DoubleJump(Player.BrowPart partID)
	{
		if (partID == Player.BrowPart.Fullbody) {
			//HeadTorsoScript.controller = HeadTorsoScript.transform.GetComponent<Controller2D>();
			DetachParts (Legs, FullBody);
			HeadTorsoScript.velocity.y = HeadTorsoScript.maxJumpVelocity;
			HeadTorsoScript.velocity.x = FullBodyScript.velocity.x;
			HeadTorsoScript.controller.collisions.below = false;
		} 
		if (partID == Player.BrowPart.TorsoLegs) {
			//temporary fix, torso controller is initialised in the start function which is not called until after
			//the maxjumpv is also not set
			//TorsoScript.controller = TorsoScript.transform.GetComponent<Controller2D>();
			DetachParts (Legs, TorsoLegs);
			TorsoScript.velocity.y = TorsoScript.maxJumpVelocity;
			TorsoScript.velocity.x = TorsoLegsScript.velocity.x;
			TorsoScript.controller.collisions.below = false;
		}
	}


}
