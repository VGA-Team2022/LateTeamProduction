using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 寝ている敵の本体とリアクション（鼻提灯とか）のアニメーションを管理するコンポーネント
/// </summary>
public class SleepingEnemyAnimControle : MonoBehaviour
{
    [SerializeField, Tooltip("寝ている人間のアニメーター")]
    Animator _bodyAnim;
    [SerializeField, Tooltip("リアクションのアニメーター")]
    Animator _reactionAnim;
    /// <summary>
    /// 寝ている敵が起きたときに呼んでください
    /// </summary>
    public void Awaken()
    {
        _bodyAnim.SetTrigger("IsAwake");
        _reactionAnim.SetTrigger("IsAwake");
    }
    /// <summary>
    /// 再度寝る時に呼んでください
    /// </summary>
    public void Sleeping()
    {
        _bodyAnim.SetTrigger("IsSleep");
        _reactionAnim.SetTrigger("IsSleep");
    }
    public void Discover()
    {
        _bodyAnim.SetTrigger("IsDiscover");
        _reactionAnim.SetTrigger("IsDiscover");
    }
}
