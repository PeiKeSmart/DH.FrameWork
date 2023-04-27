var beginTime = new Date();
var endTime = new Date();
var axiosrequerst = [];
var loads = null;

function showLoad() {
    loads = ELEMENT.Loading.service({
        lock: true,
        text: '努力加载中...',
    });
}

function hideLoad(requsturl) {
    var date = axiosrequerst.valueOf(requsturl);
    if (date != -1) {
        axiosrequerst.splice(date, 1);
    }
    if (axiosrequerst.length < 1) {
        loads.close();
    }
}

axios.interceptors.request.use(
    function (config) {
        beginTime = new Date();
        showLoad();
        vm.headerTable.forEach((item) => {
            if (item.key) {
                config.headers[item.key] = item.value;
            }
        })
        vm.authorizTable.forEach((item) => {
            if (item.key) {
                config.headers[item.key] = item.value;
            }
        })
        const authoriz = os.get('global-authorize');
        const header = os.get('global-header');
        if (authoriz) {
            authoriz.forEach((item) => {
                if (item.status) {
                    config.headers[item.key] = item.value;
                }
            })
        }
        if (header) {
            header.forEach((item) => {
                if (item.status) {
                    config.headers[item.key] = item.value;
                }
            })
        }
        config.headers['Content-Type'] = vm.api.type;
        axiosrequerst.push(config.url);
        return config;
    },
    function (error) {
        endTime = new Date();
        hideLoad(error.response.config.url);
        return error;
    }
);
axios.interceptors.response.use(
    function (response) {
        endTime = new Date();
        if(response){
            hideLoad(response.config.url);
        }
        else{
            loads.close();
        }
        return response;
    },
    function (error) {
        endTime = new Date();
        if(error.response){
            hideLoad(error.response.config.url);
        }
        else{
            loads.close();
        }
        return error;
    }
);
const vm = new Vue({
    el: "#app",
    data() {
        return {
            theme: true,
            menuKey: undefined,
            settingDig:{
                visible: false,
                settingForm:{
                    urlChar:true,
                    getSelect:false
                }
            },
            globalDig: {
                visible: false,
                tabsName: 'first',
                authoriz: [{
                    key: '',
                    value: '',
                    status: true
                }],
                header: [{
                    key: '',
                    value: '',
                    status: true
                }]
            },
            editableTabsValue: '2',
            editableTabs: [{
                title: '添加天气',
                name: '1',
            }, {
                title: '修改天气',
                name: '2',
            }],
            methodArr: [{
                value: 'GET',
            }, {
                value: 'POST',
            }, {
                value: 'PUT',
            }, {
                value: 'DELETE',
            },],
            methodVal: 'GET',
            api: {
                url: '',
                tips: '',
                menu: [],
                sourceMenu: [],
                radio: 4,
                type: 'application/json',
                isParam: false,
                isBody: false,
                body: '',
                jsonBody: ''
            },
            apiVal: '',
            activeName: 'first',
            activeFooterName: 'first',
            paramTable: [{
                key: '',
                value: '',
                summary: '',
                status: true
            }],
            authorizTable: [{
                key: '',
                value: '',
                status: true
            }],
            headerTable: [{
                key: '',
                value: '',
                status: true
            }],
            formTable: [{
                key: '',
                value: '',
                status: true
            }],
            returnTable:[],
            requestTable: [],
            requestHeader: {},
            responseHeader: {},
            response: {
                status: 0,
                time: 0,
                data: undefined
            },
            openeds: [],
            stlcalOpends: [],
            version: {
                list: [],
                index: 0,
                res: undefined
            },
            tableHeight:0
        }
    },
    watch: {
        menuKey: {
            handler() {
                let _menu = [];
                this.api.sourceMenu.forEach((item) => {
                    if (item.group.toLowerCase().includes(this.menuKey) || (item.summary && item.summary.includes(this.menuKey))) {
                        _menu.push(item);
                    }
                });
                this.api.menu = _menu;
            }
        },
        theme: {
            handler() {
                if (this.theme) {
                    document.querySelector('body').classList.add("dark");
                } else {
                    document.querySelector('body').classList.remove("dark");
                }
                os.set('theme', this.theme)
            }
        },
        paramTable: {
            handler() {
                let par = this.encodeSearchParams(this.paramTable)
                if (par) {
                    this.apiVal = this.api.url + '?' + par
                } else {
                    this.apiVal = this.api.url
                }
            },
            deep: true
        },
        'api.body': {
            handler() {
                try {
                    if (!this.api.body) {
                        this.api.jsonBody = ''
                    }
                    const jsonObj = JSON.parse(this.api.body);
                    this.api.jsonBody = JSON.stringify(jsonObj, null, 2)
                } catch (error) {
                }
            },
        }
    },
    mounted() {
        const vtheme = os.get('theme')
        if (vtheme == null || vtheme) {
            document.querySelector('body').classList.add("dark")
        } else {
            this.theme = vtheme;
        }
        this.initVersion();
        this.$nextTick(()=>{
            this.tableHeight=document.body.clientHeight-673;
            if(this.tableHeight<200){
                this.tableHeight=220;
            }
        })
    },
    methods: {
        initVersion() {
            const that = this
            axios(os.path() + '/api-config').then(function (response) {
                //console.log('config',response)
                that.version.list = response.data.urls;
                that.initApi(0);
            })
        },
        initApi(index) {
            this.api.menu = []
            const apiJson = this.version.list[index];
            //console.log('apiJson',apiJson)
            const that = this
            const menu = [], group = []
            axios(os.path() + apiJson.url).then(function (response) {
                const res = response.data;
                that.version.res = res;
                that.api.tips = res.info.title;
                // setting element menu Array
                for (let k in res.paths) {
                    for (let item in res.paths[k]) {
                        group.push(res.paths[k][item].tags[0])
                        menu.push({
                            tag: res.paths[k][item].tags[0],
                            method: item,
                            url: k,
                            summary: res.paths[k][item].summary,
                            parameters: res.paths[k][item].parameters,
                            requestBody: res.paths[k][item].requestBody,
                            responseBody:res.paths[k][item].responses
                        })
                    }
                }
                os.unique(group).forEach((item, index) => {
                    that.openeds.push(index.toString())
                    let api = []
                    menu.forEach((row) => {
                        if (row.tag === item) {
                            api.push({
                                method: row.method.toUpperCase(),
                                url: row.url,
                                summary: row.summary,
                                parameters: row.parameters,
                                requestBody: row.requestBody,
                                responseBody:row.responseBody
                            })
                        }
                    })
                    let summary = undefined
                    // tags group 
                    if (res.tags) {
                        res.tags.forEach((tag) => {
                            if (tag.name === item) {
                                summary = tag.description;
                            }
                        })
                    }
                    that.api.menu.push({group: item, summary: summary, api})
                })
                //console.log('api-menu',that.api.menu)
                that.stlcalOpends = that.openeds;
                that.api.sourceMenu = that.api.menu;
            })

        },
        versionBar(item, index) {
            this.version.index = index;
            this.initApi(index)
        },
        menuClick(item) {
            let globalSetting = os.get('globalSetting');
            if (!globalSetting) {
                globalSetting=this.settingDig.settingForm;
            }
            this.methodVal = item.method;
            const nurl = item.url.replace(/\{[^\}]+\}/,'');
            this.api.url = os.path() + (globalSetting.urlChar?nurl.toLowerCase():nurl);
            this.api.tips = item.summary ? item.summary : this.api.url;
            this.apiVal = os.path() + (globalSetting.urlChar?item.url.toLowerCase():item.url);
            // setting get param
            this.paramTable = [];
            this.requestTable=[];
            this.returnTable=[];
            this.response.data = '';
            this.api.isParam = false;
            this.api.isBody = false;
            this.api.body = undefined;
            //console.log('menu-item',item)
            if (item.parameters) {
                this.paramTable=os.parameters(item,this.version.res);
                this.api.isParam = true;
            }
            const requestRes=os.requestBody(item,this.version.res)
            if(requestRes.data){
                this.api.isBody = true;
                this.api.body=requestRes.data;
                this.requestTable=requestRes.list;
            }
            const responseRes=os.responsesBody(item, this.version.res)
            if(responseRes.data){
                this.returnTable=responseRes.data;
                this.response.data=responseRes.example;
            }else{
                this.response.data=responseRes.tip;
            }

        },
        openMenu() {
            if (this.openeds.length > 0) {
                this.openeds = [];
            } else {
                this.openeds = this.stlcalOpends;
            }
        },
        addParams() {
            this.paramTable.push({key: '', value: '', status: true})
        },
        removeParams(row) {
            this.paramTable.splice(row, 1);
        },
        addAuthoriz() {
            this.authorizTable.push({key: '', value: '', status: true})
        },
        removeAuthoriz(row) {
            this.authorizTable.splice(row, 1);
        },
        addHeader() {
            this.headerTable.push({key: '', value: '', status: true})
        },
        removeHeader(row) {
            this.headerTable.splice(row, 1);
        },
        addForm() {
            this.formTable.push({key: '', value: '', status: true})
        },
        removeForm(row) {
            this.formTable.splice(row, 1);
        },
        addGlobalAuthoriz() {
            this.globalDig.authoriz.push({key: '', value: '', status: true})
        },
        removeGlobalAuthoriz(row) {
            this.globalDig.authoriz.splice(row, 1);
        },
        addGlobalHeader() {
            this.globalDig.header.push({key: '', value: '', status: true})
        },
        removeGlobalHeader(row) {
            this.globalDig.header.splice(row, 1);
        },
        openSetting(){
            const globalSetting = os.get('globalSetting');
            if (globalSetting) {
                this.settingDig.settingForm=globalSetting;
            }
            this.settingDig.visible = true;
        },
        onSettingSubmit(){
            os.set('globalSetting',this.settingDig.settingForm);
            this.$message.success('保存成功~');
            this.settingDig.visible = false;
        },
        openGlobalSetting() {
            const authoriz = os.get('global-authorize');
            const header = os.get('global-header');
            if (authoriz) {
                this.globalDig.authoriz = authoriz
            }
            if (header) {
                this.globalDig.header = header
            }
            this.globalDig.visible = true;
        },
        saveGlobalSetting() {
            let isAuthorize = false, isHeader = false
            this.globalDig.authoriz.forEach((item) => {
                if (!item.key || !item.value) {
                    isAuthorize = true
                }
            })
            if (isAuthorize) {
                this.$message.error('全局认证信息不能为空~');
                return
            }
            this.globalDig.header.forEach((item) => {
                if (!item.key || !item.value) {
                    isHeader = true
                }
            })
            if (isHeader) {
                this.$message.error('全局Header信息不能为空~');
                return
            }
            os.set('global-authorize', this.globalDig.authoriz);
            os.set('global-header', this.globalDig.header);
            this.globalDig.visible = false;
        },
        encodeSearchParams(obj) {
            const params = []
            obj.forEach((item) => {
                if ((item.key || item.value) && item.status) {
                    params.push(item.key + '=' + item.value)
                }
            })
            return params.length > 0 ? params.join('&') : ''
        },
        apiTypeCommand(command) {
            this.api.type = command
        },
        tabsClick(tab, event) {
            //console.log(tab.index);
        },
        addTab(targetName) {
            let newTabName = ++this.tabIndex + '';
            this.editableTabs.push({
                title: 'New Tab',
                name: newTabName,
                content: 'New Tab content'
            });
            this.editableTabsValue = newTabName;
        },
        removeTab(targetName) {
            let tabs = this.editableTabs;
            let activeName = this.editableTabsValue;
            if (activeName === targetName) {
                tabs.forEach((tab, index) => {
                    if (tab.name === targetName) {
                        let nextTab = tabs[index + 1] || tabs[index - 1];
                        if (nextTab) {
                            activeName = nextTab.name;
                        }
                    }
                });
            }
            this.editableTabsValue = activeName;
            this.editableTabs = tabs.filter(tab => tab.name !== targetName);
        },
        send() {
            /*console.log('请求方式：', this.methodVal)
            console.log('请求地址：', this.apiVal)
            console.log('请求参数-Param：', this.paramTable)
            console.log('请求参数-Authoriz：', this.authorizTable)
            console.log('请求参数-Headers：', this.headerTable)
            console.log('请求参数-Form.Data：', this.formTable)
            console.log('请求参数-Body-Type：', this.api.radio)
            console.log('请求参数-Request-Type：', this.api.type)
            console.log('请求参数-Body：', this.api.body)*/
            const that = this
            if (this.methodVal === 'GET') {
                axios(this.apiVal).then(function (response) {
                    const res = JSON.parse(JSON.stringify(response))
                    that.response.time = (endTime.getTime() - beginTime.getTime()) / 1000
                    that.requestHeader = res.config.headers
                    if (res.response) {
                        that.responseHeader = res.response.headers
                        that.response.status = res.response.status
                        that.response.data = undefined
                    } else {
                        that.response.status = res.status
                        that.responseHeader = res.headers
                        try {
                            that.response.data = JSON.stringify(res.data, null, 2)
                        } catch (error) {
                            that.response.data = res.data
                        }
                    }
                });
            }
            if (this.methodVal == 'POST') {
                let param = undefined
                if (os.isJSON(this.api.body)) {
                    param = JSON.parse(this.api.body)
                } else {
                    param = this.api.body
                }
                axios.post(this.apiVal, param)
                    .then(function (response) {
                        const res = JSON.parse(JSON.stringify(response))
                        that.requestHeader = res.config.headers
                        that.response.time = (endTime.getTime() - beginTime.getTime()) / 1000
                        if (res.response) {
                            that.responseHeader = res.response.headers
                            that.response.status = res.response.status
                            that.response.data = undefined
                        } else {
                            that.response.status = res.status
                            that.responseHeader = res.headers
                            try {
                                that.response.data = JSON.stringify(res.data, null, 2)
                            } catch (error) {
                                that.response.data = res.data
                            }
                        }
                    })
                    .catch(function (error) {
                        that.$message.error(error.message);
                    });
            }
            if (this.methodVal == 'PUT') {
                let param = undefined
                if (os.isJSON(this.api.body)) {
                    param = JSON.parse(this.api.body)
                } else {
                    param = this.api.body
                }
                axios.put(this.apiVal, param)
                    .then(function (response) {
                        const res = JSON.parse(JSON.stringify(response))
                        that.requestHeader = res.config.headers
                        that.response.time = (endTime.getTime() - beginTime.getTime()) / 1000
                        if (res.response) {
                            that.responseHeader = res.response.headers
                            that.response.status = res.response.status
                            that.response.data = undefined
                        } else {
                            that.response.status = res.status
                            that.responseHeader = res.headers
                            try {
                                that.response.data = JSON.stringify(res.data, null, 2)
                            } catch (error) {
                                that.response.data = res.data
                            }
                        }
                    })
                    .catch(function (error) {
                        that.$message.error(error.message);
                    });
            }
            if (this.methodVal == 'DELETE') {
                let param = undefined
                if (os.isJSON(this.api.body)) {
                    param = JSON.parse(this.api.body)
                } else {
                    param = this.api.body
                }
                axios({
                    method: "delete",
                    url: this.apiVal,
                    data: param,
                })
                    .then((response) => {
                        const res = JSON.parse(JSON.stringify(response))
                        that.requestHeader = res.config.headers
                        that.response.time = (endTime.getTime() - beginTime.getTime()) / 1000
                        if (res.response) {
                            that.responseHeader = res.response.headers
                            that.response.status = res.response.status
                            that.response.data = undefined
                        } else {
                            that.response.status = res.status
                            that.responseHeader = res.headers
                            try {
                                that.response.data = JSON.stringify(res.data, null, 2)
                            } catch (error) {
                                that.response.data = res.data
                            }
                        }
                    })
                    .catch((error) => {
                        that.$message.error(error.message);
                    });
            }
        }
    }
})