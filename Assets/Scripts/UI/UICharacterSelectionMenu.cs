using System;
using TMPro;
using UnityEngine;

public class UICharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] private UICharacterSelectionPanel leftPanel;
    [SerializeField] private UICharacterSelectionPanel rightPanel;

    [SerializeField] private TextMeshProUGUI startGameText;
    
    private UICharacterSelectionMarker[] makers;
    private bool startEnabled;

    public UICharacterSelectionPanel LeftPanel { get { return leftPanel; } }
    public UICharacterSelectionPanel RightPanel { get { return rightPanel; } }

    private void Awake()
    {
        makers = GetComponentsInChildren<UICharacterSelectionMarker>();
    }

    private void Update()
    {
        int playerCount = 0;
        int lockedCount = 0;

        foreach (var marker in makers)
        {
            if (marker.IsPlayerIn)
                playerCount++;

            if (marker.IsLockedIn)
                lockedCount++;
        }

        startEnabled = playerCount > 0 && playerCount == lockedCount;
        startGameText.gameObject.SetActive(startEnabled);
    }

    public void TryStartGame()
    {
        if (startEnabled)
        {
            GameManager.Instance.Begin();
            gameObject.SetActive(false);
        }
    }
}