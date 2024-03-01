using GameCreator.Runtime.Characters;
using System;
using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private string _requestType;
    [SerializeField] private Character _player;
    [SerializeField] private float _registerInterval = 0.25f;
    [SerializeField] private float _deadValue = 0.01f;

    private void Start()
    {
        StartCoroutine(RegisterMovement());
    }

    private IEnumerator RegisterMovement()
    {
        Vector3 lastPosition;
        while (true)
        {
            lastPosition = _player.transform.position;
            yield return new WaitForSecondsRealtime(_registerInterval);
            Vector3 currentPosition = _player.transform.position;
            float distance = Vector3.Distance(currentPosition, lastPosition);
            if (distance > _deadValue)
            {
                SendRegisterMovement(distance);
            }
        }
    }

    private void SendRegisterMovement(float moveDistance)
    {
        ServerSender.Instance.Send(new MovementRequest(PlayerProfileController.Instance.PlayerIdentity, moveDistance), _requestType, OnReceive);
    }

    private void OnReceive(ResponseObject response)
    {
        if (response.CodeStatus == 1)
        {
            // ban handle;
        }
    }

    [Serializable]
    private class MovementRequest
    {
        public PlayerIdentity PlayerIdentity;
        public float MoveDistance;

        public MovementRequest(PlayerIdentity playerIdentity, float moveDistance)
        {
            PlayerIdentity = playerIdentity;
            MoveDistance = moveDistance;
        }
    } 
}
