
public class CardCount : Card {

	public int tileType;
	public int count;
	
	private int CountMoves () {
		TypeCounter counter = TypeCounter.getInstance ();
		if (counter == null)
			return 0;
		return count * counter.GetCountable (tileType);
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
