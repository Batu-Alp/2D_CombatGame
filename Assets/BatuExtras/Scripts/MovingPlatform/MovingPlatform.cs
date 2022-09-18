using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public List<Transform> points;
    public float move_speed;
    public int target;
    // Start is called before the first frame update
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, points[target].position, move_speed * Time.deltaTime);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (transform.position == points[target].position)
        {

            if (target == points.Count - 1)
            {

                target = 0;
            }

            else
            {

                target += 1;
            }
        }

    }
}
