using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public Vector3 RandomPosition(float value)
    {
        // 게임 오브젝트를 중심으로 기준 반지름 원을 설정한다
        float radius = value;

        // 첫 번째로 x 값을 계산한다.
        float x = Random.Range(-radius, radius);

        // z 값은 방정식으로 계산한다
        float z = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x, 2));

        // Random.Range 로 - 와 + 의 랜덤 부호를 설정 한다
        if (Random.Range(0, 2) == 0)
        {
            z = -z;
        }

        return new Vector3(x, 1, z);
    }

    IEnumerator Spawn(string name, float radius)
    {
        while (true)
        {
            PhotonNetwork.Instantiate
                (
                name,
                RandomPosition(radius),
                Quaternion.identity
                );

            yield return new WaitForSeconds(5);
        }
    }

    private void Awake()
    {
        PhotonNetwork.Instantiate
              (
               "Character",
              RandomPosition(10),
              Quaternion.identity
              );
    }
    void Start()
    {
        // 마스터 클라이언트만 
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(Spawn("Bee", 100));
        }
    }

    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }
}
