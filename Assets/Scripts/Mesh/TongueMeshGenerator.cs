using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TongueMesh : MonoBehaviour
{
    Mesh tongueMesh;

    private void Awake()
    {
        tongueMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = tongueMesh;
        CreateTongueShape();
        gameObject.SetActive(false);
    }

    private void Update() 
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.y = transform.position.y; 
        transform.LookAt(cameraPosition);
    }
    
    void CreateTongueShape()
    {
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),  // Başlangıçta dilin ucu görünmez
            new Vector3(0, 0.1f, 0),
            new Vector3(0, 0.1f, 0)  // Başlangıçta dilin ucu görünmez
        };

        int[] triangles = new int[6]
        {
            0, 2, 1,
            2, 3, 1
        };

        tongueMesh.vertices = vertices;
        tongueMesh.triangles = triangles;
        tongueMesh.RecalculateNormals();
    }

    public void ExtendTongue(Direction facingDirection, float length, float duration, System.Action onComplete)
    {
        gameObject.SetActive(true);
        Vector3[] vertices = tongueMesh.vertices;

        // Düzgün yön ayarlamaları
        Vector3 targetDirection = Vector3.zero;
        switch (facingDirection)
        {
            case Direction.Up:
                targetDirection = new Vector3(0, 0, length);  // Z ekseninde uzat
                break;
            case Direction.Down:
                targetDirection = new Vector3(0, 0, -length); // Z ekseninde ters uzat
                break;
            case Direction.Right:
                targetDirection = new Vector3(length, 0, 0);  // X ekseninde uzat
                break;
            case Direction.Left:
                targetDirection = new Vector3(-length, 0, 0); // X ekseninde ters uzat
                break;
        }

        // Hedef noktaya doğru animasyon
        DOTween.To(() => vertices[1], x => vertices[1] = x, targetDirection, duration)
            .OnUpdate(() =>
            {
                vertices[3] = vertices[1];
                tongueMesh.vertices = vertices;
                tongueMesh.RecalculateBounds();
            })
            .OnComplete(() => RetractTongue(duration / 2, onComplete));
    }

    private void RetractTongue(float duration, System.Action onComplete)
    {
        Vector3[] vertices = tongueMesh.vertices;
        Vector3 initialPosition = new Vector3(0, 0, 0);
        DOTween.To(() => vertices[1], x => vertices[1] = x, initialPosition, duration)
            .OnUpdate(() =>
            {
                vertices[3] = initialPosition;
                tongueMesh.vertices = vertices;
                tongueMesh.RecalculateBounds();
            })
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
    }

}
