using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterSystem : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float angleSpeed;
    [SerializeField] Vector3 direction;
    [SerializeField] float speed = 5.0f;

    [SerializeField] float mouseX;
    [SerializeField] float health;
    [SerializeField] Camera temporaryCamera;

    void Awake()
    {
        // ���콺 Ŀ�� ��Ȱ��ȭ �ϴ°�
        Cursor.visible = false;


        // ���콺 ��� ����
        Cursor.lockState = CursorLockMode.Locked;

        PhotonNetwork.SerializationRate = 10;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }

    void Start()
    {
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamera.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        //  �� �ڽ��� �ƴ϶�� �Լ��� ��ȯ �Ѵ�.
        if (!photonView.IsMine) return;

        direction = new Vector3
            (
               Input.GetAxisRaw("Horizontal"),
               0,
               Input.GetAxisRaw("Vertical")

               );

        transform.Translate(direction.normalized* speed * Time.deltaTime);
        mouseX += Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bee"))
        {
            PhotonView view = other.gameObject.GetComponent<PhotonView>();
            
            StartCoroutine(temporaryCamera.GetComponent<CameraShake>().Shake(0.5f,0.25f));

            if (view.IsMine)
            {
                health -= 20;

                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }
}
