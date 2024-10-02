using UnityEngine;
using System.Collections;

public class CardAnd : Card {

	public int movesIf;
	public int movesElse;

	public override int GetMovesIf ()	{
		return movesIf;
	}

	public override int GetMovesElse ()	{
		return movesElse;
	}
	
	public override bool ConditionMet () {
		bool can = true;
		for (int i = 0; i < conditions.Length; i++) {
			can = can && conditions[i].check ();
		}
		return can;
	}

}
