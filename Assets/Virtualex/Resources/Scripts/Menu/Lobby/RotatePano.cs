using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotatePano : MonoBehaviour {

    List<Transform> children;
    string course;
    string queue;
    bool isCoroutine;
    string previousPano;
    string selectedCourse;


	// Use this for initialization
	void Start () {
        queue = "";
        previousPano = "golfball";
        selectedCourse = "";
        course = "golfball";
        children = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }
	}

    public void Reset()
    {
        Rotate(selectedCourse == "" ? previousPano : selectedCourse);
    }

    public bool isPanoActive(string pano)
    {
        return course == pano;
    }

    public void Rotate(string courseName, bool isSelected = false)
    {
        previousPano = course;
        if (isSelected)
        {
            selectedCourse = courseName;
        }
        if (course != courseName)
        {
            if (!isCoroutine)
            {
                course = courseName;
                Debug.Log(course);
                StartCoroutine(spinObj());
            }
            else
            {
                queue = courseName;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isCoroutine && queue != "")
        {
            Rotate(queue);
            queue = "";
        }
	}

    IEnumerator spinObj()
    {
        isCoroutine = true;
        for (int i = 0; i < children.Count; i += 10)
        {
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(spinPanel(i));
            StartCoroutine(spinPanel(i + 1));
            StartCoroutine(spinPanel(i + 2));
            StartCoroutine(spinPanel(i + 3));
            StartCoroutine(spinPanel(i + 4));
            StartCoroutine(spinPanel(i + 5));
            StartCoroutine(spinPanel(i + 6));
            StartCoroutine(spinPanel(i + 7));
            StartCoroutine(spinPanel(i + 8));
            StartCoroutine(spinPanel(i + 9));
        }
        yield return new WaitForSeconds(0.5f);
        isCoroutine = false;
    }

    IEnumerator spinPanel(int panelNo)
    {
        if (children.Count <= panelNo)
        {
            yield return new WaitForSeconds(0);
        }
        else
        {
            /*
            Vector3 normal = (transform.position - children[panelNo].position).normalized;
            Vector3 tangent = Vector3.Cross(normal, Vector3.forward);

            for (int i = 0; i < 10; i++)
            {
                if (tangent.magnitude == 0)
                {
                    tangent = Vector3.Cross(normal, Vector3.up);
                }
                children[panelNo].Rotate(tangent, 36, Space.World);
                if (i == 5)
                {
                    children[panelNo].GetComponent<Renderer>().material = Resources.Load(@"Materials/CoursePano/" + course + "Pano") as Material;
                }
                yield return new WaitForSeconds(0.01f);
            }
             */
            children[panelNo].GetComponent<Renderer>().material = Resources.Load(@"Materials/CoursePano/" + course + "Pano") as Material;
        }
    }
}
