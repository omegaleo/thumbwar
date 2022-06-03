using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasToCamera : MonoBehaviour
{
    [Header("Resolution")]
    [SerializeField]
    private float width = 1280;
    [SerializeField]
    private float height = 720;

    bool isSet = false;

    // Start is called before the first frame update
    void Update()
    {
        if (gameObject.GetComponent<Canvas>() != null && !isSet)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            if (Camera.main != null)
            {
                canvas.worldCamera = Camera.main;
            }
            else
            {
                canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            isSet = true;
            //scaler.referenceResolution = new Vector2(width,height);
        }
    }

    private void OnEnable()
    {
        if (gameObject.GetComponent<Canvas>() != null)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            if (Camera.main != null)
            {
                canvas.worldCamera = Camera.main;
            }
            else
            {
                canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            }

            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            //scaler.referenceResolution = new Vector2(width,height);
        }
    }

    private void Awake()
    {
        if (gameObject.GetComponent<Canvas>() != null && !isSet)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();
            CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                if(Camera.main != null)
                {
                    canvas.worldCamera = Camera.main;
                }
                else
                {
                    canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                }
            }

            if (scaler != null)
            {
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                //scaler.referenceResolution = new Vector2(width,height);
            }

        }
    }
}
