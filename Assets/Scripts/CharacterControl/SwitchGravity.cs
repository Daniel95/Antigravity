﻿using UnityEngine;
using System;
using System.Collections;

public class SwitchGravity : MonoBehaviour {

    private PlayerScriptAccess plrAcces;

    private Frames frames;

    public Action switchedGravity;

    private bool inBouncyTrigger;

    private Vector2 lastDir;

	// Use this for initialization
	void Start () {
        plrAcces = GetComponent<PlayerScriptAccess>();
        frames = GetComponent<Frames>();
    }

    public void Jump()
    {
        plrAcces.controlVelocity.TempSpeedIncrease();

        Vector2 raycastResults = new Vector2(plrAcces.charRaycasting.CheckHorizontalDir(), plrAcces.charRaycasting.CheckVerticalDir());

        //check if we have raycast collision on only one axis, jumping wont work when we are in a corner
        if (raycastResults.x == 0 || raycastResults.y == 0)
        {
            Vector2 newDir = new Vector2();

            //check the raycastdir, our newDir is the opposite of one of the axes
            if (raycastResults.x != 0)
            {
                newDir.x = raycastResults.x * -1;
            }
            else
            {
                newDir.y = raycastResults.y * -1;
            }

            plrAcces.controlVelocity.SetDirection(plrAcces.controlVelocity.AdjustDirToMultiplier(newDir + plrAcces.controlVelocity.GetDirection()));

            if (switchedGravity != null)
                switchedGravity();
        }
    }

    private void Bounce() {
        plrAcces.controlVelocity.TempSpeedIncrease();

        Vector2 raycastResults = new Vector2(plrAcces.charRaycasting.CheckHorizontalDir(), plrAcces.charRaycasting.CheckVerticalDir());

        if (raycastResults.x != 0 || raycastResults.y != 0)
        {
            Vector2 newDir = plrAcces.controlVelocity.AdjustDirToMultiplier(lastDir);

            //check the raycastdir, our newDir is the opposite of one of the axes
            if (raycastResults.x != 0)
            {
                newDir.x *= -1;
            }
            if (raycastResults.y != 0)
            {
                newDir.y *= -1;
            }

            plrAcces.controlVelocity.SetDirection(newDir);

            if (switchedGravity != null)
                switchedGravity();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.Bouncy)) {
            if (collision.isTrigger)
            {
                inBouncyTrigger = true;
            }
            else
            {
                lastDir = plrAcces.controlVelocity.GetLastVelocity.normalized;

                frames.ExecuteAfterDelay(4, Bounce);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (inBouncyTrigger && collision.isTrigger && collision.transform.CompareTag(Tags.Bouncy))
        {
            inBouncyTrigger = false;
        }
    }
}
