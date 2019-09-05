using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 合成图谱数据实体类
/// </summary>
public class SynthesisMapItem
{
    private int mapID;
    private string[] mapContents;
    private string mapName;
    private int count;

    public int MapID { get { return mapID; } }
    public string[] MapContents { get { return mapContents; } }
    public string MapName { get { return mapName; } }
    public int Count { get { return count; } set { count = value; } }

    public SynthesisMapItem() { }

    public SynthesisMapItem(int id, string[] map, int count, string name)
    {
        this.mapID = id;
        this.mapContents = map;
        this.mapName = name;
        this.count = count;
    }

    public override string ToString()
    {
        return string.Format("ID:{0}|ContensLength:{1}|Name:{2}|Count:{3}", mapID, mapContents.Length, mapName, count);
    }

}
