using UnityEngine;

public class UICharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private Character characterPrefab;

    public Character CharacterPrefab { get { return characterPrefab; } }
}