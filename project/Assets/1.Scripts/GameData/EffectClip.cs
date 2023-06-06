using UnityEngine;

/// <summary>
/// ����Ʈ �����հ� ���, Ÿ�� ���� �Ӽ� �����͸� ����
/// ������ �����ε�, Ǯ���� ���� ���
/// ����Ʈ �ν��Ͻ� ��� - Ǯ���� �����ؼ� ����ϱ⵵ ��
/// </summary>
public class EffectClip
{
    // �Ӽ��� ������ �ٸ� ����Ʈ Ŭ���� �к��ϱ� ���� �뵵
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
    /// ���ϴ� ��ġ�� ���� ���ϴ� ����Ʈ�� �ν��Ͻ�
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
