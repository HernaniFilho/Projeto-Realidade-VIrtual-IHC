using UnityEngine;

public class CollectibleItemData : MonoBehaviour
{
    [Header("Dados deste Item Coletável")]
    [Tooltip("A imagem (Sprite) deste item que será mostrada na UI.")]
    public Sprite itemImageForDisplay;

    [Tooltip("O Haiku específico para este item.")]
    [TextArea(3, 10)]
    public string haikuTextForDisplay;
}