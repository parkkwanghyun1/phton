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
        // roomCatalog 에 여러개의 value 값이 들어가 있다면 room 인포에 넣어준다

        foreach(RoomInfo info in roomCatalog.Values)
        {
            // 룸을 생성한다.
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));

            //roomContent 하위 오브젝트로 위치를 설정한다.
            room.transform.SetParent(roomContent);

            // 룸 정보를 입력한다.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}
