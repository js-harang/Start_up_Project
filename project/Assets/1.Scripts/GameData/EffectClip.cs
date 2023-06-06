using UnityEngine;

/// <summary>
/// 이펙트 프리팹과 경로, 타입 등의 속성 데이터를 가짐
/// 프리팹 사전로딩, 풀링을 위한 기능
/// 이펙트 인스턴스 기능 - 풀링과 연계해서 사용하기도 함
/// </summary>
public class EffectClip
{
    // 속성은 같지만 다른 이펙트 클립을 분별하기 위한 용도
    public int realId = 0;
    public EffectType effectType = EffectType.NORMAL;
    public GameObject effectPrefab = null;
    public string effectName = string.Empty;
    public string effectPath = string.Empty;
    public string effectFullPath = string.Empty;
    public EffectClip() { }

    public void PreLoad()
    {
        this.effectFullPath = effectPath + effectName;

        if (this.effectFullPath != string.Empty && this.effectPrefab == null)
        {
            this.effectPrefab = ResourceManager.Load(effectFullPath) as GameObject;
        }
    }

    public void ReleaseEffect()
    {
        if (this.effectPrefab != null)
        {
            this.effectPrefab = null;
        }
    }

    /// <summary>
    /// 원하는 위치에 내가 원하는 이펙트를 인스턴스
    /// </summary>
    public GameObject Instantiate(Vector3 Pos)
    {
        if (this.effectPrefab == null)
        {
            this.PreLoad();
        }

        if (this.effectPrefab != null)
        {
            GameObject effect = GameObject.Instantiate(effectPrefab, Pos, Quaternion.identity);
            return effect;
        }

        return null;
    }
}
