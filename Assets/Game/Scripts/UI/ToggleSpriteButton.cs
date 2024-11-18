using UnityEngine;
using UnityEngine.UI;

public class ToggleSpriteButton : MonoBehaviour
{
    public Sprite trueSprite;
    public Sprite falseSprite;

    private Image buttonImage;
    private bool isTrue = true;

    void Start()
    {
        isTrue = GameController.instance.GetSoundsEnabled();

        buttonImage = GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogError("Image não encontrado neste GameObject.");
        }

        UpdateSprite();
    }

    public void ToggleState()
    {
        isTrue = !isTrue;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = isTrue ? trueSprite : falseSprite;
        }
    }
}
