using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject heart;
    public GameObject stick;
    public GameObject sleep;
    public GameObject bone;
    public GameObject mite;
    public GameObject worm;
    public GameObject pill;

    void Start()
    {
        GameObject[] colls =
        {
            heart, stick, sleep, bone, mite, worm, pill
        };

        // Top row: y = -0.29
        // Mid row: y = -0.5
        // Bottom row: y = -0.76
        float[] ylocs =
        {
            -0.29f, -0.5f, -0.76f
        };

        // Generate obstacles in day
        for (int i = 0; i <= 10; i++) {
            // Get random object
            int objInd = Random.Range(1, 4);
            GameObject obj = colls[objInd];

            // Get random lane
            int yInd = Random.Range(0, ylocs.Length);
            float yLoc = ylocs[yInd];

            // Get random x location between -8f and 8f
            float xLoc = Random.Range(0f, 4f);


            GameObject newObj = Instantiate(obj, new Vector2(xLoc, yLoc), Quaternion.identity);
            newObj.transform.parent = gameObject.transform;
        }
        
        // Generate obstacles in night
        for (int i = 0; i <= 15; i++) {
            // Get random object
            int objInd = Random.Range(4, 7);
            GameObject obj = colls[objInd];

            // Get random lane
            int yInd = Random.Range(0, ylocs.Length);
            float yLoc = ylocs[yInd];

            // Get random x location between -8f and 8f
            float xLoc = Random.Range(5f, 10f);


            GameObject newObj = Instantiate(obj, new Vector2(xLoc, yLoc), Quaternion.identity);
            newObj.transform.parent = gameObject.transform;
        }

        // Generate obstacles in rainbow
        for (int i = 0; i <= 20; i++) {
            // Get random object
            int objInd = Random.Range(0, 4);
            GameObject obj = colls[objInd];

            // Get random lane
            int yInd = Random.Range(0, ylocs.Length);
            float yLoc = ylocs[yInd];

            // Get random x location between -8f and 8f
            float xLoc = Random.Range(11f, 14f);


            GameObject newObj = Instantiate(obj, new Vector2(xLoc, yLoc), Quaternion.identity);
            newObj.transform.parent = gameObject.transform;
        }
    }
}
