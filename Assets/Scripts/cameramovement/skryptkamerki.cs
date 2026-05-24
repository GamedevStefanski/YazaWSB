using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam; // obiekt kamery

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize; // zakres powiêkszenia

    [Header("Ustawienia granic mapy")]
    [SerializeField]
    private Transform mapRoot;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private Vector3 dragOrigin;

    private void Awake()
    {
        CalculateMapBounds();
    }

    void Start()
    {
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    void Update()
    {
        PanCamera();
        HandleZoom(); // Dodano obs³ugê kó³ka myszy
    }

    private void CalculateMapBounds()
    {
        if (mapRoot == null) return;

        SpriteRenderer[] renderers = mapRoot.GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length == 0) return;

        Bounds combinedBounds = renderers[0].bounds;
        foreach (SpriteRenderer sr in renderers)
        {
            combinedBounds.Encapsulate(sr.bounds);
        }

        mapMinX = combinedBounds.min.x;
        mapMaxX = combinedBounds.max.x;
        mapMinY = combinedBounds.min.y;
        mapMaxY = combinedBounds.max.y;
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    // Nowa funkcja obs³uguj¹ca scroll myszki
    private void HandleZoom()
    {
        // Pobiera wartoœæ ze scrolla (zazwyczaj jest to u³amek np. 0.1 lub -0.1 za jedno 'tykniêcie')
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0f)
        {
            // Mno¿ymy przez zoomStep (i dodatkowo x10, poniewa¿ scrollData to bardzo ma³e wartoœci)
            float newSize = cam.orthographicSize - (scrollData * zoomStep * 10f);

            // Zaciskamy w dozwolonych granicach
            cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

            // Poprawiamy pozycjê, ¿eby kamera nie wysz³a poza mapê przy oddalaniu
            cam.transform.position = ClampCamera(cam.transform.position);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}