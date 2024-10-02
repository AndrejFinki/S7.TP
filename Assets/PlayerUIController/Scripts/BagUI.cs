using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BagUI : MonoBehaviour {

	public PlayerState playerState;
    public Animator animator;	
	public Text text;

    private int bin;
	
	void Start () {
		
        bin = playerState.bin;
        text.text = playerState.bin.ToString ();

	}

    public void Change() {
        text.text = playerState.bin.ToString ();
    }
	
	void Update () {
		
        if (bin != playerState.bin) {
            bin = playerState.bin;
            animator.SetTrigger("Change");
        }
		
	}

}
