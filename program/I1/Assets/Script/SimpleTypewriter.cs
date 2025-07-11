using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleTypewriter : MonoBehaviour
{

    public Text infoText;
    [TextArea] public string infoFullText; // Inspector에서 편집
    public float typeSpeed = 0.5f;
    //public Button exisButton; //이거 낼 정하기
    private Coroutine typingCoroutine;

    public AudioClip TTSSound;
    public AudioSource TTSSoundSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartTyping()
    {
        infoText.text = "";  // 패널이 켜질 때 텍스트 비우고
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText());
        TTSSoundSource.PlayOneShot(TTSSound);
    }

    void Start()
    {
        //exisButton.onClick.AddListener(ClosePopup);
        
    }

    // Update is called once per frame
    IEnumerator TypeText()
    {
        foreach (char c in infoFullText)
        {
            infoText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void ClosePopup()
    {
        gameObject.SetActive(false);   // 팝업(=PlayerRobot) 비활성화
    }
}
