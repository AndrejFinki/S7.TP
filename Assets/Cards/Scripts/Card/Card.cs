using UnityEngine;
using System.Collections;

public delegate void CardPlayed (Card card, bool successful);

public abstract class Card : MonoBehaviour {

	public string type;

	private MoveAction mAction;
	public MoveAction action {
		get {
			if (mAction == null) {
				mAction = GetComponent<MoveAction> ();
			}
			return mAction;
		}
	}

	private IfCondition[] mConditions;
	public IfCondition[] conditions {
		get {
			if (mConditions == null || mConditions.Length == 0) {
				mConditions = GetComponents<IfCondition> ();
			}
			return mConditions;
		}
	}
	
	private CardPlayed callback;

	public abstract int GetMovesIf ();
	public abstract int GetMovesElse ();
	public abstract bool ConditionMet ();

	public int GetMoves () {

		if (ConditionMet ()) {
			return GetMovesIf ();
		} else {
			return GetMovesElse ();
		}

	}

	public void Play (CardPlayed callback, BasePlayerControll playerControll)	{

		this.callback = callback;
		action.PlayAction(ActionCompleted, GetMoves (), playerControll);

	}

	public int ActionAiScore () {

		return action.ActionAiScore (GetMoves ());

	}

	public Tile ActionAiTile () {

		return action.ActionAiTile (GetMoves ());

	}
	
	private void ActionCompleted (bool successful) {

		if (callback != null) {
			callback (this, successful);
		}

	}

}
