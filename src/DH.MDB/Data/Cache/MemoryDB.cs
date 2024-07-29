using DH.ORM;
using DH.Reflection;
using DH.Serialization;

using NewLife.Log;

using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace DH;

internal class MemoryDB
{

    private static IDictionary objectList = Hashtable.Synchronized([]);
    private static IDictionary indexList = Hashtable.Synchronized([]);

    public static IDictionary GetObjectsMap()
    {
        return objectList;
    }

    public static IDictionary GetIndexMap()
    {
        return indexList;
    }


    private static Object objLock = new();

    private static Object chkLock = new();


    private static IList GetObjectsByName(Type t)
    {

        if (IsCheckFileDB(t))
        {

            lock (chkLock)
            {

                if (IsCheckFileDB(t))
                {

                    LoadDataFromFile(t);
                    _hasCheckedFileDB[t] = true;

                }

            }

        }
        return (objectList[t.FullName] as IList);
    }

    private static Hashtable _hasCheckedFileDB = [];

    private static Boolean IsCheckFileDB(Type t)
    {
        if (_hasCheckedFileDB[t] == null) return true;
        return false;
    }

    private static void LoadDataFromFile(Type t)
    {
        if (IO.File.Exists(GetCachePath(t)))
        {
            IList list = GetListWithIndex(IO.File.Read(GetCachePath(t)), t);
            objectList[t.FullName] = list;
        }
        else
        {
            objectList[t.FullName] = new ArrayList();
        }
    }

    private static IList GetListWithIndex(String jsonString, Type t)
    {

        IList list = new ArrayList();

        if (strUtil.IsNullOrEmpty(jsonString)) return list;

        List<object> lists = JsonParser.Parse(jsonString) as List<object>;

        foreach (JsonObject jsonObject in lists)
        {
            CacheObject obj = TypedDeserializeHelper.deserializeType(t, jsonObject) as CacheObject;
            int index = list.Add(obj);
            AddIdIndex(t.FullName, obj.Id, index);
            MakeIndexByInsert(obj);
        }

        return list;
    }

    private static void Serialize(Type t)
    {
        Serialize(t, GetObjectsByName(t));
    }

    private static void Serialize(Type t, IList list)
    {
        String target = SimpleJsonString.ConvertList(list);
        if (strUtil.IsNullOrEmpty(target)) return;

        String absolutePath = GetCachePath(t);
        lock (objLock)
        {

            String dir = Path.GetDirectoryName(absolutePath);
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }

            DH.IO.File.Write(absolutePath, target);
        }
    }

    private static void UpdateObjects(String key, IList list)
    {
        objectList[key] = list;
    }

    //------------------------------------------------------------------------------

    internal static CacheObject FindById(Type t, long id)
    {

        IList list = GetObjectsByName(t);
        if (list.Count > 0)
        {
            int objIndex = GetIndex(t.FullName, id);
            if (objIndex >= 0 && objIndex < list.Count)
            {
                return list[objIndex] as CacheObject;
            }
        }
        return null;
    }

    internal static IList FindBy(Type t, String propertyName, Object val)
    {

        String propertyKey = GetPropertyKey(t.FullName, propertyName);
        NameValueCollection valueCollection = GetValueCollection(propertyKey);

        String ids = valueCollection[val.ToString()];
        if (strUtil.IsNullOrEmpty(ids)) return new ArrayList();

        IList results = new ArrayList();
        String[] arrItem = ids.Split(',');
        foreach (String strId in arrItem)
        {
            long id = cvt.ToLong(strId);
            if (id < 0) continue;
            CacheObject obj = FindById(t, id);
            if (obj != null) results.Add(obj);
        }
        return results;
    }

    internal static IList FindAll(Type t)
    {
        return new ArrayList(GetObjectsByName(t));
    }

    internal static void Insert(CacheObject obj)
    {

        Type t = obj.GetType();
        String _typeFullName = t.FullName;

        IList list = FindAll(t);
        obj.Id = GetNextId(list);

        int index = list.Add(obj);

        AddIdIndex(_typeFullName, obj.Id, index);
        UpdateObjects(_typeFullName, list);

        MakeIndexByInsert(obj);

        if (IsInMemory(t)) return;

        Serialize(t);
    }

    internal static void InsertByIndex(CacheObject obj, String propertyName, Object pValue)
    {

        Type t = obj.GetType();
        String _typeFullName = t.FullName;

        IList list = FindAll(t);
        obj.Id = GetNextId(list);
        int index = list.Add(obj);

        AddIdIndex(_typeFullName, obj.Id, index);
        UpdateObjects(_typeFullName, list);

        MakeIndexByInsert(obj, propertyName, pValue);

        if (IsInMemory(t)) return;

        Serialize(t);
    }

    internal static void InsertByIndex(CacheObject obj, Dictionary<String, Object> dic)
    {

        Type t = obj.GetType();
        String _typeFullName = t.FullName;

        IList list = FindAll(t);
        obj.Id = GetNextId(list);
        int index = list.Add(obj);

        AddIdIndex(_typeFullName, obj.Id, index);
        UpdateObjects(_typeFullName, list);

        foreach (KeyValuePair<String, Object> kv in dic)
        {
            MakeIndexByInsert(obj, kv.Key, kv.Value);
        }

        if (IsInMemory(t)) return;

        Serialize(t);
    }

    internal static Result Update(CacheObject obj)
    {

        Type t = obj.GetType();

        MakeIndexByUpdate(obj);

        if (IsInMemory(t)) return new Result();

        try
        {
            Serialize(t);
            return new Result();
        }
        catch (Exception ex)
        {
            XTrace.WriteException(ex);

            throw;
        }
    }

    internal static Result UpdateByIndex(CacheObject obj, Dictionary<String, Object> dic)
    {

        Type t = obj.GetType();

        MakeIndexByUpdate(obj);

        if (IsInMemory(t)) return new Result();

        try
        {
            Serialize(t);
            return new Result();
        }
        catch (Exception ex)
        {
            XTrace.WriteException(ex);

            throw;
        }
    }

    internal static void Delete(CacheObject obj)
    {

        Type t = obj.GetType();
        String _typeFullName = t.FullName;

        MakeIndexByDelete(obj);

        IList list = FindAll(t);
        list.Remove(obj);
        UpdateObjects(_typeFullName, list);

        DeleteIdIndex(_typeFullName, obj.Id);

        if (IsInMemory(t)) return;

        Serialize(t, list);
    }

    private static long GetNextId(IList list)
    {
        if (list.Count == 0) return 1;
        CacheObject preObject = list[list.Count - 1] as CacheObject;
        return preObject.Id + 1;
    }

    private static Boolean IsInMemory(Type t)
    {
        return rft.GetAttribute(t, typeof(NotSaveAttribute)) != null;
    }

    //----------------------------------------------------------------------------------------------

    private static Object objIndexLock = new object();
    private static Object objIndexLockInsert = new object();
    private static Object objIndexLockUpdate = new object();
    private static Object objIndexLockDelete = new object();

    private static void MakeIndexByInsert(CacheObject cacheObject, String propertyName, Object pValue)
    {
        if (cacheObject == null || pValue == null) return;
        Type t = cacheObject.GetType();
        String propertyKey = GetPropertyKey(t.FullName, propertyName);
        lock (objIndexLock)
        {
            NameValueCollection valueCollection = GetValueCollection(propertyKey);
            valueCollection.Add(pValue.ToString(), cacheObject.Id.ToString());
            indexList[propertyKey] = valueCollection;
        }
    }

    private static void MakeIndexByInsert(CacheObject cacheObject)
    {
        if (cacheObject == null) return;
        Type t = cacheObject.GetType();
        PropertyInfo[] properties = GetProperties(t);
        foreach (PropertyInfo p in properties)
        {

            String propertyKey = GetPropertyKey(t.FullName, p.Name);
            lock (objIndexLockInsert)
            {
                NameValueCollection valueCollection = GetValueCollection(propertyKey);
                AddNewValueMap(valueCollection, cacheObject, p);
            }
        }
    }

    private static void MakeIndexByUpdate(CacheObject cacheObject)
    {
        if (cacheObject == null) return;
        Type t = cacheObject.GetType();
        PropertyInfo[] properties = GetProperties(t);
        foreach (PropertyInfo p in properties)
        {

            String propertyKey = GetPropertyKey(t.FullName, p.Name);

            lock (objIndexLockUpdate)
            {
                NameValueCollection valueCollection = GetValueCollection(propertyKey);
                DeleteOldValueIdMap(valueCollection, cacheObject.Id);
                AddNewValueMap(valueCollection, cacheObject, p);
            }

        }
    }

    private static void MakeIndexByUpdate(CacheObject cacheObject, String propertyName, Object pValue)
    {
        if (cacheObject == null || pValue == null) return;
        Type t = cacheObject.GetType();

        String propertyKey = GetPropertyKey(t.FullName, propertyName);

        lock (objIndexLockUpdate)
        {

            NameValueCollection valueCollection = GetValueCollection(propertyKey);
            DeleteOldValueIdMap(valueCollection, cacheObject.Id);
            valueCollection.Add(pValue.ToString(), cacheObject.Id.ToString());
            indexList[propertyKey] = valueCollection;

        }
    }

    private static void MakeIndexByDelete(CacheObject cacheObject)
    {
        if (cacheObject == null) return;
        Type t = cacheObject.GetType();
        PropertyInfo[] properties = GetProperties(t);
        foreach (PropertyInfo p in properties)
        {

            String propertyKey = GetPropertyKey(t.FullName, p.Name);
            lock (objIndexLockDelete)
            {
                NameValueCollection valueCollection = GetValueCollection(propertyKey);
                DeleteOldValueIdMap(valueCollection, cacheObject.Id);
            }
        }
    }

    private static PropertyInfo[] GetProperties(Type t)
    {
        return t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }

    private static NameValueCollection GetValueCollection(String propertyKey)
    {
        NameValueCollection valueCollection = indexList[propertyKey] as NameValueCollection;
        if (valueCollection == null) valueCollection = new NameValueCollection();
        return valueCollection;
    }

    private static void AddNewValueMap(NameValueCollection valueCollection, CacheObject cacheObject, PropertyInfo p)
    {

        Attribute attr = rft.GetAttribute(p, typeof(NotSaveAttribute));
        if (attr != null) return;

        String propertyKey = GetPropertyKey(cacheObject.GetType().FullName, p.Name);

        if (p.CanRead == false) return;
        Object pValue = rft.GetPropertyValue(cacheObject, p.Name);
        if (pValue == null || strUtil.IsNullOrEmpty(pValue.ToString())) return;

        valueCollection.Add(pValue.ToString(), cacheObject.Id.ToString());
        indexList[propertyKey] = valueCollection;
    }

    // TODO 优化
    private static void DeleteOldValueIdMap(NameValueCollection valueCollection, long oid)
    {
        foreach (String key in valueCollection.AllKeys)
        {

            String val = valueCollection[key];
            String[] arrItem = val.Split(',');
            StringBuilder result = new StringBuilder();
            foreach (String strId in arrItem)
            {
                long id = cvt.ToLong(strId);
                if (id == oid) continue;
                result.Append(strId);
                result.Append(",");
            }
            String resultStr = result.ToString();
            if (strUtil.HasText(resultStr))
                valueCollection[key] = resultStr.Trim().TrimEnd(',');
            else
                valueCollection.Remove(key);
        }
    }

    private static String GetPropertyKey(String typeFullName, String propertyName)
    {
        return typeFullName + "_" + propertyName;
    }


    //-------------------------- Id Index --------------------------------

    private static IDictionary GetIdIndexMap(String key)
    {
        if (objectList[key] == null)
        {
            objectList[key] = new Hashtable();
        }
        return (objectList[key] as IDictionary);
    }

    private static void UpdateIdIndexMap(String key, IDictionary map)
    {
        objectList[key] = map;
    }

    private static void ClearIdIndexMap(String key)
    {
        objectList.Remove(key);
    }


    private static void AddIdIndex(String typeFullName, long oid, int index)
    {
        String key = GetIdIndexMapKey(typeFullName);
        IDictionary indexMap = GetIdIndexMap(key);
        indexMap[oid] = index;
        UpdateIdIndexMap(key, indexMap);
    }
    private static void DeleteIdIndex(String typeFullName, long oid)
    {

        String key = GetIdIndexMapKey(typeFullName);

        ClearIdIndexMap(key);

        IList results = objectList[typeFullName] as IList;
        foreach (CacheObject obj in results)
        {
            AddIdIndex(typeFullName, obj.Id, results.IndexOf(obj));
        }

        IDictionary indexMap = GetIdIndexMap(key);
        UpdateIdIndexMap(key, indexMap);
    }

    private static int GetIndex(String typeFullName, long oid)
    {
        int result = -1;
        Object objIndex = GetIdIndexMap(GetIdIndexMapKey(typeFullName))[oid];
        if (objIndex != null)
        {
            result = (int)objIndex;
        }
        return result;
    }

    private static String GetIdIndexMapKey(String typeFullName)
    {
        return String.Format("{0}_oid_index", typeFullName);
    }

    //----------------------------------------------------------

    private static String GetCachePath(Type t)
    {
        return GetCacheFileName(t.FullName);
    }

    private static String GetCacheFileName(String name)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data/{name}{fileExt}");
    }

    private static readonly String fileExt = ".json";

    internal static void Clear()
    {
        _hasCheckedFileDB = new Hashtable();
        objectList = Hashtable.Synchronized(new Hashtable());
        indexList = Hashtable.Synchronized(new Hashtable());
    }

}
