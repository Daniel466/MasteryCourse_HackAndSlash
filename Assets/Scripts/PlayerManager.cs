using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player[] players;

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
    }

    internal void AddPlayerToGame(Controller controller)
    {
        var firstNonActivePlayer = players
            .OrderBy(t => t.PlayerNumber)
            .FirstOrDefault(t => t.HasController == false);
       
        firstNonActivePlayer.InitializePlayer(controller);
    }
}