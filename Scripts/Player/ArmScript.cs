using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour {

    public GameObject leftHandHuman;
    public GameObject rightHandHuman;

    public GameObject leftTentacle;
    public GameObject rightTentacle;
    public GameObject leftHandTrueForm;
    public GameObject rightHandTrueForm;

    public GameObject canvas;

    public bool human;

    private Animator leftHandAnimator;
    private Animator rightHandAnimator;
    private Animator leftHandTrueAnimator;
    private Animator rightHandTrueAnimator;

    private Animator leftTentacleAnimator;
    private Animator rightTentacleAnimator;

    // Use this for initialization
    void Start () {
        human = true;

        leftHandAnimator = leftHandHuman.GetComponent<Animator>();
        rightHandAnimator = rightHandHuman.GetComponent<Animator>();
        leftHandTrueAnimator = leftHandTrueForm.GetComponent<Animator>();
        rightHandTrueAnimator = rightHandTrueForm.GetComponent<Animator>();

        leftTentacleAnimator = leftTentacle.GetComponent<Animator>();
        rightTentacleAnimator = rightTentacle.GetComponent<Animator>();


        leftHandTrueAnimator.Play("LeftTrueHidden");
        rightHandTrueAnimator.Play("RightHandTrueHidden");
        leftTentacleAnimator.Play("LeftTentHidden");
        rightTentacleAnimator.Play("RightTentHidden");
    }
	
	// Update is called once per frame
	void Update () {
       if (Input.GetKeyDown(canvas.GetComponent<Settings>().changeForm))
        {
            if (human)
            {
                HideHumanHands();
                ShowTrueHands();
                human = false;
            }
            else
            {
                HideTrueHands();
                ShowHumanHands();
                human = true;
            }
        }

        if (leftHandAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftHandHidden"))
        {
            //leftHandHuman.SetActive(false);
        }
        if (rightHandAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightHandHidden"))
        {
            //rightHandHuman.SetActive(false);
        }
        if (leftHandTrueAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftTrueHidden"))
        {
            //leftHandTrueForm.SetActive(false);
        }
        if (rightHandTrueAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightHandTrueHidden"))
        {
            //rightHandTrueForm.SetActive(false);
        }
        if (leftTentacleAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftTentHidden"))
        {
            //leftTentacle.SetActive(false);
        }
        if (rightTentacleAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightTentHidden"))
        {
            //rightTentacle.SetActive(false);
        }

    }

    private void HideHumanHands()
    {
        leftHandAnimator.Play("LeftHandHumanHide");
        rightHandAnimator.Play("RightHandHumanHide");
        //leftHandHuman.SetActive(false);
        //rightHandHuman.SetActive(false);
    }

    private void ShowHumanHands()
    {
        leftHandHuman.SetActive(true);
        rightHandHuman.SetActive(true);

        leftHandAnimator.Play("LeftHandHumanShow");
        rightHandAnimator.Play("RightHandHumanShow");

        //leftHandHuman.SetActive(true);
        //rightHandHuman.SetActive(true);
    }

    private void HideTrueHands()
    {
        leftHandTrueAnimator.Play("LeftHandTrueHide");
        rightHandTrueAnimator.Play("RightHandTrueHide");
        leftTentacleAnimator.Play("LeftTentHide");
        rightTentacleAnimator.Play("RightTentHide");
        //leftHandTrueForm.SetActive(false);
        //rightHandTrueForm.SetActive(false);
        //leftTentacle.SetActive(false);
        //rightTentacle.SetActive(false);
    }
    private void ShowTrueHands()
    {
        leftHandTrueForm.SetActive(true);
        rightHandTrueForm.SetActive(true);
        leftTentacle.SetActive(true);
        rightTentacle.SetActive(true);

        leftHandTrueAnimator.Play("LeftHandTrueShow");
        rightHandTrueAnimator.Play("RightHandTrueShow");
        leftTentacleAnimator.Play("LeftTentShow");
        rightTentacleAnimator.Play("RightTentShow");
        //leftHandTrueForm.SetActive(true);
        //rightHandTrueForm.SetActive(true);
        //leftTentacle.SetActive(true);
        //rightTentacle.SetActive(true);
    }
}
