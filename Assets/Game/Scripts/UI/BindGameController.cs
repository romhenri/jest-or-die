using UnityEngine;
using UnityEngine.UI;

public class BindGameController : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError($"No Button component found on {gameObject.name}. Please add a Button component or attach this script to a GameObject with a Button.");
            return;
        }

        if (GameController.instance != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(GameController.instance.ToggleSounds);
        }
        // TODO: Entender pq funciona com esse Warning.
        //else
        //{
        //    Debug.LogWarning("GameController instance not found.");
        //}
    }
}
