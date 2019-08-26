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

    public int MapID { get { return mapID; } }
    public string[] MapContents { get { return mapContents; } }
    public string MapName { get { return mapName; } }

    public SynthesisMapItem() { }

    public SynthesisMapItem(int id, string[] map, string name)
    {
        this.mapID = id;
        this.mapContents = map;
        this.mapName = name;
    }

    public override string ToString()
    {
        return string.Format("ID:{0}|ContensLength:{1}|Name:{2}", mapID, mapContents.Length, mapName);
    }

}
