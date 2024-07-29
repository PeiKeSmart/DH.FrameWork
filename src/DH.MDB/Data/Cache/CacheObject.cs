using DH.ORM;

using System.Collections;

namespace DH;

/// <summary>
/// 缓存对象，常驻内存，同时以json格式存储在磁盘中
/// </summary>
[Serializable]
public class CacheObject
{

    private long _id;
    private string _name;

    private ExtData _extData;

    [NotSave]
    public ExtData Data
    {
        get
        {
            if (_extData == null) _extData = new ExtData();
            return _extData;
        }
        set
        {
            _extData = value;
        }
    }

    /// <summary>
    /// 对象的 id
    /// </summary>
    public long Id
    {
        get { return _id; }
        set { _id = value; }
    }

    /// <summary>
    /// 对象名称
    /// </summary>
    public String Name
    {
        get { return _name; }
        set { _name = value; }
    }

    /// <summary>
    /// 根据 id 检索对象
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CacheObject FindById(long id)
    {
        return MemoryDB.FindById(GetType(), id);
    }

    /// <summary>
    /// 检索出所有对象
    /// </summary>
    /// <returns></returns>
    public IList FindAll()
    {
        return MemoryDB.FindAll(GetType());
    }

    /// <summary>
    /// 根据名称检索出对象列表
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IList FindByName(String name)
    {
        return FindBy(nameof(Name), name);
    }

    /// <summary>
    /// 根据属性名，检索出对象
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public IList FindBy(String propertyName, Object val)
    {
        FindAll();
        return MemoryDB.FindBy(GetType(), propertyName, val);
    }

    /// <summary>
    /// 插入数据：并对所有属性做索引，速度较慢
    /// </summary>
    public void Insert()
    {
        MemoryDB.Insert(this);
    }

    /// <summary>
    /// 插入数据：只针对特定属性做索引，提高速度
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="pValue"></param>
    public void InsertByIndex(String propertyName, Object pValue)
    {
        MemoryDB.InsertByIndex(this, propertyName, pValue);
    }

    /// <summary>
    /// 插入数据：针对若干属性做索引
    /// </summary>
    /// <param name="dic"></param>
    public void InsertByIndex(Dictionary<String, Object> dic)
    {
        MemoryDB.InsertByIndex(this, dic);
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <returns></returns>
    public Result Update()
    {
        return MemoryDB.Update(this);
    }

    /// <summary>
    /// 更新数据：只针对特性数据做索引
    /// </summary>
    /// <param name="dic"></param>
    public void UpdateByIndex(Dictionary<String, Object> dic)
    {
        MemoryDB.UpdateByIndex(this, dic);
    }

    /// <summary>
    ///  不持久化，也不做索引
    /// </summary>
    /// <returns></returns>
    public static Result UpdateNoIndex()
    {
        return new Result();
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    public void Delete()
    {
        MemoryDB.Delete(this);
    }

}
