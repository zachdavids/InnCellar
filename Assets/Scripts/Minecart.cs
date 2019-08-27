using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    public GameObject MineTrack; // The track (spline object) to which this minecart is bound;
    public float initialTrackLocation;
    public bool broken;
    private float velocity; // steps per second (float from 0 to 1 representing portion of spline traversed per second)
    private float currentTrackPosition; // spline steps (0 to 1)
    private bool moving;
    private bool returning;
    private GameObject CarryingPlayer;


    // Start is called before the first frame update
    void Start()
    {
        HermiteSpline spline = MineTrack.GetComponent<HermiteSpline>();
        currentTrackPosition = initialTrackLocation;
        this.transform.position = spline.InterpolateSpline(currentTrackPosition);
        this.transform.LookAt(this.transform.position + spline.GetTangeantAtStep(initialTrackLocation), Vector3.up);
        this.transform.Rotate(270, 0, 0);
        CarryingPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            HermiteSpline spline = MineTrack.GetComponent<HermiteSpline>();
            float buildupAccel = 0.02f;
            float maxVelocity = 0.25f;
            if (returning) maxVelocity = 0.1f;
            if (velocity < maxVelocity)
            {
                velocity += buildupAccel;
            }

            if(currentTrackPosition > 0.7f && !returning)
            {
                velocity = maxVelocity * ((1.0f - currentTrackPosition) / 0.2f);
            }

            if(currentTrackPosition < 0.2f && returning)
            {
                velocity = maxVelocity * (currentTrackPosition / 0.2f);
            }


            if (returning)
            {
                currentTrackPosition -= velocity * Time.deltaTime;
            }
            else
            {
                currentTrackPosition += velocity * Time.deltaTime;
            }

            if(currentTrackPosition >= 0.995f && velocity < buildupAccel)
            {
                currentTrackPosition = 0.995f;
                Arrived();
            }

            if(currentTrackPosition <= 0.005f && velocity < buildupAccel)
            {
                currentTrackPosition = 0.0f;
                Returned();
            }

            this.transform.position = spline.InterpolateSpline(currentTrackPosition);
            this.transform.LookAt(this.transform.position + spline.GetTangeantAtStep(currentTrackPosition), Vector3.up);
            this.transform.Rotate(270, 0, 0);
            if (CarryingPlayer != null)
            {
                this.CarryingPlayer.transform.position = this.transform.position;
                this.CarryingPlayer.transform.LookAt(this.transform.position + spline.GetTangeantAtStep(currentTrackPosition), Vector3.up);
            }
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
                float distanceThreshold = 7.0f;
                if(Vector3.Distance(manager.Artisan.transform.position, this.transform.position) < distanceThreshold)
                {
                    Debug.Log("Artisan can mount");
                    Board(manager.Artisan);
                }
                else if(Vector3.Distance(manager.Bard.transform.position, this.transform.position) < distanceThreshold)
                {
                    Debug.Log("Bard can mount");
                    Board(manager.Bard);
                }
                else if(Vector3.Distance(manager.Warrior.transform.position, this.transform.position) < distanceThreshold)
                {
                    Debug.Log("Warrior can mount");
                    Board(manager.Warrior);
                }
                else if(Vector3.Distance(manager.Thief.transform.position, this.transform.position) < distanceThreshold)
                {
                    Debug.Log("Thief can mount");
                    Board(manager.Thief);
                }

            }
        }


    }

    private void Arrived()
    {
        returning = true;
        velocity = 0.0f;
        if(CarryingPlayer != null)
        {
            HermiteSpline spline = MineTrack.GetComponent<HermiteSpline>();
            //CarryingPlayer.transform.position = spline.InterpolateSpline(1.0f) + spline.GetTangeantAtStep(0.98f);
            CarryingPlayer.GetComponent<HeroMovement>().GotOffMinecart(spline.InterpolateSpline(1.0f) + spline.GetTangeantAtStep(0.98f));
            CarryingPlayer = null;
        }
    }

    private void Returned()
    {
        moving = false;
        returning = false;
        velocity = 0.0f;
    }

    private void Board(GameObject character)
    {
        moving = true;
        velocity = 0.0f;
        CarryingPlayer = character;
        CarryingPlayer.GetComponent<HeroMovement>().BoardedMinecart();
    }

    private void TestStart()
    {
        moving = true;
        velocity = 0.0f;
    }
}
