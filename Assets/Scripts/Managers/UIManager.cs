using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    [SerializeField]
    private Player player;
    [SerializeField]
    private RectTransform interactionUI;

    Dictionary<string, Text> textsByName = new Dictionary<string, Text>();

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
            return;
        }

        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        Text[] texts = interactionUI.GetComponentsInChildren<Text>();
        foreach(Text text in texts)
        {
            if (text == null) continue;

            textsByName.Add(text.name, text);
        }

        SetBestScore(GameManager.Instance.GetBestScore());

        // tmp 무거워서 미리 enable
        interactionUI.gameObject.SetActive(true);
        interactionUI.gameObject.SetActive(false);
    }


    public void SetDescString(string descString)
    {
        Text text;
        textsByName.TryGetValue("DescriptionText", out text);
        if (text == null) return;

        text.text = descString;
    }


    public void SetBestScore(int bestScore)
    {
        Text text;
        textsByName.TryGetValue("BestScoreText", out text);
        if (text == null) return;

        text.text = bestScore.ToString();
    }


    public void PopUpInteractionUI(Vector3 screenPos)
    {
        if (interactionUI == null) return;

        interactionUI.transform.position = screenPos;
        interactionUI.gameObject.SetActive(true);

        
    }

    public void CloseInteractionUI()
    {
        if (interactionUI == null) return;

        interactionUI.gameObject.SetActive(false);
    }
}
