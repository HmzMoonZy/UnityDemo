using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectPool))]
public class BulletMark : MonoBehaviour
{
    private Transform m_parent;       //资源管理父物体
    private ObjectPool m_pool;        //对象池,管理特效

    private Texture2D m_mainTexture;                  //主贴图
    private Texture2D m_fusionTexture;                //反融合
    private Texture2D m_prefab_indTexture;            //预制体独立贴图

    private Texture2D m_markTexture;                              //弹痕贴图
    private GameObject m_effect;                                  //特效
    private Queue<Vector2> m_markQueue = new Queue<Vector2>();    //弹痕队列

    [SerializeField] private TextureType m_textureType;           //[序列化字段]贴图属性
    [SerializeField] private int m_hp;                            //临时测试hp

    public int M_HP
    {
        get { return m_hp; }
        set
        {
            m_hp = value;
            if (m_hp <= 0)
                Destroy(gameObject);
        }
    }
    void Start()
    {
        FindInit();
    }

    /// <summary>
    /// 初始化查找组件
    /// </summary>
    private void FindInit()
    {
        //初始化弹痕贴图/设计特效/父物体
        switch (m_textureType)
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
        if (gameObject.name == "conifer")
        {
            m_mainTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().materials[2].mainTexture;
            Debug.Log(m_mainTexture.name);
        }
        else
            m_mainTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;

        m_fusionTexture = Instantiate<Texture2D>(m_mainTexture);         //用于反融合
        m_prefab_indTexture = Instantiate<Texture2D>(m_mainTexture);     //用于预制体主贴图赋值
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = m_prefab_indTexture;

        //添加对象池
        if (gameObject.GetComponent<ObjectPool>() != null)
            m_pool = gameObject.GetComponent<ObjectPool>();
        else
            m_pool = gameObject.AddComponent<ObjectPool>();
    }

    /// <summary>
    /// 初始化弹痕贴图及特效
    /// </summary>
    private void InitTexture(string markFile, string effectFile, string parent)
    {
        m_markTexture = Resources.Load<Texture2D>("Weapon/BulletMarks/" + markFile);
        m_effect = Resources.Load<GameObject>("Effects/Weapon/" + effectFile);
        this.m_parent = GameObject.Find("TempManager/" + parent).GetComponent<Transform>();
    }
    //弹痕生成(融合)逻辑
    public void CreateBulletMark(RaycastHit hit)
    {
        //生成特效
        CreateEffect(hit);
        //获取碰撞点在主贴图上的纹理坐标(uv)；
        Vector2 uv = hit.textureCoord;
        //将uv信息添加进队列
        m_markQueue.Enqueue(uv);
        //遍历弹痕贴图的宽和高；
        for (int i = 0; i < m_markTexture.width; i++)
        {
            for (int j = 0; j < m_markTexture.height; j++)
            {
                //uv.x * 主贴图宽度 - 弹痕贴图宽度 / 2 + i;
                float x = uv.x * m_mainTexture.width - 0.5f * m_markTexture.width + i;
                //uv.y * 主贴图高度 - 弹痕贴图高度 / 2 + j;
                float y = uv.y * m_mainTexture.height - 0.5f * m_markTexture.height + j;
                //通过循环索引获取弹痕像素点的颜色值；
                Color color = m_markTexture.GetPixel(i, j);
                //在主贴图的相应位置设置新的像素值；
                if (color.a >= 0.35)
                    m_prefab_indTexture.SetPixel((int)x, (int)y, color);
            }
        }

        //最终要保存融合后的贴图
        m_prefab_indTexture.Apply();
        Invoke("RemoveBulletMark", 6f);
    }
    /// <summary>
    /// 生成特效
    /// </summary>
    private void CreateEffect(RaycastHit hit)
    {
        GameObject temp = null;
        if (m_pool.IsTemp())
        {
            temp = Instantiate(m_effect, hit.point, Quaternion.LookRotation(hit.normal), m_parent);
        }
        else    //从池中初始化
        {
            temp = m_pool.GetObject();
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
        m_pool.AddObject(go);
        go.SetActive(false);
    }
    /// <summary>
    /// 消除弹痕贴图
    /// </summary>
    private void RemoveBulletMark()
    {
        Vector2 uv = new Vector2();
        if (m_markQueue.Count > 0)
            uv = m_markQueue.Dequeue();
        for (int i = 0; i < m_markTexture.width; i++)
        {
            for (int j = 0; j < m_markTexture.height; j++)
            {
                float x = uv.x * m_mainTexture.width - 0.5f * m_markTexture.width + i;
                float y = uv.y * m_mainTexture.height - 0.5f * m_markTexture.height + j;

                Color color = m_fusionTexture.GetPixel((int)x, (int)y);
                m_prefab_indTexture.SetPixel((int)x, (int)y, color);
            }
        }
        m_prefab_indTexture.Apply();
    }

}
