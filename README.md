# MyUnityDemo
# **简介**
**个人用于练习的一款第一人称生存游戏，需要实现的功能有:**  
+ 背包模块(已完成)
+ 合成模块(已完成)
+ 快捷栏(已完成)
+ 角色动画(已完成)
+ 枪械模块(已完成)
+ 地形模块
+ 建造系统
+ 采集模块
+ 剧情(对话)模块

# **目前进度**
>## ***2019年9月17日***   
>+ 双摄像机的使用。 
>+ 对象池管理弹药模型和特效预制体。
>+ 完成了快捷栏的制作,并能够使用键盘触发,关联武器模块。
>+ 完成了武器模块的模型动画，利用DOTween实现瞄准（开镜）动画。
>+ 对武器模块进行抽象,提高开发效率。
>+ 使用简单工厂模式进行武器预制体的生成,使每把武器独立。
>+ 射击触发音效、特效播放，在Animation的Event中完善动画和射击逻辑。
>+ 命中目标后对目标贴图进行贴图融合及反融合，同一预制体独立贴图。
>+ 武器耐久度UI的更新。
>### 武器切换动画示例:
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/doc/document/%E6%AD%A6%E5%99%A8%E5%88%87%E6%8D%A2Demo.gif?raw=true)
>### 武器开火及弹痕生成示例:
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/doc/document/%E6%9E%AA%E6%A2%B0%E5%BC%80%E7%81%AB%E5%BC%B9%E7%97%95Demo.gif?raw=true)
>### 耐久度UI示例:
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/doc/document/%E8%80%90%E4%B9%85%E5%BA%A6%E6%9D%A1%E9%80%BB%E8%BE%91Demo.gif?raw=true)
>### 冷兵器开火动画示例:
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/master/document/%E5%86%B7%E5%85%B5%E5%99%A8%E5%BC%80%E7%81%AB%E5%8A%A8%E7%94%BBDemo.gif)
>### 结构图
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/doc/document/%E5%B7%A5%E5%85%B7%E6%A0%8F%E5%8F%8A%E6%9E%AA%E6%A2%B0%E6%A8%A1%E5%9D%97%E9%80%BB%E8%BE%91%E5%9B%BE.png)
    


>## ***2019年9月5日***   
>### 完成了**背包模块**和**合成模块**的基础逻辑。  
>+ 具体采用了MVC结构，读取Json文件生成游戏道具。
>+ 合成模块UI交互,如道具的叠加,菜单交互。
>+ 利用合成图谱（类似MC）制作物品。
>+ 物体的拖拽功能及拖拽目标的判定逻辑。
>+ 合成物品后的数值计算。
>### 合成模块示例:
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/doc/document/%E5%90%88%E6%88%90%E6%A8%A1%E5%9D%97Demo.gif?raw=true)
>### 结构图
>![logic](https://github.com/HmzMoonZy/UnityDemo/blob/doc/document/%E8%83%8C%E5%8C%85%E9%9D%A2%E6%9D%BF%E5%90%88%E6%88%90%E9%9D%A2%E6%9D%BF%E9%80%BB%E8%BE%91%E5%9B%BE.png)
 # **已知BUG**
>+ 背包面板内的ItemUI覆盖不完整，导致背包内图片有可能添加在同一物品槽内。
>+ 拆分物品的数值重置不完整,导致合成时在背包内对物体的拆分操作无法正常判断数值。