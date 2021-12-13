using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    private Controller controller;
    
    public bool HasController { get { return controller != null; } }
    public int PlayerNumber { get { return  playerNumber; } }

    public void InitializePlayer(Controller controller)
    {
        this.controller = controller;

        gameObject.name = string.Format("Player {0} - {1}", playerNumber, controller.gameObject.name);
    }
}