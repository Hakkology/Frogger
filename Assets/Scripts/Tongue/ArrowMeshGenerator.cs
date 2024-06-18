// using UnityEngine;

// public class ArrowMeshGenerator
// {
//     public Mesh CreateArrowMesh()
//     {
//         Mesh mesh = new Mesh();

//         Vector3[] vertices = new Vector3[]
//         {
//             new Vector3(-0.5f, 0, -0.1f), // Sapın arka sol ucu
//             new Vector3(0.5f, 0, -0.1f),  // Sapın arka sağ ucu
//             new Vector3(-0.5f, 0, 0.1f),  // Sapın ön sol ucu
//             new Vector3(0.5f, 0, 0.1f),   // Sapın ön sağ ucu
//             new Vector3(-0.2f, 0, 0.3f),  // Üst üçgenin sol köşesi
//             new Vector3(0.2f, 0, 0.3f),   // Üst üçgenin sağ köşesi
//             new Vector3(0, 0, 0.5f)       // Üst üçgenin ucu
//         };

//         int[] triangles = new int[]
//         {
//             0, 2, 3,  // Arka yüzey sol üçgen
//             0, 3, 1,  // Arka yüzey sağ üçgen
//             2, 5, 3,  // Ön yüzey sol alt üçgen
//             3, 5, 6,  // Ön yüzey sağ üçgen
//             2, 4, 5,  // Ön yüzey sol üst üçgen
//             4, 6, 5   // Ön yüzey üst ucun üçgeni
//         };

//         mesh.vertices = vertices;
//         mesh.triangles = triangles;
//         mesh.RecalculateNormals();

//         return mesh;
//     }
// }
