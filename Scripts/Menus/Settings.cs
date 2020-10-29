using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    public KeyCode forward;
    public KeyCode backwards;
    public KeyCode left;
    public KeyCode right;
    //private KeyCode grapple;
    public KeyCode jump;
    //private KeyCode pause;
    public KeyCode sprint;
    public KeyCode clingToWall;
    public KeyCode changeForm;

    private bool startKeysSetUp;

    public GameObject forwardText;
    private bool forwardKeyBeingChanged;

    public GameObject backwardText;
    private bool backwardKeyBeingChanged;

    public GameObject leftText;
    private bool leftKeyBeingChanged;

    public GameObject rightText;
    private bool rightKeyBeingChanged;

    public GameObject jumpText;
    private bool jumpKeyBeingChanged;

    public GameObject sprintText;
    private bool sprintKeyBeingChanged;

    public GameObject clingText;
    private bool clingKeyBeingChanged;

    public GameObject changeFormText;
    private bool changeFormKeyBeingChanged;

    // Use this for initialization
    void Start () {
        if (!PlayerPrefs.HasKey("ForwardKey"))
        {
            forward = KeyCode.W;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("BackwardKey"))
        {
            backwards = KeyCode.S;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("LeftKey"))
        {
            left = KeyCode.A;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("RightKey"))
        {
            right = KeyCode.D;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("JumpKey"))
        {
            jump = KeyCode.Space;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("SprintKey"))
        {
            sprint = KeyCode.LeftAlt;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("ClingToWallKey"))
        {
            clingToWall = KeyCode.LeftShift;
            startKeysSetUp = true;
        }
        if (!PlayerPrefs.HasKey("ChangeFormKey"))
        {
            changeForm = KeyCode.E;
            startKeysSetUp = true;
        }
        if (startKeysSetUp == true)
        {
            Debug.Log("Setting Up keys");
            Save();
        }

        Load();

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Backslash))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Cleared Player prefs");
            Load();
        }
        if (forwardKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    forward = vKey;
                    forwardKeyBeingChanged = false;
                    forwardText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Forward: {0}", forward);
                    Save();
                }
            }
        }
        if (backwardKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    backwards = vKey;
                    backwardKeyBeingChanged = false;
                    backwardText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Backward: {0}", backwards);
                    Save();
                }
            }
        }
        if (leftKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    left = vKey;
                    leftKeyBeingChanged = false;
                    leftText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Left: {0}", left);
                    Save();
                }
            }
        }
        if (rightKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    right = vKey;
                    rightKeyBeingChanged = false;
                    rightText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Right: {0}", right);
                    Save();
                }
            }
        }
        if (jumpKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    jump = vKey;
                    jumpKeyBeingChanged = false;
                    jumpText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Jump: {0}", jump);
                    Save();
                }
            }
        }
        if (sprintKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    sprint = vKey;
                    sprintKeyBeingChanged = false;
                    sprintText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Sprint: {0}", sprint);
                    Save();
                }
            }
        }
        if (clingKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    clingToWall = vKey;
                    clingKeyBeingChanged = false;
                    clingText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Cling to wall: {0}", clingToWall);
                    Save();
                }
            }
        }
        if (changeFormKeyBeingChanged)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    changeForm = vKey;
                    changeFormKeyBeingChanged = false;
                    changeFormText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Change form: {0}", changeForm);
                    Save();
                }
            }
        }

    }

    void Save()
    {
        int intForward = (int)forward;
        PlayerPrefs.SetInt("ForwardKey", intForward);

        int intBackward = (int)backwards;
        PlayerPrefs.SetInt("BackwardKey", intBackward);

        int intLeft = (int)left;
        PlayerPrefs.SetInt("LeftKey", intLeft);

        int intRight = (int)right;
        PlayerPrefs.SetInt("RightKey", intRight);

        int intJump = (int)jump;
        PlayerPrefs.SetInt("JumpKey", intJump);

        int intSprint = (int)sprint;
        PlayerPrefs.SetInt("SprintKey", intSprint);

        int intCling = (int)clingToWall;
        PlayerPrefs.SetInt("ClingToWallKey", intCling);

        int intChangeForm = (int)changeForm;
        PlayerPrefs.SetInt("ChangeFormKey", intChangeForm);
    }
    void Load()
    {
        forward = (KeyCode)PlayerPrefs.GetInt("ForwardKey");
        backwards = (KeyCode)PlayerPrefs.GetInt("BackwardKey");
        left = (KeyCode)PlayerPrefs.GetInt("LeftKey");
        right = (KeyCode)PlayerPrefs.GetInt("RightKey");
        jump = (KeyCode)PlayerPrefs.GetInt("JumpKey");
        sprint = (KeyCode)PlayerPrefs.GetInt("SprintKey");
        clingToWall = (KeyCode)PlayerPrefs.GetInt("ClingToWallKey");
        changeForm = (KeyCode)PlayerPrefs.GetInt("ChangeFormKey");
        Debug.Log(forward);
        Debug.Log((KeyCode)PlayerPrefs.GetInt("ForwardKey"));

        forwardText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Forward: {0}", forward);
        backwardText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Backward: {0}", backwards);
        leftText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Left: {0}", left);
        rightText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Right: {0}", right);
        sprintText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Sprint: {0}", sprint);
        jumpText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Jump: {0}", jump);
        clingText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Cling to wall: {0}", clingToWall);
        changeFormText.GetComponent<UnityEngine.UI.Text>().text = string.Format("Change form: {0}", changeForm);

    }

    public void ForwardKeySettingPressed()
    {
        forwardText.GetComponent<UnityEngine.UI.Text>().text = "Forward: Press Any Key";
        forwardKeyBeingChanged = true;
    }
    public void BackwardKeySettingPressed()
    {
        backwardText.GetComponent<UnityEngine.UI.Text>().text = "Backward: Press Any Key";
        backwardKeyBeingChanged = true;
    }
    public void LeftKeySettingPressed()
    {
        leftText.GetComponent<UnityEngine.UI.Text>().text = "Left: Press Any Key";
        leftKeyBeingChanged = true;
    }
    public void RightKeySettingPressed()
    {
        rightText.GetComponent<UnityEngine.UI.Text>().text = "Right: Press Any Key";
        rightKeyBeingChanged = true;
    }
    public void SprintKeySettingPressed()
    {
        sprintText.GetComponent<UnityEngine.UI.Text>().text = "Sprint: Press Any Key";
        sprintKeyBeingChanged = true;
    }
    public void JumpKeySettingPressed()
    {
        jumpText.GetComponent<UnityEngine.UI.Text>().text = "Jump: Press Any Key";
        jumpKeyBeingChanged = true;
    }
    public void ClingKeySettingPressed()
    {
        clingText.GetComponent<UnityEngine.UI.Text>().text = "Cling to wall: Press Any Key";
        clingKeyBeingChanged = true;
    }
    public void ChangeFormSettingPressed()
    {
        changeFormText.GetComponent<UnityEngine.UI.Text>().text = "Change form: Press Any Key";
        changeFormKeyBeingChanged = true;
    }

}
