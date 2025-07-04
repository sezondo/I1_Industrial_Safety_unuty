using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public RectTransform unitButtonsPanel; // 유닛 버튼 패널
    public GameObject[] unitStatusPopups;
    public Button menuButton;
    public Button[] unitButtons; // 6개 버튼

    private bool isPanelOpen = false;

    void Start()
    {
        menuButton.onClick.AddListener(ToggleUnitButtonsPanel);

        // 각 유닛 버튼 클릭 시, 해당 팝업 열기
        for (int i = 0; i < unitButtons.Length; i++)
        {
            int idx = i;
            unitButtons[i].onClick.AddListener(() => OpenUnitStatusPopup(idx));
        }
        
    }

    void ToggleUnitButtonsPanel()
    {
        float toX = isPanelOpen ? -1990 : 0;
        unitButtonsPanel.DOAnchorPosX(toX, 0.5f);
        isPanelOpen = !isPanelOpen;
    }

    public void OpenUnitStatusPopup(int idx)
    {
        unitStatusPopups[idx].SetActive(true);
        isPanelOpen = true;
        ToggleUnitButtonsPanel();
    }

    // 각 팝업에서 닫기(X) 함수는 별도 스크립트로 연결!
}
