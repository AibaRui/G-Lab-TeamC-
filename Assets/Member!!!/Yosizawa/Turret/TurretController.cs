using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurretController : GimickBase
{
    [SerializeField, Tooltip("�ˏo����Q�[���I�u�W�F�N�g")]
    private GameObject[] _shell = default;
    [SerializeField, Range(15f, 75f), Tooltip("�ˏo����p�x")]
    private float _angle = 45f;
    [SerializeField, Tooltip("�C��̎˒�����")]
    private float _range = 1f;
    [SerializeField, Tooltip("�ˏo����Ԋu")]
    private float _interval = 1f;
    /// <summary>��������Ԋu���J�E���g����</summary>
    private float _timer = 0f;
    [SerializeField, Tooltip("�U���Ώ�")]
    private Transform[] _terget;
    /// <summary>���݁APause�����ǂ����𔻒肷��t���O</summary>
    private bool _isPause = true;

    private void Update()
    {
        if(_isPause)
        {
            _timer += Time.deltaTime;
            if (_timer >= _interval)
            {
                ThrowingShell();
                _timer = 0;
            }
        }
    }

    /// <summary>�e���ˏo���郁�\�b�h</summary>
    private void ThrowingShell()
    {
        if (_shell.Length > 0 && _terget.Length > 0)
        {
            //�U���Ώۂ̒����烉���_���őI�΂ꂽ�^�[�Q�b�g��_��
            int number = Random.Range(0, _terget.Length);

            //�e�̒����烉���_���őI�΂ꂽ���̂��ˏo����
            int random = Random.Range(0, _shell.Length);
            
            //�ˏo���x���v�Z����
            Vector2 velocity = CalculateVelocity(transform.position, _terget[number].position, _angle);

            if(velocity != Vector2.zero)
            {
                //�e�𐶐�����
                GameObject shell = Instantiate(_shell[random], transform.position, Quaternion.identity);

                //�e��Rigidbody2D���A�^�b�`����Ă��邱�Ƃ��m�񂷂�
                Rigidbody2D shellRb;
                if (shell.TryGetComponent(out Rigidbody2D rb)) shellRb = rb;
                else shellRb = shell.AddComponent<Rigidbody2D>();

                //Player�̂�����W�ɒe���ˏo����
                shellRb.AddForce(velocity * shellRb.mass, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("��������I�u�W�F�N�g or �U���Ώ� ��assign����Ă��܂���B");
        }
    }

    /// <summary>�ˏo����Ƃ��̏����x�����߂郁�\�b�h</summary>
    /// <param name="muzzle">�C��</param>
    /// <param name="terget">�^�[�Q�b�g�n�_</param>
    /// <param name="angle">�ˏo�p�x</param>
    /// <returns>�v�Z�ɂ�苁�߂�ꂽ�����x</returns>
    private Vector2 CalculateVelocity(Vector2 muzzle, Vector2 terget, float angle)
    {
        //�ʓx�@��x���@�ɕϊ�
        float rad = angle * Mathf.PI / 180;

        //���������̋��������߂�
        float x = Vector2.Distance(new Vector2(muzzle.x, 0), new Vector2(terget.x, 0));

        //���������̋��������߂�
        float y = muzzle.y - terget.y;

        //�Ε����˂̎��������x�ɂ��ĉ���
        float speed = Mathf.Sqrt(-Physics2D.gravity.y * x * x / 
            (2 * Mathf.Cos(rad) * Mathf.Cos(rad) * (x * Mathf.Tan(rad) + y)));

        //speed�̌v�Z���ʂ������l�łȂ��� or �˒��͈͊O�̎��́A�����x���O�ɂ���B
        if (float.IsNaN(speed) || x > _range)
        {
            return Vector2.zero;
        }
        else
        {
            return (new Vector2(terget.x - muzzle.x, x * Mathf.Tan(rad)).normalized * speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    public override void GameOverPause()
    {
        _isPause = false;
    }

    public override void Pause()
    {
        _isPause = false;
    }

    public override void Resume()
    {
        _isPause = true;
    }
}
