using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMark : MonoBehaviour
{

    private Texture2D _texture; //主贴图
    private Texture2D mark_Texture; //弹痕贴图

    void Awake()
    {
        _texture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
        mark_Texture = Resources.Load<Texture2D>("Weapon/BulletMarks/Bullet Decal_Stone");
    }
    //弹痕生成逻辑
    public void CreateBulletMark(RaycastHit hit)
    {
        //获取碰撞点在主贴图上的纹理坐标(uv)；
        Vector2 uv = hit.textureCoord;
        Debug.Log("UV:" + uv.x + "," + uv.y);
        //遍历弹痕贴图的宽和高；
        for (int i = 0; i < mark_Texture.width; i++)
        {
            for (int j = 0; j < mark_Texture.height; j++)
            {
                //uv.x * 主贴图宽度 - 弹痕贴图宽度 / 2 + i;
                float x = uv.x * _texture.width - 0.5f * mark_Texture.width + i;
                //uv.y * 主贴图高度 - 弹痕贴图高度 / 2 + j;
                float y = uv.y * _texture.height - 0.5f * mark_Texture.height + j;
                //通过循环索引获取弹痕像素点的颜色值；
                Color color = mark_Texture.GetPixel(i, j);
                //在主贴图的相应位置设置新的像素值；
                if (color.a >= 0.35) 
                    _texture.SetPixel((int)x, (int)y, color);
            }
        }
        //最终要保存融合后的贴图
        _texture.Apply();
    }
}
