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
        // ���� ������Ʈ ���� , // �˾� ������Ʈ 
        // prefab ���� ȣ�� �Ͽ� runtime ���� ȣ�� =  instantiate 
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
