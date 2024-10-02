using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackLinePosition : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public Text tileType;

    private RectTransform rectTransform;
    private float lineWidth;
    private GameMap gameMap;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameMap = GameMap.getInstance();

        lineWidth = transform.parent.GetComponent<RectTransform>().rect.width;
        lineWidth -= rectTransform.rect.width;
    }

    void LateUpdate()
    {
        float mapWidth = gameMap.Tiles[gameMap.Tiles.Count - 1].transform.position.x - gameMap.Tiles[0].transform.position.x;
        float playerPosition = (playerMovement.transform.position - playerMovement.offset - gameMap.Tiles[0].transform.position).x;
        float trackPosition = playerPosition / mapWidth * lineWidth;
        rectTransform.anchoredPosition = new Vector2(trackPosition, rectTransform.anchoredPosition.y);
        tileType.text = gameMap.Tiles[playerMovement.CurrentTile].tileNumber.ToString();
    }
}
