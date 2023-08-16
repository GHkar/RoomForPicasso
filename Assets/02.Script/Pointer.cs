using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    //RaycastHit hit;
    //private bool showDebugRay;
    //private float MaxDistance = 15f;    // ray 최대길이
    //private float DebugRayDuration = 1f;    
    // Start is called before the first frame update
    public float m_distacne = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;          //특정한 레이어로 지정한 오브젝트만 카메라에 노출되도록 포함시키는
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    private Transform m_CurrnetOrigin = null;       //현재좌표
    private GameObject m_CurrentObject = null;      //현재객체

    private void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchpadDown += ProcessTouchpadDown;

        PlayerEvents.OnTriggerDown += ProcessTriggerDown;       //추가
        
    }

    private void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchpadDown -= ProcessTouchpadDown;

        PlayerEvents.OnTriggerDown -= ProcessTriggerDown;       //추가
    }

    private void Start()
    {
        setLineColor();
    }

    // Update is called once per frame
    private void Update()
    {
        //if(Physics.Raycast(transform.position, transform.forward, out hit))     //(ray 원점, ray 방향, 충돌 감지할 raycasthit, ray 거리(길이)) 충돌하면 true 값 반환
        //{

        //}

        //if(showDebugRay)
        //{
        //    Debug.DrawRay(transform.position, transform.forward * MaxDistance, Color.blue, DebugRayDuration);
        //}

        Vector3 hitpoint = UpdateLine();

        m_CurrentObject = updatePointerStatus();        //raycast hit한 객체 리턴

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitpoint, m_CurrentObject);
    }

    private Vector3 UpdateLine()        // 레이저 끝점 반환
    {
        // ray 생성
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        // defalult end
        Vector3 endPosition = m_CurrnetOrigin.position + (m_CurrnetOrigin.forward * m_distacne);

        // check hit
        if (hit.collider != null)
            endPosition = hit.point;

        // set position
        m_LineRenderer.SetPosition(0, m_CurrnetOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);


        return endPosition;
    }

    //컨트롤러 조정
    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)      
    {
        // set origin of pointer
        m_CurrnetOrigin = controllerObject.transform;

        // is the laser visible
        if(controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }
    }

    private GameObject updatePointerStatus()
    {
        //create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        //check hit
        if (hit.collider)
            return hit.collider.gameObject;

        //return
        return null;
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrnetOrigin.position, m_CurrnetOrigin.forward);
        Physics.Raycast(ray, out hit, m_distacne, layer);

        return hit;
    }

    private void setLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0.0f;

        m_LineRenderer.endColor = endColor;
    }

    private void ProcessTouchpadDown()      //터치패드
    {
        if (!m_CurrnetOrigin)
            return;

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }

    private void ProcessTriggerDown()       //추가(트리거)
    {
        if (!m_CurrnetOrigin)
            return;

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }
}
