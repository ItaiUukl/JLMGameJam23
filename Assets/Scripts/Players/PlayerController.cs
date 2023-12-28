using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Lane lane1;
    [SerializeField] private Lane lane2;
    [SerializeField] private Lane lane3;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Player player3;

    [SerializeField] private float movementLockTime = 0.5f;

    private float lock1;
    private float lock2;
    private float lock3;

    private void Update() {
        UpdatePlayerLock(ref lock1);
        UpdatePlayerLock(ref lock2);
        UpdatePlayerLock(ref lock3);

        if (Input.GetKey("a"))
        {
            MovePlayer(true, ref player1, ref lock1);
        }
        if (Input.GetKey("z"))
        {
            MovePlayer(false, ref player1, ref lock1);
        }

        if (Input.GetKey("g"))
        {
            MovePlayer(true, ref player2, ref lock2);
        }
        if (Input.GetKey("b"))
        {
            MovePlayer(false, ref player2, ref lock2);
        }

        if (Input.GetKey("l"))
        {
            MovePlayer(true, ref player3, ref lock3);
        }
        if (Input.GetKey(","))
        {
            MovePlayer(false, ref player3, ref lock3);
        }

        UpdatePodColors(lane1.pod, Globals.LanesEnum.Lane1);
        UpdatePodColors(lane2.pod, Globals.LanesEnum.Lane2);
        UpdatePodColors(lane3.pod, Globals.LanesEnum.Lane3);
    }

    void MovePlayer(bool moveUp, ref Player player, ref float playerLock) {
        if (!isLocked(playerLock)) {
            player.lane = moveUp ? GetPrevLane(player.lane) : GetNextLane(player.lane);
            player.transform.position = GetLanePosition(player.lane);
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
            return lane1.pod.GetPositionForPlayer();
        }
        if (lane == Globals.LanesEnum.Lane2) {
            return lane2.pod.GetPositionForPlayer();
        }
        return lane3.pod.GetPositionForPlayer();
    }

    void UpdatePodColors(DefensePod pod, Globals.LanesEnum laneIndex) {
        pod.ResetColor();
        if (player1.lane == laneIndex) pod.AddColor(player1.color);
        if (player2.lane == laneIndex) pod.AddColor(player2.color);
        if (player3.lane == laneIndex) pod.AddColor(player3.color);
    }
}
