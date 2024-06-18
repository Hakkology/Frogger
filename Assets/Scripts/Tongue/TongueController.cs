using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TongueController : MonoBehaviour
{
    public Transform tongueRoot;
    private LineRenderer lineRenderer;
    public float extendSpeed = 3f;
    public float retractSpeed = 3f;
    public Material tongueMaterial;

    public delegate void TongueAction();
    public event TongueAction OnTongueExtended;
    public event TongueAction OnTongueRetracted;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        InitializeLineRenderer();
    }

    private void InitializeLineRenderer()
    {
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = tongueMaterial;
    }

    public void StartExtendTongue(List<Vector2Int> pathPoints)
    {
        StartCoroutine(ExtendTongueRoutine(pathPoints));
    }

    private IEnumerator ExtendTongueRoutine(List<Vector2Int> pathPoints)
    {
        List<Vector3> worldPoints = new List<Vector3>() { tongueRoot.position };
        foreach (Vector2Int point in pathPoints)
        {
            Tile tile = SingletonManager.GetSingleton<TileManager>().GetTileAt(point.x, point.y);
            if (tile != null)
                worldPoints.Add(new Vector3(tile.transform.position.x, tongueRoot.position.y, tile.transform.position.z));
        }

        lineRenderer.positionCount = worldPoints.Count;
        lineRenderer.SetPositions(worldPoints.ToArray());

        OnTongueExtended?.Invoke();
        yield return new WaitForSeconds(1);

        StartCoroutine(RetractTongueRoutine(worldPoints));
    }

    private IEnumerator RetractTongueRoutine(List<Vector3> worldPoints)
    {
        int pointCount = worldPoints.Count;
        Vector3 startPosition = tongueRoot.position;  // Başlangıç noktası

        // İlk pozisyon hariç tüm pozisyonları başlangıç noktasına ayarla
        float retractDuration = (Vector3.Distance(worldPoints[pointCount - 1], startPosition) / retractSpeed);
        float timeElapsed = 0;

        while (timeElapsed < retractDuration)
        {
            float t = timeElapsed / retractDuration;
            for (int i = 1; i < pointCount; i++)  // İlk nokta zaten başlangıçta
            {
                Vector3 position = Vector3.Lerp(worldPoints[i], startPosition, t);
                lineRenderer.SetPosition(i, position);
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Tüm pozisyonları kesin olarak başlangıç noktasına ayarla
        for (int i = 1; i < pointCount; i++)
        {
            lineRenderer.SetPosition(i, startPosition);
        }

        OnTongueRetracted?.Invoke();
    }
}
