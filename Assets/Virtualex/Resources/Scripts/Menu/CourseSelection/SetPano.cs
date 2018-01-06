using UnityEngine;
using System.Collections;

public class SetPano : MonoBehaviour {

    bool isSelected;
    MenuPanelCode basePanel;
    float selectionTime;

    public RotatePano pano;
    string courseName;

	// Use this for initialization
	void Start () {
        isSelected = false;
        basePanel = GetComponent<MenuPanelCode>();
        selectionTime = 0;
        courseName = GetComponent<CourseSelectionInfo>().courseName;
	}

    public void Selected()
    {
        pano.Rotate(courseName, true);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isSelected)
        {
            if (basePanel.IsSelected())
            {
                selectionTime += Time.deltaTime;
                if (selectionTime >= 0.5f)
                {
                    pano.Rotate(courseName);
                    isSelected = true;
                }
            }
            else
            {
                selectionTime = 0;
            }
        }
        else
        {
            if (!basePanel.IsSelected())
            {
                isSelected = false;
                selectionTime = 0;
                pano.Reset();
            }
        }
	}
}
