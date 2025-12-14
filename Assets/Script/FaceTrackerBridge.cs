using UnityEngine;
using System;

public class FaceTrackerBridge : MonoBehaviour
{
    public Transform glassesHolder;
    public bool showLandmarks = true;
    
    private Vector3[] testLandmarks = new Vector3[468];
    
    void Start()
    {
        if (glassesHolder == null) 
            glassesHolder = GameObject.Find("GlassesHolder")?.transform;
        
        GenerateTestFace();
    }
    
    void Update()
    {
        UpdateGlasses();
    }
    
    void GenerateTestFace()
    {
        for (int i = 0; i < 468; i++)
        {
            testLandmarks[i] = new Vector3(
                (i % 30) * 0.005f - 0.07f,
                (i / 30) * 0.005f - 0.05f,
                Mathf.Sin(i * 0.1f) * 0.02f
            );
        }
    }
    
    public void OnFaceData(string jsonData)
    {
        FaceData data = JsonUtility.FromJson<FaceData>(jsonData);
        
        testLandmarks[1].x = data.noseX * 0.5f - 0.25f;
        testLandmarks[1].y = -(data.noseY * 0.5f - 0.25f);
        
        Debug.Log($"Face detected: Nose at ({data.noseX:F2}, {data.noseY:F2})");
    }
    
    [System.Serializable]
    public class FaceData
    {
        public float noseX;
        public float noseY;
    }
    
    void UpdateGlasses()
    {
        if (glassesHolder == null) return;
        
        // Fix for landmarks 6 & 197 not initialized
        Vector3 nosePos = testLandmarks[1];
        glassesHolder.localPosition = new Vector3(nosePos.x, nosePos.y, 0.1f);
        
        if (showLandmarks)
        {
            Debug.DrawRay(nosePos, Vector3.forward * 0.05f, Color.red);
            Debug.DrawRay(testLandmarks[1], Vector3.up * 0.02f, Color.yellow);
        }
    }
}
