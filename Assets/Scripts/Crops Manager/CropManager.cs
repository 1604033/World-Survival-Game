using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    
    public Action OnCropGrow;
    public Action<Vector3Int> OnResetWaterLevel;
    public static CropManager instance;
    public List<Crop> crops = new List<Crop>();
    public List<Vector3Int> cropPositions = new List<Vector3Int>();
    public float waitTime  = 10f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
         
        }
        
    }

    private void Start()
    {
        //DayTimeController.Instance.OnnewDayStart += ResetWaterLevel;
        //StartCoroutine(GrowCrop());
    }

    public void AddCrop(Crop crop, Vector3Int cropGridPos)
    {
        crops.Add(crop);
        cropPositions.Add(cropGridPos);
    }
    
    IEnumerator GrowCrop(){
        
        OnCropGrow?.Invoke();
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(GrowCrop());
    }
    
    public void ResetWaterLevel()
    {
        for (int i = 0; i < crops.Count; i++)
        {
            Crop crop = crops[i];
            crop.HydrationLevel = 0;
            Vector3Int cropGridPos = cropPositions[i];
            OnResetWaterLevel?.Invoke(cropGridPos);

        }
    } 
    
}