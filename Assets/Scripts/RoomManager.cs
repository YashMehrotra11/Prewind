using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RoomManager : MonoBehaviour
{
    public TextMeshProUGUI pastText;
    public TextMeshProUGUI presentText;
    public TextMeshProUGUI futureText;

    public TextMeshProUGUI pastHintText;
    public TextMeshProUGUI presentHintText;
    public TextMeshProUGUI futureHintText;

    public Transform cameraTransform;
    public Transform playerTransform;

    public static RoomManager Instance;
    private void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Present();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Past();
        }
        if (Input.GetKey(KeyCode.C))
        {
            Present();
        }
        if (Input.GetKey(KeyCode.V))
        {
            Future();
        }
        if (Input.GetKey(KeyCode.R))
        {
            Rewind();
        }
    }

    private void Past()
    {
        pastText.color = Color.white;
        presentText.color = Color.grey;
        futureText.color = Color.gray;

        pastHintText.gameObject.SetActive(true);
        presentHintText.gameObject.SetActive(false);
        futureHintText.gameObject.SetActive(false);

        Vector3 playerOffset = playerTransform.position - cameraTransform.position;
        cameraTransform.position = new Vector3(-20, cameraTransform.position.y, cameraTransform.position.z);
        playerTransform.position = cameraTransform.position + playerOffset;
    }

    private void Present()
    {
        pastText.color = Color.gray;
        presentText.color = Color.white;
        futureText.color = Color.gray;

        pastHintText.gameObject.SetActive(false);
        presentHintText.gameObject.SetActive(true);
        futureHintText.gameObject.SetActive(false);

        Vector3 playerOffset = playerTransform.position - cameraTransform.position;
        cameraTransform.position = new Vector3(0, cameraTransform.position.y, cameraTransform.position.z);
        playerTransform.position = cameraTransform.position + playerOffset;
    }

    private void Future()
    {
        pastText.color = Color.gray;
        presentText.color = Color.gray;
        futureText.color = Color.white;

        pastHintText.gameObject.SetActive(false);
        presentHintText.gameObject.SetActive(false);
        futureHintText.gameObject.SetActive(true);

        Vector3 playerOffset = playerTransform.position - cameraTransform.position;
        cameraTransform.position = new Vector3(20, cameraTransform.position.y, cameraTransform.position.z);
        playerTransform.position = cameraTransform.position + playerOffset;
    }

    public void Rewind()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
