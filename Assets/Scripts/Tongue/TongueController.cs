using UnityEngine;
using System.Collections;

public class TongueController : MonoBehaviour
{
    public Transform tongueRoot; // Kurbağanın ağzı/dilin başlangıç noktası
    private LineRenderer lineRenderer;
    public float extendSpeed = 5f; // Dilin uzama hızı
    public float retractSpeed = 7f; // Dilin geri çekilme hızı

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
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    public void ExtendTongue(Vector3 targetPosition)
    {
        StartCoroutine(ExtendTongueRoutine(targetPosition));
    }

    private IEnumerator ExtendTongueRoutine(Vector3 targetPosition)
    {
        // Y koordinatını tongueRoot'un Y koordinatı ile sabitle
        targetPosition.y = tongueRoot.position.y;

        Vector3 startPosition = tongueRoot.position;
        float distance = Vector3.Distance(new Vector3(startPosition.x, 0, startPosition.z), 
                                        new Vector3(targetPosition.x, 0, targetPosition.z)); // Y ekseni dikkate alınmadan hesapla
        float extendDuration = distance / extendSpeed;
        float retractDuration = distance / retractSpeed;

        float time = 0;
        while (time < extendDuration)
        {
            float t = time / extendDuration;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, t);
            position.y = tongueRoot.position.y; // Y pozisyonunu sabit tut
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, position);
            time += Time.deltaTime;
            yield return null;
        }

        lineRenderer.SetPosition(1, targetPosition); // Ensure the tongue reaches the target
        OnTongueExtended?.Invoke(); // Tetikleme eventi
        yield return new WaitForSeconds(1); // Pause at full extension

        // Dilin geri çekilmesi
        time = 0;
        while (time < retractDuration)
        {
            float t = time / retractDuration;
            Vector3 position = Vector3.Lerp(targetPosition, startPosition, t);
            position.y = tongueRoot.position.y; // Y pozisyonunu sabit tut
            lineRenderer.SetPosition(1, position);
            time += Time.deltaTime;
            yield return null;
        }

        lineRenderer.SetPosition(1, startPosition); // Tamamen geri çekilme
        OnTongueRetracted?.Invoke(); // Tetikleme eventi
    }

}
