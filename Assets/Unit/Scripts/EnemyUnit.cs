using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Interface;

public class EnemyUnit : UnitBase
{
    [SerializeField] public IAttackable defaultTarget;//�v���C���[�̖{���n

    protected override void Awake()
    {
        maxHealth = 150f;

        attackPower = 20f;
        defensePower = 5f;

        TeamId = Team.Enemy;

        base.Awake(); // UnitBase �� Awake ���Ăяo��
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable unit = other.GetComponent<IAttackable>();
        if (unit != null && unit.TeamId != TeamId)
        {
            SetTarget(unit);
            ChangeState(UnitState.Combat);
        }
    }

    protected override void FixedUpdate()
    {
        if (attackTarget == null)
        {
            SearchForNewTarget();
        }

        base.FixedUpdate();
    }

    public float searchRadius = 10f;
    public LayerMask targetLayer; // �U���Ώۂ̃��C���[�i��FBuilding, Player�Ȃǁj
    void SearchForNewTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, searchRadius, targetLayer);

        // IAttackable�������A���G�`�[���̂��̂�T��
        var candidates = hits
            .Select(h => h.GetComponent<IAttackable>())
            .Where(a => a != null && a.TeamId != TeamId)
            .ToList();

        if (candidates.Count == 0)
        {
            SetTarget(defaultTarget);
            Debug.Log($"�V�����^�[�Q�b�g��ݒ�: {attackTarget}");
            return;
        }

        // �ł��߂��^�[�Q�b�g��I��
        var newTarget = candidates
            .OrderBy(a => Vector3.Distance(transform.position, ((MonoBehaviour)a).transform.position))
            .First();
        SetTarget(newTarget);
    }

}
