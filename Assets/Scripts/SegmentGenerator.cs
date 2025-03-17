using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject[] segment; // Array of segments to spawn
    [SerializeField] int zPos = 50; // Z position for spawning
    [SerializeField] bool creatingSegment = false; // Flag to track segment creation
    [SerializeField] int segmentNum; // Randomly chosen segment index
    [SerializeField] Transform player; // Player's transform to track position
    private List<GameObject> spawnedSegments = new List<GameObject>(); // List to store spawned segments

    void Update()
    {
        if (!creatingSegment)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }

        CleanupSegments(); // Check and destroy segments behind the player
    }

    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, segment.Length); // Randomly pick a segment
        GameObject newSegment = Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
        spawnedSegments.Add(newSegment); // Add the new segment to the list
        zPos += 50; // Increment the Z position for the next segment
        yield return new WaitForSeconds(3);
        creatingSegment = false;
    }

    void CleanupSegments()
    {
        // Remove segments that are far behind the player
        for (int i = spawnedSegments.Count - 1; i >= 0; i--)
        {
            if (spawnedSegments[i].transform.position.z < player.position.z - 50) // Adjust the distance threshold as needed
            {
                Destroy(spawnedSegments[i]); // Destroy the segment
                spawnedSegments.RemoveAt(i); // Remove it from the list
            }
        }
    }
}
