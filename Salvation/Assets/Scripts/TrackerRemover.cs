using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerRemover : MonoBehaviour
{

    public GameObject tracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Swap this out with OnPathComplete call to increase efficiency
        if(Time.frameCount % 5 == 0)
        {
            if (transform.position == tracker.transform.position)
            {
                tracker.SetActive(false);
            }
        }
    }
}
