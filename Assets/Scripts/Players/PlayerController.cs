using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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

    private void Start() {
        player1.lane = lane1;
        player2.lane = lane2;
        player3.lane = lane3;
        player1.transform.position = GetLanePosition(player1.lane);
        player2.transform.position = GetLanePosition(player2.lane);
        player3.transform.position = GetLanePosition(player3.lane);
    }

    private void Update() {
        UpdatePlayerLock(ref lock1);
        UpdatePlayerLock(ref lock2);
        UpdatePlayerLock(ref lock3);

        UsePlayerKeyPair(ref player1, ref lock1, "a", "z");
        UsePlayerKeyPair(ref player2, ref lock2, "g", "b");
        UsePlayerKeyPair(ref player3, ref lock3, "l", ",");
    }

    void UsePlayerKeyPair(ref Player player, ref float playerLock, string upKey, string downKey)
    {
        if (!player.isActiveAndEnabled) {
            return;
        }
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

            UpdatePodColors(lane1);
            UpdatePodColors(lane2);
            UpdatePodColors(lane3);
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

    Lane GetPrevLane(Lane lane) {
        var newIdx = (Globals.LanesEnum)Math.Max((int)lane.laneIndex - 1, (int)Globals.LanesEnum.Lane1);
        return GetLaneByIndex(newIdx);
    }

    Lane GetNextLane(Lane lane) {
        var newIdx = (Globals.LanesEnum)Math.Min((int)lane.laneIndex + 1, (int)Globals.LanesEnum.Lane3);
        return GetLaneByIndex(newIdx);
    }

    Lane GetLaneByIndex(Globals.LanesEnum idx) {
        if (lane1.laneIndex == idx) return lane1;
        if (lane2.laneIndex == idx) return lane2;
        if (lane3.laneIndex == idx) return lane3;
        throw new Exception("Could not find lane with index " + idx);
    }
    
    Vector3 GetLanePosition(Lane lane) {
        return lane.pod.GetPositionForPlayer();
    }

    void UpdatePodColors(Lane lane) {
        lane.pod.ResetColor();
        if (player1.isActiveAndEnabled && player1.lane == lane) lane.pod.AddColor(player1.color);
        if (player2.isActiveAndEnabled && player2.lane == lane) lane.pod.AddColor(player2.color);
        if (player3.isActiveAndEnabled && player3.lane == lane) lane.pod.AddColor(player3.color);
    }

    public List<Player> GetPlayersInLane(Lane lane) {
        var lst = new List<Player>();
        if (player1.lane == lane) lst.Add(player1);
        if (player2.lane == lane) lst.Add(player2);
        if (player3.lane == lane) lst.Add(player3);

        return lst;
    }

    public void OnPlayerDamage(Player player) {
        if (player.Life == 0) {
            player.gameObject.SetActive(false);
            Debug.Log(player.color + ": I'm a goner :(");
            return;
        }
        player.Life -= 1;
        Debug.Log(player.color + ": Ouch");
    }
}
