using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionScript : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        UpdateLevelButtons();
    }

    private void UpdateLevelButtons()
    {
        int unlockedLevel = GameController.instance.GetLevel();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= unlockedLevel)
            {
                SetChildrenOpacity(levelButtons[i], 1f);
                levelButtons[i].interactable = true;
            }
            else
            {
                SetChildrenOpacity(levelButtons[i], 0.5f);
                levelButtons[i].interactable = false;
            }
        }
    }

    private void SetChildrenOpacity(Button button, float alpha)
    {
        foreach (Graphic graphic in button.GetComponentsInChildren<Graphic>())
        {
            if (graphic.gameObject != button.gameObject)
            {
                Color graphicColor = graphic.color;
                graphic.color = new Color(graphicColor.r, graphicColor.g, graphicColor.b, alpha);
            }
        }
    }
}
