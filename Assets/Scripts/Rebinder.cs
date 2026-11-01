using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Rebinder : MonoBehaviour
{
    public PlayerMove player;      // 실습 3의 스크립트
    public TMP_Text buttonLabel;

    const string SaveKey = "rebinds";

    void Start()
    {
        // 이전에 저장한 리바인딩이 있으면 불러온다
        string saved = PlayerPrefs.GetString(SaveKey, "");
        if (!string.IsNullOrEmpty(saved))
            player.Controls.LoadBindingOverridesFromJson(saved);
        UpdateLabel();
    }

    public void StartRebind()
    {
        var action = player.Controls.Player.Interact;
        buttonLabel.text = "키를 누르세요…";

        action.Disable();
        action.PerformInteractiveRebinding()
              .WithControlsExcluding("Mouse")
              .OnComplete(op =>
              {
                  op.Dispose();
                  action.Enable();
                  PlayerPrefs.SetString(SaveKey,
                      player.Controls.SaveBindingOverridesAsJson());
                  UpdateLabel();
              })
              .Start();
    }

    void UpdateLabel()
    {
        buttonLabel.text = "Interact: " +
            player.Controls.Player.Interact.GetBindingDisplayString();
    }
}