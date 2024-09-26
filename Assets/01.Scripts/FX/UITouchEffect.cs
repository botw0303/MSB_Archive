using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITouchEffect : MonoBehaviour
{
    private EffectObject effectObject;

    [SerializeField]
    private ParticleSystem mouseTrail;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PoolableMono pam = PoolManager.Instance.Pop(PoolingType.TouchEffect);

            if(MaestrOffice.Camera.orthographic)
            {
                pam.transform.position = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
                mouseTrail.Play();
            }
            else
            {
                Vector2 mousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle
                (UIManager.Instance.CanvasTrm, Input.mousePosition, Camera.main, 
                out mousePos);

                pam.transform.localPosition = mousePos;
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            mouseTrail.Stop();
        }
    }
}
