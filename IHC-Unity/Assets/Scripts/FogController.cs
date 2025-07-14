using UnityEngine;

public class FogController : MonoBehaviour
{
    [Header("Configurações da Névoa")]
    public float initialFogDensity = 0.15f;
    public float densityAfterItem1 = 0.07f;
    public float densityAfterItem2 = 0.035f;
    public float densityAfterItem3 = 0f;
    public float transitionDuration = 1.0f;

    private int itemsCollected = 0;
    private const int totalItemsToCollect = 3;

    private float currentFogDensity;
    private float targetFogDensity;
    private float transitionTimer = 0f;
    private bool isTransitioning = false;

    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = initialFogDensity;
        currentFogDensity = initialFogDensity;
        targetFogDensity = initialFogDensity;
    }

    void Update()
    {
        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(transitionTimer / transitionDuration);
            RenderSettings.fogDensity = Mathf.Lerp(currentFogDensity, targetFogDensity, progress);

            if (progress >= 1f)
            {
                isTransitioning = false;
                RenderSettings.fogDensity = targetFogDensity;
                currentFogDensity = targetFogDensity;
            }
        }
    }

    public void ItemColetado()
    {
        itemsCollected++;

        if (itemsCollected <= totalItemsToCollect)
        {
            switch (itemsCollected)
            {
                case 1:
                    targetFogDensity = densityAfterItem1;
                    break;
                case 2:
                    targetFogDensity = densityAfterItem2;
                    break;
                case 3:
                    targetFogDensity = densityAfterItem3;
                    break;
                default:
                    targetFogDensity = densityAfterItem3;
                    break;
            }
            currentFogDensity = RenderSettings.fogDensity;
            transitionTimer = 0f;
            isTransitioning = true;
        }
    }
}