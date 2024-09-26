using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BattleTutorial : Stage
{
    [SerializeField] private EpisodeData _battleTutorialSO;
    private List<Action> tutorialActionList;

    [Header("Ʃ�丮�� �̺�Ʈ")]
    public UnityEvent onTutorialStartTrigger;
    public UnityEvent onTutorialEndTrigger;

    [Header("�б� �̺�Ʈ")]
    public Action onQuaterStartTrigger = null;
    public Action onQuaterEndTrigger = null;

    [Header("����׿� ����")]
    //public SimpleEnemy dummyEnemyPrefab;
    public CinemachineVirtualCamera enemyZoomCam;

    [Header("Ʈ����")]
    [SerializeField] private TutorialTriggerObject _moveTrigger;
    [SerializeField] private TutorialTriggerObject _jumpTrigger;
    [SerializeField] private TutorialTriggerObject _dashTrigger;

    //private SimpleEnemy _curEnemy;

    protected override void Awake()
    {
        base.Awake();

        tutorialActionList = new List<Action>();

        #region Add Action to List
        
        tutorialActionList.Add(MoveTutorial);
        tutorialActionList.Add(JumpTutorial);
        tutorialActionList.Add(DashTutorial);
        tutorialActionList.Add(AttackTutorial);
        tutorialActionList.Add(TutorialEnd);

        #endregion

        onQuaterEndTrigger += QuaterStart;

        
        TutorialStart();
    }

    public void TutorialStart()
    {
        //EpisodeManager.Instanace.StartEpisode();
        //EpisodeManager.Instanace.EpisodeEndEvent += QuaterStart;

        QuaterStart();
        int[] pauseIdx = { 14, 18, 22, 28, 36 };
        //EpisodeManager.Instanace.StartEpisode(_battleTutorialSO, pauseIdx);

        //��ȭâ�� ���ͼ� ������ �Ѵ�.
        //��ȭâ�� ���� ��쿡�� �б� ������ �˸���.
        //������ �׵��� ������ �����ϱ� �׳� ����� ������ �Է¹޾� �Ѱ��ִ� ģ�� �ϳ��� �ȴ�.
    }


    public void TutorialEnd()
    {
        //EpisodeManager.Instanace.EpisodeEndEvent -= QuaterStart;

        //Ʃ�丮���� ������ ��쿡�� ���Ǽҵ尡 ����Ǿ��� ���� �κ�� �����־���Ѵ�.

        onTutorialEndTrigger?.Invoke();

        SceneManager.LoadScene("SampleScene");
    }

    //�бⰡ ���۵Ǿ��� ������ �غ��� �̺�Ʈ�� �����Ѵ�.
    //1. �����̴� �� ������ �����̴� ����� �˷��ش�.
    //2. ���� ����� �˷��ش�.
    //3. ���� ���´�. ���� ������� ���� �˷��ش�.
    //4. �⺻ �������δ� ���� �ʴ� ���� ���´�.
    //5. �⺻ �����Ǵ� �˱� ��ų�� ����� �� �ֵ��� �������ش�.
    //6. ���ư���?

    //�бⰡ ���۵Ǿ��� ������ ��ȭâ���� ���;��Ѵ�.
    //��ȭâ�� Ʃ�丮�� ������ ���� �� ����� �Ѿ�� ���� ���´ٰ� �� �� �ִ�.
    private void QuaterStart()
    {
        onQuaterStartTrigger = null;
        onQuaterStartTrigger += tutorialActionList[CurPhase];
        Debug.Log(CurPhase);
        onQuaterStartTrigger?.Invoke();

        //if (curPhase >= 1)
        //{
        //    onQuaterStartTrigger -= tutorialActionList[curPhase - 1];
        //    onQuaterStartTrigger += tutorialActionList[curPhase];
        //}
        //else
        //{
        //    onQuaterStartTrigger -= tutorialActionList[0];
        //    onQuaterStartTrigger += tutorialActionList[1];
        //}
    }

    public void QuaterEnd(string message)
    {
        print(message);

        //EpisodeManager.Instanace.SetPauseEpisode(false);
        onQuaterEndTrigger?.Invoke();
    }

    #region Tutorial Actions

    private void MoveTutorial()
    {
        Debug.Log("Let's move");

        StartCoroutine(DebugInputCoroutine("��������!", () => _moveTrigger.playerIsInTriggered));
    }

    private void JumpTutorial()
    {
        Debug.Log("Let's jump");

        StartCoroutine(DebugInputCoroutine("����!", () => _jumpTrigger.playerIsInTriggered));
    }

    private void AttackTutorial()
    {
        Debug.Log("Let's attack");

        //_curEnemy = Instantiate(dummyEnemyPrefab);
        //_curEnemy.transform.position = new Vector3(81f, -3.13f, 0);

        StartCoroutine(ZoomToEnemy());
    }

    private void DashTutorial()
    {
        StartCoroutine(DebugInputCoroutine("����!", () => _dashTrigger.playerIsInTriggered));
    }

    private IEnumerator ZoomToEnemy()
    {
        int front = 15;
        int behind = 10;

        //GameManager.Instance.Player.PlayerInput._controls.Player.Disable();

        enemyZoomCam.m_Priority = front;
        vCam.m_Priority = behind;

        yield return new WaitUntil(() => !Camera.main.GetComponent<CinemachineBrain>().IsBlending);
        yield return new WaitForSeconds(2.0f);

        enemyZoomCam.m_Priority = behind;
        vCam.m_Priority = front;

        yield return new WaitUntil(() => !Camera.main.GetComponent<CinemachineBrain>().IsBlending);

        //GameManager.Instance.Player.PlayerInput._controls.Player.Disable();
        //_curEnemy.FreezeTime(false);
        //StartCoroutine(DebugInputCoroutine("���� �����߷���!", () =>
        //{
        //    print(_curEnemy.transform.GetComponent<Health>().isDead);
        //    bool dead = _curEnemy.transform.GetComponent<Health>().isDead;
        //    if (dead)
        //    {
        //        _curEnemy = null;
        //    }
        //    return dead;
        //}, true));
    }
    #endregion

    private IEnumerator DebugInputCoroutine(string message, Func<bool> action, bool phaseClear = true)
    {
        Debug.Log(message);
        //GameManager.Instance.Player.PlayerInput._controls.Player.Disable();

        //yield return new WaitUntil(() => EpisodeManager.Instanace.isInPause);

        //GameManager.Instance.Player.PlayerInput._controls.Player.Enable();

        yield return new WaitUntil(()=> action.Invoke());
        Debug.Log("clear");
        if(phaseClear) CurPhaseCleared = true;
    }
}
