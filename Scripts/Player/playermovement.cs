using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour {

    public float defultFov;
    public float emodeFov;


    private float fps;

    Camera cam;
    public float speed;
    public float sprintMultiplier;
    private float currentSpeed;


    //Speed stuff
    public float trueFormSpeedMultiplier;
    public float totalMultiplier;
    public float trueFormSpeed;
    public float sprinting;


    private Rigidbody rbdy;
    public float camspeed;
    public float pullStrength;
    public float wallJumpStrength;
    public float pullMaxDistance;
    GameObject Player;

    private bool grappeled;
    private bool releasemouse1;
    private string wallOrObject;

    public float distToGround;

    private Vector3 pullPoint;

    public float pullDelay;
    private float pullDelayRemaining;
    private float pullDelaySave;
    private float pullDistance;

    Vector3 rightmove;
    Vector3 forwardmove;

    private Vector3 collisionPoint;

    private bool onWall;
    private GameObject grappledObject;

    private bool collidedWithWall = false;

    public float startingSprintAmount;
    public float sprintAmount;
    public float sprintRechargeRate;
    public float sprintUseageRate;
    private bool sprintInUseThisFrame;
    private bool sprintButtonPressed;

    public Image stamina;
    public float staminaAmount;

    //healthbar stuff
    private Color fullHealthColour = new Color32(255, 0, 0, 255);
    private Color lowHealthColour = new Color32(50, 50, 50, 255);
    public Image healthbar;
    public float maxhealth;
    private float currentHealth;

    public Image healthBarBackground;

    public GameObject camera;

    public GameObject canvas;

    private Vector3 previousVelocityAdded;


    //This needs to be removed after development
    public GameObject showDistance;


    private float airTime;
    public float minSurviveFall;
    public float damageForSeconds;

    private float previousVelocityY;
    private bool previouslyGrounded;

    public float previousVelocityThreshhold;

    public float npcCheckDistance;

    public bool canRegen;
    public float healthRegenAmount;
    public float framesOfHealthRegenLeft;

    public bool eMode;

    private float framesOfEModeLeft;
    private int npcsEaten;

    // Use this for initialization
    void Start() {
        rbdy = gameObject.GetComponent<Rigidbody>();
        //Screen.showCursor = false;
        //rbdy = GetComponent<Rigidbody>();
        // get the distance to ground
        currentSpeed = speed;
        grappeled = false;
        releasemouse1 = false;
        sprintAmount = startingSprintAmount;

        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        //stamina.GetComponent<Image>().color = new Color32(0, 0, 225, 255);

        currentHealth = maxhealth;

        sprinting = 1;
    }
    bool IsGrounded()
    {
        bool onground = (Physics.Raycast(transform.position, -Vector3.up, (distToGround + 0.1f)));
        return onground;
    }

    // Update is called once per frame
    void Update() {

        if (camera.GetComponent<PauseMenu>().paused == false)
        {
            if (npcsEaten == 10)
            {
                eMode = true;
                rbdy.useGravity = false;
                rbdy.velocity = new Vector3(0, 0, 0);
                npcsEaten = 0;
                framesOfEModeLeft = 20 * fps;
            }
            if (framesOfEModeLeft > 0)
            {
                framesOfEModeLeft -= 1;
            }
            if (framesOfEModeLeft <= 0 & eMode == true)
            {
                eMode = false;
                rbdy.useGravity = true;
            }
            float h = camspeed * Input.GetAxis("Mouse X");
            // float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(0, h, 0);
            if (Input.GetKey(canvas.GetComponent<Settings>().jump) & IsGrounded() & !eMode)
            {
                //rbdy.velocity = new Vector3(0, 5, 0);
                rbdy.AddForce(transform.up * 400);
            }
            else if (Input.GetKeyDown(canvas.GetComponent<Settings>().jump) & onWall & !eMode)
            {
                rbdy.useGravity = true;
                //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}, {1}, {2}",collisionPoint ,transform.position,(collisionPoint - transform.position) * -wallJumpStrength);
                //rbdy.AddForce((collisionPoint - transform.position) * -wallJumpStrength);
                onWall = false;
                GameObject.Find("StretchBone").GetComponent<StretchArmScript>().ReturnToDefult();
            }

            //temp to test health
            //if (Input.GetKeyUp(KeyCode.O))
            //{
            //    currentHealth -= 10;
            //    if (currentHealth < 0)
            //    {
            //        currentHealth = 0;
            //    }
            //}

            if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(canvas.GetComponent<Settings>().changeForm) & gameObject.GetComponent<ArmScript>().human)
            {
                //Debug.Log("Mouse 1 released");
                if (grappeled == true)
                {
                    //rbdy.velocity = Vector3.zero;
                    //rbdy.velocity = new Vector3(rbdy.velocity.x, 0, rbdy.velocity.z);
                    rbdy.angularVelocity = Vector3.zero;
                    wallOrObject = null;
                    GameObject.Find("StretchBone").GetComponent<StretchArmScript>().ReturnToDefult();
                }
                grappeled = false;
                releasemouse1 = false;
                if (Input.GetKeyDown(canvas.GetComponent<Settings>().changeForm) & gameObject.GetComponent<ArmScript>().human)
                {
                    releasemouse1 = true;
                }
            }
            if (!eMode)
            {
                checkFallDamage();
            }
            previousVelocityY = rbdy.velocity.y;

            healthRegen();
            //Debug.Log(1 / Time.deltaTime);
            fps = 1 / Time.deltaTime;

            //if (Input.GetKeyUp(KeyCode.L))
            //{
            //    gameObject.transform.position = GameObject.FindGameObjectWithTag("NPC").transform.position;
            //}

        }
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Testing Movement");
        }
    }

    private void FixedUpdate()
    {
        if (camera.GetComponent<PauseMenu>().paused == false)
        {
            if (!eMode)
            {
                Camera.main.fieldOfView = defultFov;
                //rbdy.velocity -= previousVelocityAdded;
                //previousVelocityAdded = new Vector3(0, 0, 0);

                if (Input.GetMouseButton(0) & gameObject.GetComponent<ArmScript>().human)
                {
                    if (releasemouse1 == false)
                    {
                        rbdy.useGravity = true;

                        onWall = false;
                        if (grappeled == false)
                        {
                            GameObject camera = GameObject.Find("Main Camera");
                            cam = camera.GetComponent<Camera>();
                            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit) & hit.distance <= pullMaxDistance)
                            {
                                //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", hit.distance);
                                if (hit.transform.tag == "Wall" || hit.transform.tag == "Building" || hit.transform.tag == "WholeBuilding")
                                {
                                    Vector3 forward = transform.TransformDirection(Vector3.right) * -hit.distance;
                                    Debug.DrawRay(transform.position, forward, Color.green);
                                    pullDelayRemaining = pullDelay * hit.distance;
                                    pullDelaySave = pullDelay * hit.distance;
                                    pullDistance = hit.distance;
                                    //rbdy.AddForce((hit.point - transform.position) * pullStrength);
                                    pullPoint = hit.point;
                                    grappeled = true;
                                    wallOrObject = "Wall";
                                }
                                else if (hit.transform.tag == "InteractibleObject")
                                {
                                    Vector3 forward = transform.TransformDirection(Vector3.right) * -hit.distance;
                                    Debug.DrawRay(transform.position, forward, Color.green);
                                    pullDelayRemaining = pullDelay * hit.distance;
                                    pullDelaySave = pullDelay * hit.distance;
                                    pullDistance = hit.distance;
                                    //rbdy.AddForce((hit.point - transform.position) * pullStrength);
                                    pullPoint = hit.point;
                                    grappeled = true;
                                    wallOrObject = "Object";
                                    grappledObject = hit.collider.gameObject;
                                }
                            }
                            else
                            {
                                //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", hit.distance);
                                releasemouse1 = true;
                            }
                        }
                        else if (grappeled == true)
                        {
                            if (CheckForObjectInBetween() == false)
                            {
                                if (pullDelayRemaining <= 0)
                                {
                                    if (wallOrObject == "Wall")
                                    {
                                        GameObject.Find("StretchBone").GetComponent<StretchArmScript>().StretchToPoint(pullPoint);
                                        rbdy.AddForce((pullPoint - transform.position) * pullStrength);
                                    }
                                    else if (wallOrObject == "Object")
                                    {
                                        GameObject.Find("StretchBone").GetComponent<StretchArmScript>().StretchToPoint(grappledObject.transform.position);
                                        Rigidbody grappledrbdy = grappledObject.GetComponent<Rigidbody>();
                                        grappledrbdy.AddForce((transform.position - grappledObject.transform.position) * pullStrength);

                                        if (grappledObject.name == "lamp(Clone)")
                                        {
                                            grappledObject.GetComponent<LAMP>().isGrappled = true;
                                        }
                                    }
                                }
                                else
                                {
                                    float percentageToGo = pullDelayRemaining / pullDelaySave;
                                    Vector3 currentArmLocation = Vector3.MoveTowards(transform.position, pullPoint, (1 - percentageToGo) * pullDistance);
                                    GameObject.Find("StretchBone").GetComponent<StretchArmScript>().StretchToPoint(currentArmLocation);
                                    //Debug.Log(string.Format("{0} {1} {2} {3} {4}", currentArmLocation, transform.position, percentageToGo, pullDelayRemaining, pullDistance));
                                    pullDelayRemaining -= 1;
                                }
                            }
                            else
                            {
                                grappeled = false;
                                releasemouse1 = true;
                                GameObject.Find("StretchBone").GetComponent<StretchArmScript>().ReturnToDefult();
                            }
                        }

                        //releasemouse1 = true;
                    }
                }
                //True Form Left Mouse Button go here
                if (Input.GetMouseButton(0) & gameObject.GetComponent<ArmScript>().human == false)
                {
                    checkForNpcInView();
                }
                if (Input.GetMouseButton(1) & gameObject.GetComponent<ArmScript>().human == true & grappeled == true & wallOrObject == "Object")
                {
                    RaycastHit hit;
                    if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit))
                    {
                        Rigidbody grappledrbdy = grappledObject.GetComponent<Rigidbody>();
                        grappledrbdy.AddForce((hit.point - grappledObject.transform.position) * pullStrength * 10);

                        wallOrObject = null;
                        GameObject.Find("StretchBone").GetComponent<StretchArmScript>().ReturnToDefult();

                        grappeled = false;
                        releasemouse1 = true;
                    }
                }



                forwardmove = Vector3.zero;
                rightmove = Vector3.zero;
                if (Input.GetKey(canvas.GetComponent<Settings>().sprint))
                {
                    sprintButtonPressed = true;
                }
                if (Input.GetKeyUp(canvas.GetComponent<Settings>().sprint) || sprintAmount <= 0)
                {
                    //currentSpeed = speed;
                    sprinting = 1;
                }
                if (onWall == false)
                {
                    if (Input.GetKey(canvas.GetComponent<Settings>().forward))
                    {

                        //transform.position += transform.right * Time.fixedDeltaTime * -speed;
                        //rbdy.MovePosition(transform.position + -transform.right * Time.fixedDeltaTime * speed);
                        if (sprintButtonPressed & sprintAmount > 0)
                        {
                            sprintSpeed();
                            sprintInUseThisFrame = true;
                        }
                        //rbdy.velocity += -transform.right * currentSpeed;
                        //previousVelocityAdded += -transform.right * currentSpeed;
                        //Debug.Log(previousVelocityAdded);
                        forwardmove = -transform.right * Time.fixedDeltaTime * currentSpeed;
                    }
                    else if (Input.GetKey(canvas.GetComponent<Settings>().backwards))
                    {
                        //transform.position += transform.right * Time.fixedDeltaTime * speed;
                        //rbdy.MovePosition(transform.position + transform.right * Time.fixedDeltaTime * speed);
                        if (sprintButtonPressed & sprintAmount > 0)
                        {
                            sprintSpeed();
                            sprintInUseThisFrame = true;
                        }
                        //rbdy.velocity += transform.right * currentSpeed;
                        //previousVelocityAdded += transform.right * currentSpeed;
                        forwardmove = transform.right * Time.fixedDeltaTime * currentSpeed;
                    }
                    if (Input.GetKey(canvas.GetComponent<Settings>().right))
                    {
                        //transform.position += transform.forward * Time.fixedDeltaTime * speed;
                        //rbdy.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
                        if (sprintButtonPressed & sprintAmount > 0)
                        {
                            sprintSpeed();
                            sprintInUseThisFrame = true;
                        }
                        rightmove = transform.forward * Time.fixedDeltaTime * currentSpeed;
                    }
                    else if (Input.GetKey(canvas.GetComponent<Settings>().left))
                    {
                        //transform.position += transform.forward * Time.fixedDeltaTime * -speed;
                        //rbdy.MovePosition(transform.position + -transform.forward * Time.fixedDeltaTime * speed);
                        if (sprintButtonPressed & sprintAmount > 0)
                        {
                            sprintSpeed();
                            sprintInUseThisFrame = true;
                        }
                        rightmove = -transform.forward * Time.fixedDeltaTime * currentSpeed;
                    }
                    rbdy.MovePosition(rbdy.position + forwardmove + rightmove);
                    //rbdy.velocity = new Vector3(0, rbdy.velocity.y, 0);
                    // rbdy.velocity += forwardmove + rightmove;
                    //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", rbdy.velocity);
                    forwardmove = Vector3.zero;
                    rightmove = Vector3.zero;
                    if (IsGrounded())
                    {
                        rbdy.velocity = Vector3.zero;
                        rbdy.angularVelocity = Vector3.zero;
                    }
                }

                if (sprintInUseThisFrame == false)
                {
                    if (sprintButtonPressed == false)
                    {
                        if (sprintAmount < startingSprintAmount)
                        {
                            sprintAmount += sprintRechargeRate;
                        }
                        if (sprintAmount > startingSprintAmount)
                        {
                            sprintAmount = startingSprintAmount;
                        }
                    }
                }
                else
                {
                    sprintInUseThisFrame = false;
                }

                staminaAmount = sprintAmount / startingSprintAmount;
                stamina.fillAmount = staminaAmount;

                healthbar.fillAmount = currentHealth / maxhealth;

                //healthBarBackground.fillAmount = currentHealth / maxhealth;

                sprintButtonPressed = false;
                healthbar.GetComponent<Image>().color = Color.Lerp(lowHealthColour, fullHealthColour, currentHealth / maxhealth);
            }
            if (gameObject.GetComponent<ArmScript>().human == false)
            {
                trueFormSpeed = trueFormSpeedMultiplier;
            }
            else
            {
                trueFormSpeed = 1;
            }

            totalMultiplier = speed * sprinting * trueFormSpeed;
            currentSpeed = totalMultiplier;
        }
        if (eMode)
        {
            Camera.main.fieldOfView = emodeFov;
            if (Input.GetKey(canvas.GetComponent<Settings>().forward))
            {
                rbdy.AddForce(camera.transform.forward * 100);
            }
            if (Input.GetKey(canvas.GetComponent<Settings>().backwards))
            {
                rbdy.AddForce(-camera.transform.forward * 100);
            }
            if (Input.GetKey(canvas.GetComponent<Settings>().left))
            {
                rbdy.AddForce(-camera.transform.right * 100);
            }
            if (Input.GetKey(canvas.GetComponent<Settings>().right))
            {
                rbdy.AddForce(camera.transform.right * 100);
            }
            if (Input.GetKey(canvas.GetComponent<Settings>().jump))
            {
                rbdy.AddForce(gameObject.transform.up * 100);
            }
            if (Input.GetKey(canvas.GetComponent<Settings>().clingToWall))
            {
                rbdy.AddForce(-gameObject.transform.up * 100);
            }

            EModePullCheck();

            
        }
    }
    private void sprintSpeed()
    {
        if (sprintAmount > 0)
        {
            sprinting = sprintMultiplier;
            sprintAmount -= sprintUseageRate;
            if (sprintAmount < 0)
            {
                sprintAmount = 0;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building" & !IsGrounded())
        {
            if (Input.GetKey(canvas.GetComponent<Settings>().clingToWall))
            {
                onWall = true;
                grappeled = false;
                releasemouse1 = true;
                rbdy.velocity = Vector3.zero;
                rbdy.angularVelocity = Vector3.zero;
                rbdy.useGravity = false;
                //Debug.Log("Get off the wall!");
                collidedWithWall = true;
                //Debug.Log(other.contacts[0].point);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collidedWithWall)
        {
            collisionPoint = collision.contacts[0].point;
            collidedWithWall = false;
        }
    }

    bool CheckForObjectInBetween()
    {
        if (wallOrObject == "Wall")
        {
            //Debug.Log("checking");
            var direction = pullPoint - transform.position;
            float distance = Vector3.Distance(pullPoint, transform.position);
            bool objectInBetween = (Physics.Raycast(transform.position, direction, (distance - 0.1f)));
            Debug.DrawRay(transform.position, direction, Color.red, 5);
            //Debug.Log(objectInBetween);
            //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", objectInBetween);

            if (objectInBetween)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", hit.collider.gameObject.name);
                }
            }

            return objectInBetween;
        }
        else
        {
            return false;
        }
    }

    void checkFallDamage()
    {
        if (IsGrounded())
        {
            if (previouslyGrounded)
            {
                previouslyGrounded = true;
            }
            else
            {
                if (-previousVelocityY > previousVelocityThreshhold)
                {
                    currentHealth -= damageForSeconds * -previousVelocityY;
                    //Debug.Log("DO DAMAGE YOU PEICE OF SHIT PROGRAM");
                    if (currentHealth < 0)
                    {
                        currentHealth = 0;
                    }
                }
            }
        }
        else
        {
            previouslyGrounded = false;
        }
        //showDistance.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}", previousVelocityY);

        //if (!IsGrounded())
        //{
        //    airTime += Time.deltaTime;
        //}
        //else
        //{
        //    if (airTime > minSurviveFall)
        //    {
        //        currentHealth -= damageForSeconds * airTime;
        //    }
        //    airTime = 0;
        //}
    }

    public void checkForNpcInView()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, npcCheckDistance))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.tag == "NPC")
            {
                Destroy(hit.collider.gameObject);
                //Debug.Log("NPC destroyed");
                canRegen = true;
                framesOfHealthRegenLeft += 5 * (fps);

                npcsEaten += 1;
            }
        }
    }

    public void healthRegen()
    {
        if (canRegen)
        {
            if (currentHealth < maxhealth)
            {
                currentHealth += healthRegenAmount*Time.deltaTime;
                if (currentHealth > maxhealth)
                {
                    currentHealth = maxhealth;
                }
            }
            framesOfHealthRegenLeft -= 1;
        }

        if (framesOfHealthRegenLeft <= 0)
        {
            canRegen = false;
            framesOfHealthRegenLeft = 0;
        }
    }
    //public void checkForBuilding()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, npcCheckDistance))
    //    {
    //        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        if (hit.collider.tag == "Building")
    //        {
    //            hit.collider.GetComponent<buildingswitch>().triggerswitch();

    //        }
    //    }
    //}

    public void EModePullCheck()
    {
        if (Input.GetMouseButton(0) & gameObject.GetComponent<ArmScript>().human)
        {
            if (releasemouse1 == false)
            {
                //rbdy.useGravity = true;

                onWall = false;
                if (grappeled == false)
                {
                    GameObject camera = GameObject.Find("Main Camera");
                    cam = camera.GetComponent<Camera>();
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) & hit.distance <= pullMaxDistance * 5)
                    {
                        //Debug.Log(hit.transform.tag);
                        if (hit.transform.tag == "WholeBuilding")
                        {
                            Vector3 forward = transform.TransformDirection(Vector3.right) * -hit.distance;
                            Debug.DrawRay(transform.position, forward, Color.green);
                            pullDelayRemaining = pullDelay * hit.distance;
                            pullDelaySave = pullDelay * hit.distance;
                            pullDistance = hit.distance;
                            pullPoint = hit.point;
                            grappeled = true;
                            wallOrObject = "Object";

                            grappledObject = hit.collider.gameObject;
                        }
                    }
                    else
                    {
                        releasemouse1 = true;
                    }
                }
                else if (grappeled == true)
                {
                    if (CheckForObjectInBetween() == false)
                    {
                        if (pullDelayRemaining <= 0)
                        {
                            if (wallOrObject == "Object")
                            {
                                GameObject.Find("StretchBone").GetComponent<StretchArmScript>().StretchToPoint(grappledObject.transform.position);
                                grappledObject.GetComponent<buildingswitch>().triggerGrapple();
                            }
                        }
                        else
                        {
                            float percentageToGo = pullDelayRemaining / pullDelaySave;
                            Vector3 currentArmLocation = Vector3.MoveTowards(transform.position, grappledObject.transform.position, (1 - percentageToGo) * pullDistance);
                            GameObject.Find("StretchBone").GetComponent<StretchArmScript>().StretchToPoint(currentArmLocation);
                            //Debug.Log(string.Format("{0} {1} {2} {3} {4}", currentArmLocation, transform.position, percentageToGo, pullDelayRemaining, pullDistance));
                            pullDelayRemaining -= 1;
                        }
                    }
                    else
                    {
                        grappeled = false;
                        releasemouse1 = true;
                        GameObject.Find("StretchBone").GetComponent<StretchArmScript>().ReturnToDefult();
                    }
                }
            }
        }


        if (Input.GetMouseButton(1) & gameObject.GetComponent<ArmScript>().human == true & grappeled == true & wallOrObject == "Object")
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit))
            {
                //Rigidbody grappledrbdy = grappledObject.GetComponent<Rigidbody>();
                //grappledrbdy.AddForce((hit.point - grappledObject.transform.position) * pullStrength * 10);

                grappledObject.GetComponent<buildingswitch>().TriggerYeet(hit.point);

                wallOrObject = null;
                GameObject.Find("StretchBone").GetComponent<StretchArmScript>().ReturnToDefult();


                grappeled = false;
                releasemouse1 = true;
            }
        }


    }

    //public void MegaYeet()
    //{
    //    object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));

    //    foreach (object o in obj)
    //    {

    //        GameObject g = (GameObject)o;

    //        if (g.tag == "Building" & Vector3.Distance(gameObject.transform.position, g.transform.position) <= 60)
    //        {
    //            //Debug.Log(g.name);
    //            g.GetComponent<buildingswitch>().triggerswitch();
    //        }
    //        if (g.tag == "InteractibleObject" & Vector3.Distance(gameObject.transform.position, g.transform.position) <= 60 & g.activeSelf)
    //        {
    //            g.GetComponent<Rigidbody>().AddForce((g.transform.position - gameObject.transform.position) * pullStrength * 100);
    //        }

    //    }

    //}

}
