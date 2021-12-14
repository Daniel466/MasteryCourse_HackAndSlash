using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    private Controller controller;
    private UIPlayerText uiPlayerText;

    public bool HasController { get { return controller != null; } }
    public int PlayerNumber { get { return playerNumber; } }

    private void Awake()
    {
        uiPlayerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitializePlayer(Controller controller)
    {
        this.controller = controller;

        gameObject.name = string.Format("Player {0} - {1}", playerNumber, controller.gameObject.name);

        uiPlayerText.HandlePlayerInitialized();
    }
}