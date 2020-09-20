using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour {

	public Story dialogue;


	void Start()
	{
		FindObjectOfType<StoryManager>().StartDialogue(dialogue);
	}
}
