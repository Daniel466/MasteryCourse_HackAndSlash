using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image markerImage;
    [SerializeField] private Image lockImage;
    
    private UICharacterSelectionMenu menu;
    private bool initializing;
    private bool initalized;
    
    public bool IsLockedIn { get; private set; }
    public bool IsPlayerIn { get { return player.HasController; } }

    private void Awake()
    {
        menu = GetComponentInParent<UICharacterSelectionMenu>();
        markerImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsPlayerIn == false)
            return;

        if (!initializing)
            StartCoroutine(Initialize());
        
        if (!initalized)
            return;
        
        // Check for player controls and selection + locking character
        if (!IsLockedIn)
        {
            if (player.Controller.horizontal > 0.5)
            {
                MoveToCharacterPanel(menu.RightPanel);
            }
            else if (player.Controller.horizontal < -0.5)
            {
                MoveToCharacterPanel(menu.LeftPanel);
            }

            if (player.Controller.attackPressed)
            {
                LockCharacter();
            }
        }
        else
        {
            if (player.Controller.attackPressed)
            {
                menu.TryStartGame();
            }
        }
    }

    private void LockCharacter()
    {
        IsLockedIn = true;
        lockImage.gameObject.SetActive(true);
        //markerImage.gameObject.SetActive(false);
    }
    
    private void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
    }

    private IEnumerator Initialize()
    {
        initializing = true;
        MoveToCharacterPanel(menu.LeftPanel);
        
        yield return new WaitForSeconds(0.5f);
        markerImage.gameObject.SetActive(true);
        initalized = true;
    }
}