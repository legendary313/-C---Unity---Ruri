using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;
	private void Start()
	{
		dialogue = new Dialogue();
		dialogue.sentences = new string[13];
		dialogue.sentences[0] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''Oh young lady, it was thousands of hardness to arrive here. It has been a long time since a species came to visit me. Do you have any wishes ?''";
		dialogue.sentences[1] = "\t<b><color=#0DE2FF>Ruri:</color></b> \n\t|''I'm coming from the Shiawase village in the other side of Vestronia. I have a sister. Her name is Akiko. ";
		dialogue.sentences[2] = "\t<b><color=#0DE2FF>Ruri:</color></b> \n\t|''She has been caused by a mysterious disease. I come here to beg you for the remedy.''";
		dialogue.sentences[3] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''I am Ivern, the mighty guard of this giant Fushigi forrest. ";
		dialogue.sentences[4] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''In the past, there was a string of lives flowing throughout the forrest and six goddess pillows,...''";
		dialogue.sentences[5] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''...who guard the mother forrest from the flow of time, reserve the peace and longevity of this land.''";
		dialogue.sentences[6] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''But now, there’s only me standing still here, dedicating the rest of intrival power to protect the forrest.''";
		dialogue.sentences[7] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''Losing the spring and being eroded by the time, I hardly have any power to save lives of species anymore.'' ";
		dialogue.sentences[8] = "\t<b><color=#0DE2FF>Ruri:</color></b> \n\t|''Can I do something to help you out?";
		dialogue.sentences[9] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''The string of lives orignates from the Western poppy of Gaia, on the other side of Bami remnants.";
		dialogue.sentences[10] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''If you find out the spring and wake up the pillow of god lying across the forrest, I could gain the last power I have lost.''";
		dialogue.sentences[11] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''My young lady, with your vigorous belief, I give you Ki, the fairytail of hope, it will support you.''";
		dialogue.sentences[12] = "\t<b><color=#00FF00>Ivern:</color></b> \n\t|''You can also find other ones across the forrest and request them for their help.''";
	}

	private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag.Equals("Player"))
		{	
			FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		}
	}

}
