using DG.Tweening;
using UnityEngine;

public class TongueSegment : MonoBehaviour
{
    private Transform startPoint;
    private Transform endPoint;

    public void Initialize(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
        UpdateScaleAndPosition();
    }

    private void UpdateScaleAndPosition()
    {
        // Scale the segment to fit between the start and end points
        float distance = Vector3.Distance(startPoint.position, endPoint.position);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance);

        // Position the segment at the midpoint between start and end points
        transform.position = (startPoint.position + endPoint.position) / 2;

        // Rotate the segment to face the end point
        transform.LookAt(endPoint);
    }

    public void Extend(float duration, System.Action onComplete)
    {
        float targetDistance = Vector3.Distance(startPoint.position, endPoint.position);
        Vector3 targetScale = new Vector3(transform.localScale.x, transform.localScale.y, targetDistance);

        transform.DOScale(targetScale, duration).OnComplete(() => onComplete?.Invoke());
    }
}
