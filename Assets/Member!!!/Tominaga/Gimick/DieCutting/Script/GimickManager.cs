using UnityEngine;

public class GimickManager : MonoBehaviour
{
    [SerializeField, Tooltip("本フラグ有効時、他のオブジェクトのギミックを起動する")]
    public bool is_complete_ = false;
    [SerializeField]
    private GameObject die_cuttting_;
    private DieCutting dc_;
    void Start()
    {
        dc_ = die_cuttting_.GetComponent<DieCutting>();
    }

    void Update()
    {
        if (dc_.Is_complete) { is_complete_ = true; }
    }
}
