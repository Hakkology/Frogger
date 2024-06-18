using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

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

        StartCoroutine(RetractTongueRoutine(worldPoints, pathPoints));
    }

    private IEnumerator RetractTongueRoutine(List<Vector3> worldPoints, List<Vector2Int> pathPoints)
    {
        TileManager tileManager = SingletonManager.GetSingleton<TileManager>();
        int pointCount = worldPoints.Count;
        List<GameObject> collectedGrapes = new List<GameObject>();

        float retractDuration = Vector3.Distance(worldPoints[pointCount - 1], tongueRoot.position) / retractSpeed;
        float timeElapsed = 0;

        while (timeElapsed < retractDuration)
        {
            float t = timeElapsed / retractDuration;
            for (int i = 0; i < pointCount; i++)
            {
                Vector3 position = Vector3.Lerp(worldPoints[i], tongueRoot.position, t);
                lineRenderer.SetPosition(i, position);

                if (i > 0) // Check to collect grapes only after the first point
                {
                    Vector2Int gridPos = pathPoints[i-1];
                    Tile tile = tileManager.GetTileAt(gridPos.x, gridPos.y);
                    if (tile != null)
                    {
                        foreach (BaseObject obj in tile.ObjectsOnTile)
                        {
                            if (obj is Grape grape && !collectedGrapes.Contains(grape.gameObject))
                            {
                                collectedGrapes.Add(grape.gameObject); // Üzümü toplananlar listesine ekleyin.
                                grape.transform.DOMove(tongueRoot.position, retractDuration * (1-t)).SetEase(Ease.Linear).OnComplete(() =>
                                {
                                    // OnComplete içinde objenin yok edilip edilmediğini kontrol et.
                                    if (grape != null && grape.gameObject != null) 
                                    {
                                        Destroy(grape.gameObject);
                                    }
                                });
                            }
                        }
                    }
                }
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure all grape positions are at tongue root when animation completes
        foreach (GameObject grape in collectedGrapes)
        {
            grape.transform.position = tongueRoot.position; // In case any animation hasn't completed
        }

        // Reset all line positions to the start
        for (int i = 0; i < pointCount; i++)
        {
            lineRenderer.SetPosition(i, tongueRoot.position);
        }

        OnTongueRetracted?.Invoke();
    }

}
