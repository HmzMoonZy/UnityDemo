using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ClipName
{
    /// <summary>
    /// 野猪攻击音效.
    /// </summary>
    BoarAttack,
    /// <summary>
    /// 野猪死亡音效.
    /// </summary>
    BoarDeath,
    /// <summary>
    /// 野猪受伤音效.
    /// </summary>
    BoarInjured,
    /// <summary>
    /// 野人攻击音效.
    /// </summary>
    ZombieAttack,
    /// <summary>
    /// 野人死亡音效.
    /// </summary>
    ZombieDeath,
    /// <summary>
    /// 野人受伤音效.
    /// </summary>
    ZombieInjured,
    /// <summary>
    /// 野人尖叫音效.
    /// </summary>
    ZombieScream,
    /// <summary>
    /// 子弹命中地面音效.
    /// </summary>
    BulletImpactDirt,
    /// <summary>
    /// 子弹命中身体音效.
    /// </summary>
    BulletImpactFlesh,
    /// <summary>
    /// 子弹命中金属音效.
    /// </summary>
    BulletImpactMetal,
    /// <summary>
    /// 子弹命中石头音效.
    /// </summary>
    BulletImpactStone,
    /// <summary>
    /// 子弹命中木材音效.
    /// </summary>
    BulletImpactWood,
    /// <summary>
    /// 玩家角色急促呼吸声.
    /// </summary>
    PlayerBreathingHeavy,
    /// <summary>
    /// 玩家角色受伤音效.
    /// </summary>
    PlayerHurt,
    /// <summary>
    /// 玩家角色死亡音效.
    /// </summary>
    PlayerDeath,
    /// <summary>
    /// 身体命中音效.
    /// </summary>
    BodyHit
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager _Instance;

    private AudioClip[] m_audioClips;       //用于遍历音频文件
    private Dictionary<string, AudioClip> m_audioClipDic;
    private void Awake()
    {
        _Instance = this;

        m_audioClips = Resources.LoadAll<AudioClip>("Audio/Enemy/");
        m_audioClipDic = new Dictionary<string, AudioClip>();

        for (int i = 0; i < m_audioClips.Length; i++)
        {
            m_audioClipDic.Add(m_audioClips[i].name, m_audioClips[i]);
        }

    }
    /// <summary>
    /// 通过音频文件名获取音频文件
    /// </summary>
    public AudioClip GetAudioClipByName(ClipName name)
    {
        AudioClip temp = null;
        m_audioClipDic.TryGetValue(name.ToString(), out temp);
        return temp;
    }
    /// <summary>
    /// 在vector播放名为fileName的音频文件
    /// </summary>
    public void PlayAudioAtPos(ClipName name, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(GetAudioClipByName(name), pos);
    }
    /// <summary>
    /// 为一个游戏物体添加音频组件并播放音频文件
    /// </summary>
    public AudioSource PlayAudioFormComponent(GameObject gobj, ClipName name, bool auto = true, bool loop = false)
    {
        AudioSource temp_as = gobj.AddComponent<AudioSource>();
        temp_as.clip = GetAudioClipByName(name);
        temp_as.playOnAwake = auto;
        if (auto) temp_as.Play();
        temp_as.loop = loop;

        return temp_as;
    }
}
