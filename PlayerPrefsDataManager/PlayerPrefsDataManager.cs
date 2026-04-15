using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using System.Linq;

/// <summary>
/// PlayerPrefs数据管理类 统一管理数据的存储和读取
/// </summary>
public class PlayerPrefsDataManager
{
    private static PlayerPrefsDataManager instance = new PlayerPrefsDataManager();
    public static PlayerPrefsDataManager Instance
    {
        get
        {
            return instance;
        }
    }
    private PlayerPrefsDataManager()
    {

    }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据对象的唯一key 自己控制</param>
    public void SaveData(object data, string keyName)//此keyName传入的为fieldKey
    {
        //就是要通过Type得到传入数据对象的所有字段
        //然后结合PlayerPrefs来进行存储
        #region 获取传入对象的所有字段
        Type type = data.GetType();
        FieldInfo[] fields = type.GetFields();
        #endregion

        #region 遍历这些字段 进行数据存储
        string fieldKey;
        FieldInfo field;
        for (int i = 0; i < fields.Length; i++)
        {
            //对每一个字段 进行数据存储
            //得到具体的字段信息
            field = fields[i];
            //通过FieldInfo可以直接得到 字段的类型field.FieldType.Name 和字段的名字 field.Name
            #region 自定义key的规则 进行数据存储
            fieldKey = keyName + "_" + type.Name + "_" + field.FieldType.Name + "_" + field.Name;
            //         Player1    _    PlayerInfo   _    Int32                   _    age
            #endregion
            //获取值
            //field.GetValue(data); ——→返回值是object
            //封装了一个方法 专门来存储值
            SaveValue(field.GetValue(data),fieldKey);
        }
        #endregion
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 用于存储单个数据的方法
    /// </summary>
    /// <param name="value">字段类型 用于判断 用哪个API来存储</param>
    /// <param name="keyName">用于获取具体数据</param>
    private void SaveValue(object value, string keyName) //此keyName传入的为fieldKey
    {
        //直接通过PlayerPrefs进行存储
        //根据数据类型的不同 来决定使用哪一个API来进行存储
        //PlayerPrefs只支持3种数据类型
        //判断数据类型 然后调用其他方法
        Type fieldType = value.GetType();
        //类型判断
        if (fieldType == typeof(string))
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if (fieldType == typeof(int))
        {
            PlayerPrefs.SetInt(keyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (fieldType == typeof(bool))
        {
            //自己定义一个存储bool类型的规则
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //如何判断泛型类型
        //通过反射 获取父子关系
        else if (typeof(IList).IsAssignableFrom(fieldType)) //相当于是判断 字段是不是IList的子类
        {
            //父类装子类
            IList list = value as IList;
            //先存储数量
            PlayerPrefs.SetInt(keyName, list.Count);
            int index = 0;
            foreach (object obj in list)
            {
                //存储具体的值 
                //递归用于判断List中泛型的类型
                SaveValue(obj, keyName + index);
                index++;
            }
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //父类装子类
            IDictionary dic = value as IDictionary;
            //先储存数量
            PlayerPrefs.SetInt(keyName, dic.Count);
            int index = 0;
            foreach (object key in dic.Keys)
            {
                SaveValue(key, keyName + "_Key" + index);
                SaveValue(dic[key], keyName + "_Value" + index);
                index++;
            }
        }
        //自定义类型
        else if (typeof(object).IsAssignableFrom(fieldType))
        {
            SaveData(value, keyName);
        }
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="type">想要读取的数据类型</param>
    /// <param name="keyName">数据对象的唯一key 自己控制</param>
    /// <returns></returns>
    public object LoadData(Type type, string keyName)
    {
        //不用object对象传入 而用Type传入
        //主要目的是节约一行代码（在外部）
        //假设现在要读取一个Player类型的数据，如果是传object 就必须在外部new一个对象传入
        //现在有Type的 只用传入一个Type ——→ typeof(Player) 然后在内部动态创建一个对象然后返回出来 达到少写一行代码的作用

        //根据传入的类型 和 keyName
        //依据存储数据时 key的拼接规则 来进行数据的获取赋值 然后返回出去

        //根据传入的Type 创建一个对象 用于存储数据
        object data = Activator.CreateInstance(type);
        //要往new出来的对象中填充数据

        //得到所有字段
        FieldInfo[] fields = type.GetFields();
        FieldInfo field;
        string fieldKey;
        for(int i = 0; i < fields.Length; i++)
        {
            field = fields[i];
            //key的存储规则一定和存储一模一样
            fieldKey = keyName + "_" + type.Name + "_" + field.FieldType.Name + "_" + field.Name;
            //填充数据到data中
            field.SetValue(data,LoadValue(field.FieldType,fieldKey));
        }
        return data;
    }

    /// <summary>
    /// 得到单个数据的方法
    /// </summary>
    /// <param name="value">字段类型 用于判断 用哪个API来读取</param>
    /// <param name="keyName">用于获取具体数据</param>
    /// <returns></returns>
    private object LoadValue(Type fieldType, string keyName)
    {
        if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(keyName, "");
        }
        else if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(keyName, 0);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName, 0);
        }
        else if (fieldType == typeof(bool))
        {
            return PlayerPrefs.GetInt(keyName, 0) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName, 0);
            //实例化一个IList对象来进行赋值 父类装子类
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            int count = PlayerPrefs.GetInt(keyName, 0);
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                dic.Add(LoadValue(kvType[0], keyName + "_Key" + i), LoadValue(kvType[1], keyName + "_Value" + i));
            }
            return dic;
        }
        else return LoadData(fieldType, keyName);
    }
}
