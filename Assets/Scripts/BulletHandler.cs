using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    private float startTime, journeyLength, speed;
    private bool destroyCalled = false;

    private Vector3 start, end;

    private BaseTileHandler target;
    private Unit origin;

    // Update is called once per frame
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(start, end, fractionOfJourney);
        if (fractionOfJourney >= 1 && !destroyCalled)
        {
            destroyCalled = true;
            target.takeDamage(origin.attack);
            // deal damage to target unit
            Destroy(this.gameObject);
        }
    }

    const int PROJECTILE_CONST = 150;
    public void setDestination(Vector3 start, BaseTileHandler target, Unit u)
    {
        startTime = Time.time;
        this.start = start;
        this.end = target.getGameObject().transform.position;
        this.target = target;
        journeyLength = Vector3.Distance(start, end);
        speed = u.projectileSpeed * PROJECTILE_CONST;
        origin = u;
    }
}
