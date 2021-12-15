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

    private void Awake()
    {
        menu = GetComponentInParent<UICharacterSelectionMenu>();
        markerImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (player.HasController == false)
            return;

        if (!initializing)
            StartCoroutine(Initialize());
        
        if (!initalized)
            return;
        
        // Check for player controls and selection + locking character
        if (player.Controller.horizontal > 0.5)
        {
            MoveToCharacterPanel(menu.RightPanel);
        }
        else if (player.Controller.horizontal < -0.5)
        {
            MoveToCharacterPanel(menu.LeftPanel);
        }
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