using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceUISwitcher : MonoBehaviour
{
    public GameObject keyboardHint;    // 키보드 안내 텍스트
    public GameObject gamepadHint;     // 게임패드 안내 텍스트
    public CanvasGroup touchControls;  // 가상 조이스틱 그룹 (Canvas Group으로 숨김 — 오브젝트는 끌지 않음)

    PlayerInput playerInput;

    //void Awake()
    //{
        //playerInput = GetComponent<PlayerInput>();
        //playerInput.onControlsChanged += OnControlsChanged;   // 스킴이 바뀔 때
    //}

    void OnEnable()  
    { 
        if (playerInput == null) playerInput = GetComponent<PlayerInput>();   
        // 재컴파일 뒤에도 다시 채움
        playerInput.onControlsChanged += OnControlsChanged;                     
        // 스킴이 바뀔 때
        InputSystem.onDeviceChange += OnDeviceChange;                           
        // 장치가 생기거나 사라질 때
    }
    void OnDisable() 
    { 
        if (playerInput != null) playerInput.onControlsChanged -= OnControlsChanged;
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void Start() { Refresh(); }   // Player Input이 스킴을 정한 뒤 첫 갱신

    void OnControlsChanged(PlayerInput input) { Refresh(); }
    void OnDeviceChange(InputDevice device, InputDeviceChange change) { Refresh(); }

    void Refresh()
    {
        if (playerInput == null || keyboardHint == null || gamepadHint == null || touchControls == null) return;   // 종료 중 파괴된 오브젝트 접근 방지
        string scheme = playerInput.currentControlScheme;
        bool hasTouch = Touchscreen.current != null;   // 폰·Touch Simulation이면 true, PC 빌드면 false

        keyboardHint.SetActive(scheme == "Keyboard&Mouse");
        gamepadHint.SetActive(scheme == "Gamepad" && !hasTouch);   // 가상 스틱 드래그로 생기는 Gamepad에는 안 띄움

        // 터치스크린 장치가 있으면 조작부를 항상 보인다 (S40: 스킴이 아니라 플랫폼으로 판단)
        touchControls.alpha = hasTouch ? 1f : 0f;
        touchControls.blocksRaycasts = hasTouch;
        touchControls.interactable = hasTouch;

        Debug.Log("활성 스킴: " + scheme + " / 터치스크린: " + hasTouch);
    }
}