using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void MovementFinished ();

public class PlayerMovement : MonoBehaviour
{
    
	public float movementSpeed;
	public Vector3 offset;
	public int currentTile;

	public int CurrentTile {
		get { return currentTile; }
	}

	private bool moving;
	private GameMap gameMap;
	private Animator animator;
	private MovementFinished callback;
	private Vector3 initialScaleVector;

	void Start ()
	{
		initialScaleVector = transform.localScale;
		gameMap = GameMap.getInstance ();
		animator = GetComponent<Animator> ();
		currentTile = 0;
		transform.position = GetPositionForTile (currentTile);
		Messenger.Broadcast<int> (MessengerIDs.PLAYER_MOVED_TO, currentTile);

	}

	public void Move (int tiles, MovementFinished callback)
	{
		if (moving)
			return;
		this.callback = callback;
		moving = true;
		Messenger.Broadcast<int> (MessengerIDs.PLAYER_MOVED_FROM, currentTile);
		animator.SetBool ("running", true);
		MoveTo (GetTile (tiles), 0);
	}

	private Vector3 GetPositionForTile (int tile)
	{
		return new Vector3 (gameMap.Tiles [tile].transform.position.x + offset.x, transform.position.y, transform.position.z);
	}

	private int GetTile (int moves)
	{
		int next = currentTile + moves;
		next = Mathf.Clamp (next, 0, gameMap.Tiles.Count - 1);
		return next;
	}

	IEnumerator Clown (int moves)
	{
		animator.SetBool ("running", false);
		animator.SetBool ("skating", false);
		yield return new WaitForSeconds (.5f);
		animator.SetBool ("running", true);
		MoveTo (GetTile (moves), 0);
	}
    
	private void MoveTo (int to, float delay)
	{
		Vector3 currentScale = initialScaleVector;
		currentScale.Scale (new Vector3 (Mathf.Sign (to - currentTile), 1, 1));
		transform.localScale = currentScale;
		currentTile = to;
		iTween.MoveTo (gameObject, iTween.Hash ("position", GetPositionForTile (to), "speed", movementSpeed, "delay", delay, "easetype", iTween.EaseType.linear, "oncomplete", "MoveComplete"));
	}

	private void MoveComplete ()
	{
		Tile tile = gameMap.Tiles [currentTile];
		TileAction currentTileAction = tile.GetComponent<TileAction> ();
		if (currentTileAction != null) {
			int moves = currentTileAction.TileEnter ();
			if (moves > 0) {
				animator.SetBool ("running", false);
				animator.SetBool ("skating", true);
				MoveTo (GetTile (moves), 0);
			} else {
				StartCoroutine (Clown(moves));
			}

		} else {
			transform.localScale = initialScaleVector;
			animator.SetBool ("running", false);
			animator.SetBool ("skating", false);
			moving = false;
			Messenger.Broadcast<int> (MessengerIDs.PLAYER_MOVED_TO, currentTile);
			if (callback != null) {
				callback ();
			}
		}
	}

}
