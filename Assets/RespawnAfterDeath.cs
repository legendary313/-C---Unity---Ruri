using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class RespawnAfterDeath : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        string path = Application.persistentDataPath + "/player.data";
        // if (File.Exists(path)){
        //     // animator.gameObject.GetComponent<PlayerController>().loadGameAfterDead();
        //     if (PlayerPrefs.GetInt("playingMap") == 1){
        //         SceneManager.LoadScene("Map1");
        //     }else if (PlayerPrefs.GetInt("playingMap") == 2){
        //         SceneManager.LoadScene("Map2");
        //     }
        // }else{
        //     SceneManager.LoadScene("");
        // }
        SceneManager.LoadScene("MainGame");
        // StartCoroutine();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
