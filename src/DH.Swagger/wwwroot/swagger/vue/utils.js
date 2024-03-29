﻿var os = {
    path:function(){
        return window.location.protocol+'//'+location.host;
    },
    set: function (table, settings) {
        var _set = JSON.stringify(settings);
        return localStorage.setItem(table, _set);
    },
    get: function (table) {
        var data = localStorage.getItem(table);
        try {
            data = JSON.parse(data);
        } catch (err) {
            return null;
        }
        return data;
    },
    remove: function (table) {
        return localStorage.removeItem(table);
    },
    clear: function () {
        return localStorage.clear();
    },
    isJSON: function (str) {
        if (typeof str == 'string') {
            try {
                var obj = JSON.parse(str);
                if (typeof obj == 'object' && obj) {
                    return true;
                } else {
                    return false;
                }

            } catch (e) {
                return false;
            }
        }
    },
    time:function(date, fmt='yyyy-MM-dd hh:mm:s'){
        date = new Date(date);
        var o = {
            "M+" : date.getMonth()+1,                 
            "d+" : date.getDate(),                    
            "h+" : date.getHours(),                   
            "m+" : date.getMinutes(),                 
            "s+" : date.getSeconds(),                 
            "q+" : Math.floor((date.getMonth()+3)/3), 
            "S"  : date.getMilliseconds()             
        };
        if(/(y+)/.test(fmt)) {
            fmt=fmt.replace(RegExp.$1, (date.getFullYear()+"").substr(4 - RegExp.$1.length));
        }
        for(var k in o) {
            if(new RegExp("("+ k +")").test(fmt)){
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));
            }
        }
        return fmt;
    },
    unique:function(arr) {
        const res = new Map();
        return arr.filter((arr) => !res.has(arr) && res.set(arr, 1))
    },
    uuid : function (length = 32) {
        const num =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        let str = "";
        for (let i = 0; i < length; i++) {
            str += num.charAt(Math.floor(Math.random() * num.length));
        }
        return str;
    },
    parameters(obj,sourceObj){
        let list=[];
        obj.parameters.forEach((item) => {
            let dataType=undefined,value=undefined;
            if(item.schema){
                const {items,format,type} = item.schema;
                if(item.schema.default){
                    value=item.schema.default;
                }
                if(item.schema.$ref){
                    const entityRes=os.refSchemasEntity(item.schema.$ref,sourceObj);
                    dataType=entityRes.type;
                }
                if(type && type==='array' && items && items.$ref){
                    const entityRes=os.refSchemasEntity(items.$ref,sourceObj);
                    dataType='List<'+entityRes.type+'>';
                }
                if(type && type==='string' || type && type==='boolean'){
                    dataType=''+type+'';
                }
                if(type && type==='integer' && format && format==='int32'){
                    dataType=''+format+'';
                }
                if(type && type==='integer' && format && format==='int64'){
                    dataType=''+format+'';
                }
                if(type && type==='string' && format && format==='date-time'){
                    dataType=''+format+'';
                }
                if(type && type==='array' && items && items.format==='int32'){
                    dataType='List<'+items.format+'>';
                }
                if(type && type==='array' && items && items.format==='int64'){
                    dataType='List<'+items.format+'>';
                }
                if(type && type==='array' && items && items.type==='string'){
                    dataType='List<'+items.type+'>';
                }
            }
            const globalSetting = os.get('globalSetting');
            let checkSelectStatus=false;
            if (globalSetting) {
                checkSelectStatus=globalSetting.getSelect;
            }
            list.push({key: item.name,type:dataType, value: value, status: checkSelectStatus, summary: item.description});
        });
        return list;
    },
    // 请求body参数
    requestBody(obj,sourceObj){
        if(!obj.requestBody){
            return {data:null};
        }
        let schema=null;
        const content=obj.requestBody.content;
        for (let k in content){
            schema=content[k].schema;
        }
        if(!schema){
            return {data:null};
        }
        if (schema.type==='string'){
            return {data:'""',list:[{id:os.uuid(),key:'string',type:'string',summary:'string-字符串',children:undefined}]};
        }
        if (schema.type==='integer'){
            return {data:'0',list:[{id:os.uuid(),key:'integer',type:'integer',summary:'integer-字符',children:undefined}]};
        }
        if (schema.type==='array'){
            if(schema.items && schema.items.type && schema.items.type==='integer'){
                return {data:'[0]',list:[{id:os.uuid(),key:'integer',type:'array',summary:'List<int>',children:undefined}]};
            }
            if(schema.items && schema.items.type && schema.items.type==='string'){
                return {data:'[""]',list:[{id:os.uuid(),key:'string',type:'array',summary:'List<string>',children:undefined}]};
            }
            if(schema.items && schema.items.$ref){
                let objectRes=os.requestResolObject(schema.items.$ref,sourceObj);
                return {data:JSON.stringify(JSON.parse('['+objectRes.jsonStr+']'), null, 2),list:objectRes.data};
            }
        }
        if(schema.$ref){
            let objectRes=os.requestResolObject(schema.$ref,sourceObj);
            return {data:objectRes.jsonStr,list:objectRes.data};
        }
        return {data:null};
    },
    responsesBody(obj,sourceObj){
        if(!obj.responseBody){
            return null;
        }
        const {200:{content,description}}=obj.responseBody;
        if(!content){
            return {data:null,tip:description};
        }
        let entityPath=undefined;
        for (let k in content){
            if(content[k].schema.items){
                entityPath=content[k].schema.items.$ref;
            }
            else{
                entityPath=content[k].schema.$ref;
            }
        }
        if(!entityPath){
            return null;
        }
        let objectRes=os.requestResolObject(entityPath,sourceObj);
        return {data:objectRes.data,tip:description,example:objectRes.jsonStr};
    },
    //解析object
    requestResolObject(entityPath,sourceObj){
        entityPath=entityPath.replace('#/','');
        const pathArr=entityPath.split('/');
        const entity=sourceObj.components.schemas[pathArr[2]];
        let list=[];
        for (let k in entity.properties){
            let _summary='';
            let childrenData={};
            if(entity.properties[k].description)
                _summary=entity.properties[k].description;
            if(entity.properties[k].$ref)
            {
                childrenData=os.refSchemasEntity(entity.properties[k].$ref,sourceObj);
                entity.properties[k].type=childrenData.type;
                _summary=childrenData.summary;
            }
            if(entity.properties[k].items && entity.properties[k].type==='array'){
                if(entity.properties[k].items.$ref){
                    childrenData=os.refSchemasEntity(entity.properties[k].items.$ref,sourceObj);
                }
            }
            list.push({id:os.uuid(),key:k,type:entity.properties[k].type,nullable:entity.properties[k].nullable,summary:_summary,children:childrenData.data});
        }
        return {data:list,jsonStr:os.refSchemasJson(list)};
    },
    refSchemasEntity(path,sourceObj){
        path=path.replace('#/','');
        const pathArr=path.split('/');
        const entity=sourceObj.components.schemas[pathArr[2]];
        let list=[];
        if(entity.enum){
            entity.enum.forEach((item)=>{
                list.push({id:os.uuid(),key:item,value:'',status:true,nullable:null,summary:entity.format,type:entity.type,obj:null});
            });
            return {data:list,type:'Enum',summary:'枚举'};
        }
        if(entity.type!=='object')
            return 'null';
        for (let k in entity.properties){
            list.push({id:os.uuid(),key:k,value:'',status:true,nullable:entity.properties[k].nullable,summary:entity.properties[k].description,type:entity.properties[k].type,obj:entity.properties[k]});
        }
        return {data:list,type:'object',summary:entity.description};
    },
    refSchemasJson(arr){
        let jsonStr='{';
        arr.forEach((item)=>{
            if(item.type==='string')
                jsonStr+='"'+item.key+'"'+':'+'"string",';
            else if(item.type==='integer')
                jsonStr+='"'+item.key+'"'+':'+'0,';
            else if(item.type==='boolean')
                jsonStr+='"'+item.key+'"'+':'+'false,';
            else if(item.type==='Enum')
                jsonStr+='"'+item.key+'"'+':'+'1,';
            else if(item.type==='datetime')
                jsonStr+='"'+item.key+'"'+':'+'"'+os.time(new Date())+'",';
            else if(item.type==='object')
                jsonStr+='"'+item.key+'"'+':'+''+os.refSchemasJson(item.children)+',';
            else if(item.type==='array' && !item.children)
            {
                jsonStr+='"'+item.key+'"'+':'+'[0],';
            }
            else if(item.type==='array' && item.children)
            {
                jsonStr+='"'+item.key+'"'+':'+'['+os.refSchemasJson(item.children)+'],';
            }
        });
        jsonStr=jsonStr.substr(0, jsonStr.length - 1);
        jsonStr+='}';
        //console.log('jsonStr',JSON.stringify(JSON.parse(jsonStr), null, 2))
        return JSON.stringify(JSON.parse(jsonStr), null, 2);
    },
}