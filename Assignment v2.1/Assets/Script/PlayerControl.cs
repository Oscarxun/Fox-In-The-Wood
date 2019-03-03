using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    Transform cam;
    Rigidbody rb;

    public float normalSpeed;
    public float fastSpeed;

    private float moveSpeed;

    public TextController textController;
    public TextController losePanel;

    public GameObject pausePanel;

    public float jumpForce;
    private bool isGrounded = false;
    public float rotateSpeed;

    private Vector3 moveDirection;

    private bool canCatch = false;
    public bool isAlive;

    public Animator anim;

    private List<GameObject> objectInRange = new List<GameObject>();

    public int numCaught;
    public Transform foxIconObject;
    public int level;

    public bool caught = false;

    public Collider nextLevel;

    private int hp;
    public GameObject hpBar;
    public bool isDamaged = false;
    private bool canDestroy = false;

    public GameObject locked;

    // Use this for initialization

    public void Awake()
    {
        anim.updateMode = AnimatorUpdateMode.Normal;
    }

    void Start () {
        Time.timeScale = 1;
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();

        hp = 100;
        hpBar.GetComponent<HealthBar>().SetValue(hp);
        
        moveSpeed = normalSpeed;
        isAlive = true;

        level = GameData.Level;
        Debug.Log(GameData.Level);
        if(level == 2)
        {
            transform.position = new Vector3(78, 27, -2);
        }
        foxIconObject.GetComponent<LittleFoxIcon>().ResetIcon(level);
        numCaught = 0;
	}

    void FixedUpdate()
    {
        moveDirection = (cam.right * Input.GetAxisRaw("Horizontal")) + (cam.forward * Input.GetAxisRaw("Vertical"));

        moveDirection.y = 0;
        //moveDirection.Normalize();



        //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //{
        //    anim.SetBool("Attack", false);
        //    anim.SetBool("idle", true);

        //}
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetBool("Attack", false);
            anim.SetBool("idle", true);
        }

        if (Input.GetMouseButtonDown(0))
        {

            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("Attack", true);

        }



        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //anim.SetBool("Attack", false);
            //anim.SetBool("Attack", false);
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.velocity = new Vector3(0, jumpForce, 0);
                isGrounded = false;
                anim.SetBool("idle", false);
                anim.SetBool("walk", false);
                anim.SetBool("run", false);

                anim.SetBool("Jump", true);
            }
            else if (isGrounded)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("idle", true);
            }

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {

                // (Rotate from this, to this, at this speed)
                rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed * Time.deltaTime);
                Vector3 movement = transform.forward * moveSpeed;
                movement.y = rb.velocity.y;
                rb.velocity = movement;

                anim.SetBool("idle", false);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = fastSpeed;
                    anim.SetBool("idle", false);
                    anim.SetBool("walk", false);
                    anim.SetBool("run", true);
                }
                else
                {
                    moveSpeed = normalSpeed;
                    anim.SetBool("walk", true);
                    anim.SetBool("run", false);
                }
            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", false);
                anim.SetBool("idle", true);


            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (canCatch && Input.GetKeyDown(KeyCode.F))
        {
            Caught();
            textController.CloseMessagePanel();
        }

        if(isDamaged)
        {
            hp -= 25;
            if (hp <= 0)
                isAlive = false;
            
            hpBar.GetComponent<HealthBar>().SetValue(hp);
            isDamaged = false;
        }

        if(!isAlive)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("idle", false);
            anim.SetBool("Attack", false);
            anim.SetBool("death", true);

            losePanel.OpenMessagePanel();
            Time.timeScale = 0;
            //if(Input.anyKeyDown)
            //{
            //    SceneManager.LoadScene("GamePlay");
            //    losePanel.CloseMessagePanel();
            //    Time.timeScale = 1;
            //}
        }

        if(numCaught == 3)
        {
            nextLevel.isTrigger = true;
            locked.gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Little Fox")
        {
            canCatch = true;
            caught = false;
            textController.OpenMessagePanel();
            objectInRange.Add(other.gameObject);
        }

        if (other.gameObject.tag == "NextLevel" && nextLevel.isTrigger)
        {
            foxIconObject.GetComponent<LittleFoxIcon>().ClearIcons(level);
            level++;
            GameData.LevelCompleted = level;
            GameData.Level = level;
            numCaught = 0;
            foxIconObject.GetComponent<LittleFoxIcon>().ResetIcon(level);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Apple")
        {
            if (hp < 100)
            {
                hp += 15;
                if (hp > 100)
                    hp = 100;
                hpBar.GetComponent<HealthBar>().SetValue(hp);
            }
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WoodenBox")
        {
            //Debug.Log(100);
            canDestroy = true;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && !anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                //Debug.Log(333434);
                other.GetComponent<Destructable>().DestroyCrate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Little Fox")
        {
            canCatch = false;
            textController.CloseMessagePanel();
            objectInRange.Remove(other.gameObject);
        }

        if (other.gameObject.tag == "WoodenBox")
        {
            canDestroy = false;
        }
    }

    private void Caught()
    {
        if (!caught)
        {
            numCaught++;
            foxIconObject.GetComponent<LittleFoxIcon>().CatchFox(numCaught);
            caught = true;
        }
        foreach (GameObject gameObject in objectInRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "Bridge")
        {
            isGrounded = true;
        }
    }

    public void RestartGame()
    {
        numCaught = 0;
        losePanel.CloseMessagePanel();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        losePanel.CloseMessagePanel();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
