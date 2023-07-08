using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float gravityModifier;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float doubleJumpForce;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem dirtParticle;

    public Animator playerAnim { get; private set; }
    public bool isGameOver { get; set; }
    public bool doubleSpeed { get; private set; }

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private bool isOnGround = true;
    private Rigidbody rb;
    private bool doubleJumpUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        isGameOver = false;
        doubleSpeed = false;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();       
    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
            dirtParticle.Stop();
            doubleJumpUsed = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            rb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 5.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (isGameOver)
            {
                dirtParticle.Stop();
            }
            else
            {
                dirtParticle.Play();
            }
        }
        else if (collision.gameObject.GetComponent<MoveLeft>())
        {
            Debug.Log("Game Over");
            isGameOver = true;
            playerAudio.PlayOneShot(crashSound, 1.0f);                       
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
        }
    }
}
