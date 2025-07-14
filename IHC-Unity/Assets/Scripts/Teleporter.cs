using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Teleporter : MonoBehaviour
{
    [Header("Configurações do Teletransporte")]
    [Tooltip("O nome da cena para a qual o jogador será teletransportado.")]
    public string sceneToLoad;

    [Tooltip("Tag do objeto que pode ser teletransportado (geralmente 'Player').")]
    public string playerTag = "Player";

    [Header("Configurações do Haicai Final")]
    [Tooltip("O Painel de UI que será ativado para exibir o Haicai final.")]
    public GameObject finalHaikuPanel; 

    [Tooltip("O componente TextMeshPro que exibirá o Haicai final.")]
    public TMP_Text finalHaikuTextDisplay; 

    [Tooltip("O Haicai que será exibido antes do teletransporte.")]
    [TextArea(3, 10)]
    public string finalHaikuMessage;

    [Tooltip("Tempo em segundos que o Haicai final ficará visível antes do teletransporte.")]
    public float haikuDisplayDuration = 3f;

    private bool teleporting = false;

    void Start()
    {
        if (finalHaikuPanel != null)
        {
            finalHaikuPanel.SetActive(false);
        }

        if (finalHaikuPanel == null || finalHaikuTextDisplay == null)
        {
            // Warning handling (keeping this for critical dependency)
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !teleporting)
        {
            teleporting = true;
            
            DisplayFinalHaiku();
        }
    }

    void DisplayFinalHaiku()
    {
        if (finalHaikuPanel != null && finalHaikuTextDisplay != null)
        {
            finalHaikuTextDisplay.text = finalHaikuMessage;
            finalHaikuPanel.SetActive(true);
            
            Invoke("LoadNewScene", haikuDisplayDuration);
        }
        else
        {
            LoadNewScene();
        }
    }

    void LoadNewScene()
    {
        if (finalHaikuPanel != null)
        {
            finalHaikuPanel.SetActive(false);
        }

        if (string.IsNullOrEmpty(sceneToLoad))
        {
            // Error handling (keeping this for critical dependency)
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}