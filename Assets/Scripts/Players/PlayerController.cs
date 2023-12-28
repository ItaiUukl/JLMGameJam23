using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 Lane1Edge;
    [SerializeField] private Vector3 Lane2Edge;
    [SerializeField] private Vector3 Lane3Edge;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;

    [SerializeField] private float movementLockTime = 0.5f;

    private Globals.LanesEnum p1Lane;
    private Globals.LanesEnum p2Lane;
    private Globals.LanesEnum p3Lane;

    private float lock1;
    private float lock2;
    private float lock3;

    private void Update() {
        UpdatePlayerLock(ref lock1);
        UpdatePlayerLock(ref lock2);
        UpdatePlayerLock(ref lock3);

        if (Input.GetKey("a"))
        {
            MovePlayer(true, ref p1Lane, ref player1, ref lock1);
        }
        if (Input.GetKey("z"))
        {
            MovePlayer(false, ref p1Lane, ref player1, ref lock1);
        }

        if (Input.GetKey("g"))
        {
            MovePlayer(true, ref p2Lane, ref player2, ref lock2);
        }
        if (Input.GetKey("b"))
        {
            MovePlayer(false, ref p2Lane, ref player2, ref lock2);
        }

        if (Input.GetKey("l"))
        {
            MovePlayer(true, ref p3Lane, ref player3, ref lock3);
        }
        if (Input.GetKey(","))
        {
            MovePlayer(false, ref p3Lane, ref player3, ref lock3);
        }
    }

    void MovePlayer(bool moveUp, ref Globals.LanesEnum lane, ref GameObject player, ref float playerLock) {
        if (!isLocked(playerLock)) {
            lane = moveUp ? GetPrevLane(lane) : GetNextLane(lane);
            player.transform.position = GetLanePosition(lane);
            LockPlayer(ref playerLock);
        }
    }

    bool isLocked(float playerLock) {
        return playerLock > 0;
    }

    void LockPlayer(ref float playerLock) {
        playerLock = movementLockTime;
    }

    void UpdatePlayerLock(ref float playerLock) {
        if (isLocked(playerLock)) {
            playerLock -= Time.deltaTime;
        } else {
            playerLock = 0;
        }
    }

    Globals.LanesEnum GetPrevLane(Globals.LanesEnum lane) {
        return (Globals.LanesEnum)Math.Max((int)lane - 1, (int)Globals.LanesEnum.Lane1);
    }

    Globals.LanesEnum GetNextLane(Globals.LanesEnum lane) {
        return (Globals.LanesEnum)Math.Min((int)lane + 1, (int)Globals.LanesEnum.Lane3);
    }
    
    Vector3 GetLanePosition(Globals.LanesEnum lane) {
        if (lane == Globals.LanesEnum.Lane1) {
            return Lane1Edge;
        }
        if (lane == Globals.LanesEnum.Lane2) {
            return Lane2Edge;
        }
        return Lane3Edge;
    }
}
