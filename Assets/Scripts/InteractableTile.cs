using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum EInteractionType
{
    PlayingGame,

}

[CreateAssetMenu(fileName = "New Interactable Tile", menuName = "Tiles/InteractableTile")]
public class InteractableTile : Tile
{
    public EInteractionType InteractionType;

    public void Interact()
    {
        switch (InteractionType)
        {
            case EInteractionType.PlayingGame:
                SceneManager.LoadScene("MiniGameScene");
                break;
            default:
                break;
        }
    }
    
}
