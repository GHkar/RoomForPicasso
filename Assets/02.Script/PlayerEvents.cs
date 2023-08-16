using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    //ovr 카메라의 anchor 값을 받아옴
    #region Anchors
    public GameObject m_LeftAnchor;
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;

    public static UnityAction OnTriggerUp = null;       //추가
    public static UnityAction OnTriggerDown = null;     //추가

    #endregion
    
    //컨트롤러와 앵커 값 매치 및 last station 유지
    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    //hmd 장착했는지 안했는지에 따른 동작
    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // check for active input //hmd썼는지
        if (!m_InputActive)
            return;

        // check if a controller exists //컨트롤러 인식
        CheckForController();

        // check for input source //컨트롤러 입력이 어디서 오는지
        CheckInputSource();

        //check for actual input //컨트롤러 버튼 클릭 시
        Input();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        // Right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;

        // Left remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;

        //if no controllers, headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) &&
            !OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;


        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    private void CheckInputSource()     //안드로이드 디버그 시 잘 동작하는지 확인하기 위한 debug용
    {
        // left remote
        if(OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.LTrackedRemote))
        {

        }

        // right remote
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.RTrackedRemote))
        {

        }

        // headset //헤드셋에 있는 터치패드
        if (OVRInput.GetDown(OVRInput.Button.Any, OVRInput.Controller.Touchpad))
        {

        }

        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
    }

    private void Input()
    {
        //헤드셋 터치패드
        // touchpad down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadDown != null)
                OnTouchpadDown();
        }

        // touchpad up
        if(OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadUp != null)
                OnTouchpadUp();
        }


        //trigger
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))      //추가
        {
            if (OnTriggerDown != null)
                OnTriggerDown();
        }

        if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))      //추가
        {
            if (OnTriggerUp != null)
                OnTriggerUp();
                
        }
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        // if values are the same, return
        if (check == previous)
            return previous;

        // get controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        // if no controller, set to the head
        if (controllerObject == null)
            controllerObject = m_HeadAnchor;

        // send out event
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        // return new value
        return check;
    }

    private void PlayerFound()
    {
        m_InputActive = true;
    }

    private void PlayerLost()
    {
        m_InputActive = false;
    }

    //컨트롤러와 앵커값 맞추기
    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTrackedRemote, m_LeftAnchor },
            {OVRInput.Controller.RTrackedRemote, m_RightAnchor },
            {OVRInput.Controller.Touchpad, m_HeadAnchor }


        };

        return newSets;
    }
}
