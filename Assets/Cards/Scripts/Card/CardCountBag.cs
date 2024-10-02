
public class CardCountBag : Card {
	

	private int CountMoves () {
		PlayerQueue playerQueue = PlayerQueue.getInstance ();
		if (playerQueue == null)
			return 0;
		return playerQueue.GetCurrentPlayer ().bin;
	}

	public override int GetMovesIf ()	{
		return CountMoves ();
	}

	public override int GetMovesElse ()	{
		return CountMoves ();
	}

	public override bool ConditionMet () {
		return true;
	}

}
