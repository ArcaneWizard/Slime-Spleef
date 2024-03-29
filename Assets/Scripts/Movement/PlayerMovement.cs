using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private AudioSource audioSource;
    private bool canPlayAudio = true;

    protected override void Awake()
    {
        base.Awake();
        audioSource = transform.GetComponent<AudioSource>();
    }

    protected override void Update() 
    {
        if (!generalDeath.IsDead)
        {
            IsSliding = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space);
            GetUserInput();


            if (IsGrounded && canPlayAudio)
            {
                canPlayAudio = false;
                audioSource.Play();
                StartCoroutine(allowAudioToPlay());
            }

        }

        base.Update();
    }

    private void GetUserInput()
    {
        int x = 0;
        int y = 0;

        if (Input.GetKey(KeyCode.W))
            ++y;
        if (Input.GetKey(KeyCode.S))
            --y;
        if (Input.GetKey(KeyCode.A))
            --x;
        if (Input.GetKey(KeyCode.D))
            ++x;

        MovementDir = new Vector2(x, y);
    }



    private IEnumerator allowAudioToPlay()
    {
        yield return new WaitForSeconds(0.25f);
        canPlayAudio = true;
    }
}
