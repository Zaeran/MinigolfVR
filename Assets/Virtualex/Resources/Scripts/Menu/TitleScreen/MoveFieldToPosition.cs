using UnityEngine;
using System.Collections;

public class MoveFieldToPosition : MonoBehaviour {

    public Transform InputPosition;
    public Transform FinalPosition;

    Transform posToMoveTo;

    void Start()
    {
        posToMoveTo = InputPosition;
    }

    public void MoveToInput()
    {
        posToMoveTo = InputPosition;
    }

    public void MoveToFinal()
    {
        posToMoveTo = FinalPosition;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, posToMoveTo.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, posToMoveTo.position, 80.0f * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, posToMoveTo.rotation, 80 * Time.deltaTime);
        }

        /*if (Input.GetKeyDown(KeyCode.F))
        {
            if (posToMoveTo == InputPosition)
            {
                posToMoveTo = FinalPosition;
            }
            else
            {
                posToMoveTo = InputPosition;
            }
        }
         */
    }
}
