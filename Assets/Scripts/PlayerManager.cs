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

	// Use this for initialization
	void Start () {
		FullBodyScript = FullBody.GetComponent<Player> ();
		HeadScript = Head.GetComponent<Player> ();
		TorsoScript = Torso.GetComponent<Player> ();
		LegsScript = Legs.GetComponent<Player> ();
		HeadTorsoScript = HeadTorso.GetComponent<Player> ();
		TorsoLegsScript = TorsoLegs.GetComponent<Player> ();

		FullBody.SetActive (false);
		Head.SetActive (false);
		Torso.SetActive (true);
		Legs.SetActive (true);
		HeadTorso.SetActive (false);
		TorsoLegs.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		if (TorsoScript.makingContactWithPart == Player.BrowPart.Legs) {
			TorsoScript.makingContactWithPart = Player.BrowPart.None;
			TorsoLegs.SetActive(true);
			TorsoLegs.transform.position = Legs.transform.position;
			Torso.SetActive(false);
			Legs.SetActive(false);
		}
	}

	void CombineParts (GameObject part1, GameObject part2) {

	}


}
