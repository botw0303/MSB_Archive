using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ExplosionScreen : MonoBehaviour
{
    //����׿� ��ũ��Ʈ�Դϴ�. ����� �ϼ��Ǿ� ������ ������ ���ŵ� �����մϴ�.
    private Rigidbody[] _explosionRbs;

    [Header("Objs")]
    [SerializeField] private GameObject _renderCam;
    [SerializeField] private GameObject _shatterEffectObj;
    [SerializeField] private Transform _explosionTrm;
    [SerializeField] private ParticleSystem _swordScreenEffect;

    [Header("explosion status")]
    [SerializeField] private float _explosionPow;
    [SerializeField] private float _explosionRad;

    [Header("Timing")]
    [SerializeField] private float _delayTime;
    [SerializeField] private float _destroyTime;

    private void Update()
    {
        //����� ��ǲ�Դϴ�.
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartCoroutine(Shatter());
        }
    }

    private IEnumerator Shatter()
    {
        //���� �ؽ��Ŀ� �˱� ������ �� �� �����Ӹ� ��� �뵵�� �ڵ��Դϴ�.
        _renderCam.SetActive(true);
        yield return new WaitForEndOfFrame();
        _renderCam.SetActive(false);

        //����Ʈ ����
        _swordScreenEffect.Play();
        yield return new WaitForSeconds(_delayTime);

        //shatter Effect Object ����
        var obj = Instantiate(_shatterEffectObj, _renderCam.transform.position, Quaternion.identity);
        obj.transform.position += new Vector3(0,0,5);
        obj.transform.rotation = Quaternion.Euler(0, 0, 180);

        //������Ʈ �ڽĵ鿡 �ִ� Rigidbody���� �����ͼ� ExplosionForce���ݴϴ�.
        _explosionRbs = obj.GetComponentsInChildren<Rigidbody>();
        foreach(var rb in _explosionRbs)
        {
            rb.AddExplosionForce(_explosionPow, _explosionTrm.position, _explosionRad);
        }

        yield return new WaitForSeconds(_destroyTime);

        Destroy(obj);
    }
}
