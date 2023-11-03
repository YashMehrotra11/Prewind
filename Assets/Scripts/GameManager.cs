using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pastTileset;
    public GameObject presentTileset;
    public GameObject futureTileset;

    public TextMeshProUGUI pastText;
    public TextMeshProUGUI presentText;
    public TextMeshProUGUI futureText;

    public TextMeshProUGUI hint;
    public GameObject exit;

    public static GameManager Instance;
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
        pastTileset.SetActive(true);
        presentTileset.SetActive(false);
        futureTileset.SetActive(false);

        pastText.color = Color.white;
        presentText.color = Color.grey;
        futureText.color = Color.gray;

        hint.gameObject.SetActive(false);
        exit.SetActive(true);
    }

    private void Present()
    {
        pastTileset.SetActive(false);
        presentTileset.SetActive(true);
        futureTileset.SetActive(false);

        pastText.color = Color.gray;
        presentText.color = Color.white;
        futureText.color = Color.gray;

        hint.gameObject.SetActive(true);
        exit.SetActive(false);
    }

    private void Future()
    {
        pastTileset.SetActive(false);
        presentTileset.SetActive(false);
        futureTileset.SetActive(true);

        pastText.color = Color.gray;
        presentText.color = Color.gray;
        futureText.color = Color.white;

        hint.gameObject.SetActive(true);
        exit.SetActive(false);
    }

    public void Rewind()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
