using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreate;
    public InputField roomName;
    public InputField roomPerson;
    public Transform roomContent;

    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();

    // Update is called once per frame
    void Update()
    {
        if (roomName.text.Length > 0 && roomPerson.text.Length >0 )
        {
            roomCreate.interactable = true;

        }
        else
        {
            roomCreate.interactable = false;
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // roomCatalog �� �������� value ���� �� �ִٸ� room ������ �־��ش�

        foreach(RoomInfo info in roomCatalog.Values)
        {
            // ���� �����Ѵ�.
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            //roomContent ���� ������Ʈ�� ��ġ�� �����Ѵ�.
            room.transform.SetParent(roomContent);

            // �� ������ �Է��Ѵ�.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
