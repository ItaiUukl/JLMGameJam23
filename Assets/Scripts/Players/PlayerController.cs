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

        UsePlayerKeyPair(ref player1, ref lock1, "a", "z");
        UsePlayerKeyPair(ref player2, ref lock2, "g", "b");
        UsePlayerKeyPair(ref player3, ref lock3, "l", ",");
        
        UpdatePodColors(lane1.pod, Globals.LanesEnum.Lane1);
        UpdatePodColors(lane2.pod, Globals.LanesEnum.Lane2);
        UpdatePodColors(lane3.pod, Globals.LanesEnum.Lane3);
    }

    void UsePlayerKeyPair(ref Player player, ref float playerLock, string upKey, string downKey)
    {
        if (Input.GetKeyUp(upKey))
        {
            UnlockPlayer(ref playerLock);
        }
        else if (Input.GetKey(upKey))
        {
            MovePlayer(true, ref player, ref playerLock);
        }
        if (Input.GetKeyUp(downKey))
        {
            UnlockPlayer(ref playerLock);
        }
        else if (Input.GetKey(downKey))
        {
            MovePlayer(false, ref player, ref playerLock);
        }
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

    void UnlockPlayer(ref float playerLock) {
        playerLock = 0;
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
