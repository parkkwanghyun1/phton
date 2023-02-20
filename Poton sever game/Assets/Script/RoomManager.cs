using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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

    public void OnClickCreateRoom()
    {
        // 룸 옵션을 설정
        RoomOptions Room = new RoomOptions();

        // 최대 접속자의 수를 설정
        Room.MaxPlayers = byte.Parse(roomPerson.text);

        // 룸의 오픈 여부를 설정
        Room.IsOpen = true;

        // 로비에서 룸 목록을 노출 시킬지 설정

        Room.IsVisible = true;

        // 룸을 생성하는 함수

        PhotonNetwork.CreateRoom(roomName.text, Room);
    }

    public void AllDeleteRoom()
    {

        // Transform 오브젝트에 있는 하위 오브젝트에 접근하여 전체 삭제를 시도한다.
        foreach(Transform trans in roomContent)
        {
            // Trnasform 이 가지고 있는 게임 오브젝트를 삭제한다.
            Destroy(trans.gameObject);
        }
    }

    // 해당 로비에 방 목록의 변경 사항이 있을 때 호출 되는 함수이다
    //(방 추가, 삭제, 참가 등 )
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i ++)
        {
            // 해당 이름이 roomCatalog 의 Key 값으로 설정되어 있다면
            if (roomCatalog.ContainsKey(roomList[i].Name))
                {
                // RemoveFromList ; (true) 룸에서 삭제가 되었을 때
                if (roomList[i].RemovedFromList)
                {
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
              }
            roomCatalog[roomList[i].Name]= roomList[i];
        }
    }
}
