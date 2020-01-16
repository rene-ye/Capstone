using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    private float startTime, journeyLength, speed;

    private Vector3 start, end;

    // Update is called once per frame
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(start, end, fractionOfJourney);
        if (fractionOfJourney >= 1)
        {
            // deal damage to target unit
            Destroy(this.gameObject);
        }
    }

    public void setDestination(Vector3 start, Vector3 end, float velocity)
    {
        startTime = Time.time;
        this.start = start;
        this.end = end;
        journeyLength = Vector3.Distance(start, end);
        speed = velocity;
    }
}
