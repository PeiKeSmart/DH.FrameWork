﻿using DH.ORM;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DH.Data
{
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
        public ExtData data
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
        public CacheObject findById(long id)
        {
            return MemoryDB.FindById(this.GetType(), id);
        }

        /// <summary>
        /// 检索出所有对象
        /// </summary>
        /// <returns></returns>
        public IList findAll()
        {
            return MemoryDB.FindAll(this.GetType());
        }

        /// <summary>
        /// 根据名称检索出对象列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList findByName(String name)
        {
            return this.findBy("Name", name);
        }

        /// <summary>
        /// 根据属性名，检索出对象
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public IList findBy(String propertyName, Object val)
        {
            findAll();
            return MemoryDB.FindBy(this.GetType(), propertyName, val);
        }


        /// <summary>
        /// 插入数据：并对所有属性做索引，速度较慢
        /// </summary>
        public void insert()
        {
            MemoryDB.Insert(this);
        }

        /// <summary>
        /// 插入数据：只针对特定属性做索引，提高速度
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="pValue"></param>
        public void insertByIndex(String propertyName, Object pValue)
        {
            MemoryDB.InsertByIndex(this, propertyName, pValue);
        }

        /// <summary>
        /// 插入数据：针对若干属性做索引
        /// </summary>
        /// <param name="dic"></param>
        public void insertByIndex(Dictionary<String, Object> dic)
        {
            MemoryDB.InsertByIndex(this, dic);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        public Result update()
        {
            return MemoryDB.Update(this);
        }

        /// <summary>
        /// 更新数据：只针对特性数据做索引
        /// </summary>
        /// <param name="dic"></param>
        public void updateByIndex(Dictionary<String, Object> dic)
        {
            MemoryDB.updateByIndex(this, dic);
        }

        /// <summary>
        ///  不持久化，也不做索引
        /// </summary>
        /// <returns></returns>
        public Result updateNoIndex()
        {
            return new Result();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void delete()
        {
            MemoryDB.Delete(this);
        }

    }
}
