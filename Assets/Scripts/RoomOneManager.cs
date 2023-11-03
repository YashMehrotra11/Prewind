using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class RoomOneManager : MonoBehaviour
{
    public TextMeshProUGUI pastText;
    public TextMeshProUGUI presentText;
    public TextMeshProUGUI futureText;

    public TextMeshProUGUI hint;

    public Transform cameraTransform;
    public Transform playerTransform;

    public static RoomOneManager Instance;
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

        hint.gameObject.SetActive(false);

        playerTransform.position = new Vector2(-20, 0.5f);
        cameraTransform.position = new Vector3(-20, cameraTransform.position.y, cameraTransform.position.z);
    }

    private void Present()
    {
        pastText.color = Color.gray;
        presentText.color = Color.white;
        futureText.color = Color.gray;

        hint.gameObject.SetActive(true);

        playerTransform.position = new Vector2(0, 0.5f);
        cameraTransform.position = new Vector3(0, cameraTransform.position.y, cameraTransform.position.z);
    }

    private void Future()
    {
        pastText.color = Color.gray;
        presentText.color = Color.gray;
        futureText.color = Color.white;

        hint.gameObject.SetActive(true);

        playerTransform.position = new Vector2(20, 0.5f);
        cameraTransform.position = new Vector3(20, cameraTransform.position.y, cameraTransform.position.z);
    }

    public void Rewind()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
