using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotificationManager : MonoBehaviour
{

    [SerializeField] Text title;
    [SerializeField] Text content;

    public static NotificationManager NotificationWindow(string titleMessage, string contentMessage)
    {
        // 게임 오브젝트 생성 , // 팝업 오브젝트 
        // prefab 에서 호출 하여 runtime 으로 호출 =  instantiate 
        GameObject notification = Instantiate(Resources.Load<GameObject>("NotificationWindow"));

        NotificationManager resultWindow = notification.GetComponent<NotificationManager>();

        resultWindow.title.text = titleMessage;
        resultWindow.content.text = titleMessage;

        return resultWindow;
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
