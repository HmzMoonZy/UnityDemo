using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectPool))]
public class BulletMark : MonoBehaviour
{
    private Transform parent;       //资源管理父物体
    private ObjectPool pool;        //对象池,管理特效

    private Texture2D mainTexture;                  //主贴图
    private Texture2D fusionTexture;                //反融合
    private Texture2D prefabIndTexture;             //预制体独立贴图

    private Texture2D markTexture;                             //弹痕贴图
    private GameObject effect;                                  //特效
    private Queue<Vector2> markQueue = new Queue<Vector2>();    //弹痕队列

    [SerializeField] private TextureType textureType;            //[序列化字段]贴图属性
    [SerializeField] private int hp;                             //临时测试hp

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
                Destroy(gameObject);
        }
    }
    void Awake()
    {
        FindInit();
    }

    /// <summary>
    /// 初始化查找组件
    /// </summary>
    private void FindInit()
    {
        //初始化弹痕贴图/设计特效/父物体
        switch (textureType)
        {
            case TextureType.Metal:
                InitTexture("Bullet Decal_Metal", "Bullet Impact FX_Metal", "ALLBulletMark_Metal");
                break;
            case TextureType.Stone:
                InitTexture("Bullet Decal_Stone", "Bullet Impact FX_Stone", "ALLBulletMark_Stone");
                break;
            case TextureType.Wood:
                InitTexture("Bullet Decal_Wood", "Bullet Impact FX_Wood", "ALLBulletMark_Wood");
                break;
        }

        //主贴图备份通过实例化存储，不能直接赋值。
        //预制体的贴图是地址值,须独立.
        mainTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
        fusionTexture = Instantiate<Texture2D>(mainTexture);        //用于反融合
        prefabIndTexture = Instantiate<Texture2D>(mainTexture);     //用于预制体主贴图赋值
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = prefabIndTexture;

        //添加对象池
        if (gameObject.GetComponent<ObjectPool>() != null)
            pool = gameObject.GetComponent<ObjectPool>();
        else
            pool = gameObject.AddComponent<ObjectPool>();
    }

    /// <summary>
    /// 初始化弹痕贴图及特效
    /// </summary>
    private void InitTexture(string markFile, string effectFile, string parent)
    {
        markTexture = Resources.Load<Texture2D>("Weapon/BulletMarks/" + markFile);
        effect = Resources.Load<GameObject>("Effects/Weapon/" + effectFile);
        this.parent = GameObject.Find("TempManager/" + parent).GetComponent<Transform>();
    }
    //弹痕生成(融合)逻辑
    public void CreateBulletMark(RaycastHit hit)
    {
        //生成特效
        CreateEffect(hit);
        //获取碰撞点在主贴图上的纹理坐标(uv)；
        Vector2 uv = hit.textureCoord;
        //将uv信息添加进队列
        markQueue.Enqueue(uv);
        //遍历弹痕贴图的宽和高；
        for (int i = 0; i < markTexture.width; i++)
        {
            for (int j = 0; j < markTexture.height; j++)
            {
                //uv.x * 主贴图宽度 - 弹痕贴图宽度 / 2 + i;
                float x = uv.x * mainTexture.width - 0.5f * markTexture.width + i;
                //uv.y * 主贴图高度 - 弹痕贴图高度 / 2 + j;
                float y = uv.y * mainTexture.height - 0.5f * markTexture.height + j;
                //通过循环索引获取弹痕像素点的颜色值；
                Color color = markTexture.GetPixel(i, j);
                //在主贴图的相应位置设置新的像素值；
                if (color.a >= 0.35)
                    prefabIndTexture.SetPixel((int)x, (int)y, color);
            }
        }

        //最终要保存融合后的贴图
        prefabIndTexture.Apply();
        Invoke("RemoveBulletMark", 6f);
    }
    /// <summary>
    /// 生成特效
    /// </summary>
    private void CreateEffect(RaycastHit hit)
    {
        GameObject temp = null;
        if (pool.IsTemp())
        {
            temp = Instantiate(effect, hit.point, Quaternion.LookRotation(hit.normal), parent);
        }
        else    //从池中初始化
        {
            temp = pool.GetObject();
            temp.SetActive(true);
            temp.GetComponent<Transform>().position = hit.point;
            temp.GetComponent<Transform>().rotation = Quaternion.LookRotation(hit.normal);
        }
        temp.name = "bulletMarkEffect";
        StartCoroutine("DelayIntoPool", temp);
    }
    /// <summary>
    /// 特效延迟入池
    /// </summary>
    private IEnumerator DelayIntoPool(GameObject go)
    {
        yield return new WaitForSeconds(2);
        pool.AddObject(go);
        go.SetActive(false);
    }
    /// <summary>
    /// 消除弹痕贴图
    /// </summary>
    private void RemoveBulletMark()
    {
        Vector2 uv = new Vector2();
        if (markQueue.Count > 0)
            uv = markQueue.Dequeue();
        for (int i = 0; i < markTexture.width; i++)
        {
            for (int j = 0; j < markTexture.height; j++)
            {
                float x = uv.x * mainTexture.width - 0.5f * markTexture.width + i;
                float y = uv.y * mainTexture.height - 0.5f * markTexture.height + j;

                Color color = fusionTexture.GetPixel((int)x, (int)y);
                prefabIndTexture.SetPixel((int)x, (int)y, color);
            }
        }
        prefabIndTexture.Apply();
    }

}
