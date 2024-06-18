// using UnityEngine;

// /// <summary>
// /// Represents a segment of the tongue.
// /// </summary>
// public class TongueSegment : MonoBehaviour
// {
//     /// <summary>
//     /// Initializes and extends the tongue segment from the starting to the ending position.
//     /// </summary>
//     /// <param name="start">The world position to start the segment.</param>
//     /// <param name="end">The world position to end the segment.</param>
//     public void InitializeAndExtend(Vector3 start, Vector3 end)
//     {
//         Vector3 direction = (end - start).normalized;
//         float distance = Vector3.Distance(start, end);

//         // Set the initial position and rotation of the segment.
//         transform.position = start + direction * distance / 2;
//         transform.LookAt(end);
//         transform.Rotate(90, 0, 0); // Adjust rotation to align with the direction of extension.

//         // Set the scale to match the distance. Assuming the segment scales along its Y axis.
//         transform.localScale = new Vector3(transform.localScale.x, distance, transform.localScale.z);
//     }
// }
