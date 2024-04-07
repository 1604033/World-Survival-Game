using System;
using BuildingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Building_system.UI
{
    public class UI_Active_Item : MonoBehaviour
    {
        [SerializeField] private BuildingPlacer _buildingPlacer;

        [SerializeField]private Button closeButon;
        [SerializeField]private TextMeshProUGUI titleText;
        [SerializeField]private TextMeshProUGUI itemsText;
        [SerializeField]private GameObject panel;

        private void Awake()
        {
            if (closeButon != null)
            {
                closeButon.onClick.RemoveAllListeners();
                closeButon.onClick.AddListener(Hide);
            }

            _buildingPlacer.ActiveBuildableChanged += OnActiveBuildableChanged;
        }

        private void Hide()
        {
            _buildingPlacer.SetActiveBuildable(null);
            panel.SetActive(false);
        }

        private void Start()
        {
            OnActiveBuildableChanged();
        }

        
        private void OnActiveBuildableChanged()
        {
            if (_buildingPlacer.ActiveBuildingItem != null)
            {
                panel.SetActive(true);
                var item = _buildingPlacer.ActiveBuildingItem;
                titleText.text= item.Name;
                if (itemsText != null)
                    itemsText.text =
                        $"{item.itemsDatasNeededToConstruct[0].itemName.ToString() + " " + item.itemsNeedCounts[0].ToString()}";
            }
            }
        
    }
}
