using UnityEngine;

/// <summary>
/// data의 기본 클래스
/// 데이터의 갯수와 이름의 목록 리스트
/// </summary>
public class BaseData : ScriptableObject
{
    public const string dataDirectory = "/9.ResourcesData/Resources/Data/";
    public string[] names = null;

    public BaseData() { }

    public int GetDataCount()
    {
        int retValue = 0;

        if (this.names != null)
        {
            retValue = this.names.Length;
        }

        return retValue;
    }

    /// <summary>
    /// 툴에 출력하기 위한 리스트를 만들어주는 함수
    /// </summary>
    public string[] GetNameList(bool showID, string filterWord = "")
    {
        string[] retList = new string[0];

        if (this.names == null)
        {
            return retList;
        }

        retList = new string[this.names.Length];

        for (int i = 0; i < this.names.Length; i++)
        {
            if (filterWord != "")
            {
                if (names[i].ToLower().Contains(filterWord.ToLower()) == false)
                {
                    continue;
                }
            }

            if (showID)
            {
                retList[i] = i.ToString() + " : " + this.names[i];
            }
            else
            {
                retList[i] = this.names[i];
            }
        }

        return retList;
    }

    public virtual int AddData(string newName)
    {
        return GetDataCount();
    }
    public virtual void RemoveData(int index)
    {

    }
    public virtual void Copy(int index)
    {

    }
}
