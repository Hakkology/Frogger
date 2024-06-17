using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class TongueManager : MonoBehaviour
{
    public GameObject tongueSegmentPrefab;
    private List<GameObject> tongueSegments = new List<GameObject>();
    private TileManager tileManager;

    private void Start()
    {
        tileManager = SingletonManager.GetSingleton<TileManager>();
    }

    public void ExtendTongue(List<Vector2Int> path, System.Action onComplete)
    {
        StartCoroutine(ExtendTongueCoroutine(path, onComplete));
    }

    private IEnumerator<WaitForSeconds> ExtendTongueCoroutine(List<Vector2Int> path, System.Action onComplete)
    {
        Vector3 startPoint = transform.position;

        foreach (Vector2Int position in path)
        {
            if (tileManager == null)
                tileManager = SingletonManager.GetSingleton<TileManager>();

            Tile tile = tileManager.GetTileAt(position.x, position.y);
            if (tile != null)
            {
                Vector3 endPoint = tile.transform.position + new Vector3(0, 0.3f, 0);
                Debug.Log($"Creating tongue segment from {startPoint} to {endPoint}");

                GameObject tongueSegment = Instantiate(tongueSegmentPrefab, startPoint, Quaternion.identity, transform);
                tongueSegments.Add(tongueSegment);

                TongueSegment segmentScript = tongueSegment.GetComponent<TongueSegment>();
                segmentScript.Initialize(startPoint, endPoint);
                segmentScript.Extend(0.5f, null);

                startPoint = endPoint;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                Debug.LogError($"Tile not found at position: {position}");
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);
        RetractTongue(onComplete);
    }

    private void RetractTongue(System.Action onComplete)
    {
        foreach (GameObject segment in tongueSegments)
        {
            Destroy(segment);
        }

        tongueSegments.Clear();
        onComplete?.Invoke();
    }
}
