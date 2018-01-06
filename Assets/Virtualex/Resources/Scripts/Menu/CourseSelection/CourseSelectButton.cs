using UnityEngine;
using System.Collections;

public class CourseSelectButton : MonoBehaviour {

    public CourseSelectionInfo csi;
    public SetPano pano;
    public SceneVoter voter;
    public GameObject menuPanel;

    public void Activate()
    {
        voter.AddVote(csi.courseName);
        pano.Selected();
    //    Application.LoadLevel(courseName);
    }
}
