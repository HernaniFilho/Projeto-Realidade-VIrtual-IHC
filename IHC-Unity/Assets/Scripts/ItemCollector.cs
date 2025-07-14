using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [Header("Referências da UI")]
    [Tooltip("O Painel (GameObject) na UI que contém a imagem e o texto do Haiku.")]
    public GameObject itemDisplayPanel; 

    [Tooltip("O componente Image na UI que exibirá a imagem do item.")]
    public Image itemDisplayImage; 

    [Tooltip("O componente TextMeshPro que exibirá o Haiku.")]
    public TMP_Text haikuTextDisplay; 

    [Tooltip("Tempo em segundos que o item e o Haiku ficarão visíveis.")]
    public float displayDuration = 3f; 

    private FogController fogController;

    void Start()
    {
        fogController = GetComponent<FogController>();
        
        if (fogController == null)
        {
            // Error handling (keeping this for critical dependency)
        }

        if (itemDisplayPanel == null || itemDisplayImage == null || haikuTextDisplay == null)
        {
            // Error handling (keeping this for critical dependency)
        }
        else
        {
            itemDisplayPanel.SetActive(false);
            itemDisplayImage.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CollectibleItemData collectedItemData = other.GetComponent<CollectibleItemData>();

        if (other.CompareTag("Coletavel") && collectedItemData != null)
        {
            if (fogController != null)
            {
                fogController.ItemColetado();
            }

            if (itemDisplayPanel != null && itemDisplayImage != null && haikuTextDisplay != null)
            {
                itemDisplayImage.sprite = collectedItemData.itemImageForDisplay;
                itemDisplayImage.enabled = true;
                
                haikuTextDisplay.text = collectedItemData.haikuTextForDisplay;

                itemDisplayPanel.SetActive(true);
                Invoke("HideItemDisplay", displayDuration); 
            }
            
            Destroy(other.gameObject); 
        }
    }

    void HideItemDisplay()
    {
        if (itemDisplayPanel != null)
        {
            itemDisplayPanel.SetActive(false);
            itemDisplayImage.enabled = false;
        }
    }
}