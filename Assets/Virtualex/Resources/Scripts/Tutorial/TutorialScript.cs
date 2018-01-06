using UnityEngine;
using System.Collections;

/// <summary>
/// manages tutorial events
/// </summary>
public class TutorialScript : MonoBehaviour {

    public GameObject putterReveal;
    public GameObject putter;
    public GameObject startArea;
    public GameObject course;
    public GameObject popper;
    public HighlightButton menuBtn;

    bool hasGrabbedPutter;
    ClubReturnToHand club;
    AudioSource myAudio;

    Coroutine currentWaitCoroutine;

	// Use this for initialization
	void Start () {
        hasGrabbedPutter = false;
        myAudio = GetComponent<AudioSource>();
        club = putter.GetComponent<ClubReturnToHand>();
        StartCoroutine(IntroVoice());
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasGrabbedPutter && club.isInHand)
        {
            hasGrabbedPutter = true;
            StartCoroutine(CreateCourse());
        }
	}

    public void RampDone()
    {
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Teleport1") as AudioClip);
    }

    public void RampFail()
    {
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/RampFail") as AudioClip);
    }

    public void TeleportDone()
    {
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/HoleFinish") as AudioClip);
    }

    public void BallOut()
    {
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/BallOut") as AudioClip);
    }

    public void BallIn()
    {
        StartCoroutine(CreatePopper());
    }

    public void PopperPopped()
    {
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Complete1") as AudioClip);
        menuBtn.ActivateColor();
    }

    IEnumerator IntroVoice()
    {
        yield return new WaitForSeconds(5);
        myAudio.Play();
        yield return new WaitForSeconds(16.0f);
        StartCoroutine(RevealPutter());
    }

    IEnumerator RevealPutter()
    {
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Putter") as AudioClip);
        yield return new WaitForSeconds(2.5f);
        putterReveal.SetActive(true);
        putter.SetActive(true);
        currentWaitCoroutine = StartCoroutine(PutterEscalation());
    }

    IEnumerator PutterEscalation()
    {
        yield return new WaitForSeconds(20);
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Putter2") as AudioClip);
        currentWaitCoroutine = StartCoroutine(PutterEscalation2());
    }

    IEnumerator PutterEscalation2()
    {
        yield return new WaitForSeconds(20);
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Putter3") as AudioClip);
        currentWaitCoroutine = StartCoroutine(PutterEscalation3());
    }

    IEnumerator PutterEscalation3()
    {
        yield return new WaitForSeconds(20);
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Putter4") as AudioClip);
    }

    IEnumerator CreateCourse()
    {
        StopCoroutine(currentWaitCoroutine);
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/Ramp1") as AudioClip);
        yield return new WaitForSeconds(5);
        startArea.SetActive(false);
        putterReveal.SetActive(false);
        course.SetActive(true);
    }

    IEnumerator CreatePopper()
    {
        myAudio.Stop();
        myAudio.PlayOneShot(Resources.Load(@"Sounds/TutorialSpeech/PopperCreation") as AudioClip);
        yield return new WaitForSeconds(9.25f);
        popper.SetActive(true);
    }
}
