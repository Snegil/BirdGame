/*
PLAYER RUN
        if (runAnim && !animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            animator.ResetTrigger("Idle");
animator.SetTrigger("Movement");
        }
        if (runAnim)
{
    animator.speed = isRunning && !isRollerbladeOn ? 2 : Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1);
}



PLAYER JUMP
        animator.speed = 1;
animator.SetTrigger("Jump");



PLAYER CONTROLLER
        animator = playerModel.GetComponent<Animator>();
animator.Update(0f);
animator.Update(0f);


------------------------------------------

PlayerStateChange?.Invoke(playerState);
if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) > deadZone)
{
        playerState = PlayerStates.Walk;
        //PlayerStateChange?.Invoke(playerState);
        return;
}
AllowCamControl = false;

idle.Idle(groundCheck);

------------------------------------------

                PlayerStateChange?.Invoke(playerState);
Debug.Log("WALKING");
if (isRunning)
{
        playerState = PlayerStates.Run;
        //PlayerStateChange?.Invoke(playerState);
        return;
}
if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) < deadZone)
{
        playerState = PlayerStates.Idle;
        //PlayerStateChange?.Invoke(playerState);
        return;
}
AllowCamControl = true;

walk.Walk(waypoint, gameObject, groundCheck, playerModel, rb);

winterTyre.CustomDamping(rb);

------------------------------------------

PlayerStateChange?.Invoke(playerState);
Debug.Log("RUNNING");
if (!isRunning)
{
        playerState = PlayerStates.Walk;
        //PlayerStateChange?.Invoke(playerState);
        return;
}
if (Mathf.Clamp(Vector3.Distance(gameObject.transform.position, waypoint.position), 0, 1) < deadZone)
{
        playerState = PlayerStates.Idle;
        //PlayerStateChange?.Invoke(playerState);
        return;
}

AllowCamControl = true;

run.Run(waypoint, gameObject, groundCheck, playerModel, rb);

winterTyre.CustomDamping(rb);

------------------------------------------

PlayerStateChange?.Invoke(playerState);
AllowCamControl = true;

run.Run(waypoint, gameObject, groundCheck, playerModel, rb);

winterTyre.CustomDamping(rb);

if (!groundCheck.GroundedCheck(0.1f) && rb.linearVelocity.y < 0)
{
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * (1 + fasterDownwardforce), rb.linearVelocity.z);
        return;
}

if (!isjumping)
{
        isjumping = true;

        jump.Jump(rb);
        playerState = PlayerStates.Land;
        return;
}

------------------------------------------

PlayerStateChange?.Invoke(playerState);
AllowCamControl = true;

playerState = PlayerStates.Idle;
isjumping = false;

*/