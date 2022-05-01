using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ThirstySpotUIUpdater : MonoBehaviour
{
    [SerializeField] private ThirstySpot spotToTrack;
    
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private Image waterTracker;
    [SerializeField] private Image growthTracker;
    [SerializeField] private Image healthTracker;
    [FormerlySerializedAs("UIRoot")] [SerializeField] private GameObject uiRoot;

    private void Start()
    {
        if (!spotToTrack && !TryGetComponent(out spotToTrack))
            return;

        spotToTrack.waterUpdated.AddListener(UpdateWater);
        spotToTrack.growthUpdated.AddListener(UpdateGrowth);
        spotToTrack.plantStatus.AddListener(UpdateStatus);
        spotToTrack.healthUpdated.AddListener(UpdateHealth);
        
        UpdateStatus(spotToTrack.PlantDefinition);
    }

    private void UpdateHealth(float newHealth)
    {
        healthTracker.fillAmount = newHealth / spotToTrack.MaxHealth;
    }

    private void UpdateStatus(PlantDefinition plant)
    {
        uiRoot.SetActive(plant != null);

        if (plant != null)
        {
            headerText.SetText(plant.plantName);
        }
    }

    private void UpdateGrowth(float updatedValue)
    {
        growthTracker.fillAmount = updatedValue / spotToTrack.PlantDefinition.timeToGrow;
    }

    private void UpdateWater(float updatedValue)
    {
        waterTracker.fillAmount = updatedValue / 100f;
    }

    private void OnDestroy()
    {
        spotToTrack.waterUpdated.RemoveListener(UpdateWater);
        spotToTrack.growthUpdated.RemoveListener(UpdateGrowth);
        spotToTrack.plantStatus.RemoveListener(UpdateStatus);
    }
}