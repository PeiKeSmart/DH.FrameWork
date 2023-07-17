/**
 * 初始化连接
 * @param {object} option 参数
 * @param {string} option.url 连接的url地址
 * @param {string} option.loggingLevel 日志级别,默认为 Error
 * @param {number} option.delay 延迟连接 默认为3000毫秒
 * @param {function} option.onStarted 启动时触发
 * @param {function} option.onLine 启动时触发
 * @param {function} option.offLine 启动时触发
 * @returns {object} 连接的实例
 */
function initSignalr(option) {
    var config = Object.assign(true, {
        loggingLevel: signalR.LogLevel.Error,
        delay: 3000,
        url: ''
    }, option);

    var connection = new signalR.HubConnectionBuilder()
        .configureLogging(config.loggingLevel)
        .withUrl(config.url, {
            accessTokenFactory: () => config.accessTokenFactory
        })
        .withHubProtocol(new signalR.protocols.msgpack.MessagePackHubProtocol())
        .withAutomaticReconnect([0, 2000, 5000, 10000, 20000, 60000])
        .build();

    connection.onreconnecting(function (info) {
        console.info('----------------------------------signalr-- onreconnecting', info);
    });

    connection.onclose(function (err) {
        location.reload();
        console.info('--------------------------------signalr-- onclose', err);
    });

    connection.on('OnNotify', config.onNotify);

    connection.on('OnLine', config.onLine);

    connection.on('OffLine', config.offLine);

    setTimeout(function () {
        connection.start().then(function (data) {
            option.onStarted && option.onStarted(data);
            console.log("连接成功");
        }).catch(function (error) {
            if (error.toString().indexOf("Forbidden") > -1 || error.toString().indexOf("401") > -1 || error.toString().indexOf("403") > -1) {
                var AccessToken = storage.get("AccessToken");
                var RefreshToken = storage.get("RefreshToken");
                var AccessTokenUtcExpires = storage.get("AccessTokenUtcExpires");
                var RefreshUtcExpires = storage.get("RefreshUtcExpires");
                var Remember = storage.get("remember");

                var now = new Date().getTime(); // 获取当前时间戳

                var seconds = Math.floor((RefreshUtcExpires - now) / 1000); //  计算时间戳与当前时间之间的秒数
                var seconds1 = Math.floor((AccessTokenUtcExpires - now) / 1000); //  计算时间戳与当前时间之间的秒数

                if (AccessTokenUtcExpires === null || RefreshUtcExpires === null || seconds < 0) {
                    $.ajax
                        ({
                            url: logoutUrl,  // 退出登录
                            dataType: 'json',
                            method: 'POST',
                            data:
                            {

                            },
                            success: function (data) {
                                if (data.code == 0) {
                                    storage.remove("AccessToken");
                                    storage.remove("RefreshToken");
                                    storage.remove("AccessTokenUtcExpires");
                                    storage.remove("RefreshUtcExpires");
                                    storage.remove("remember");

                                    window.location.href = loginUrl;
                                }
                            }
                        })
                    return;
                }

                $.ajax
                    ({
                        url: refreshTokenUrl,  // 刷新Token
                        dataType: 'json',
                        method: 'POST',
                        data:
                        {
                            RefreshToken: RefreshToken,
                            RefreshExpireMinutes: Remember
                        },
                        success: function (res) {
                            console.log(res);

                            if (!res.success) {
                                $.ajax
                                    ({
                                        url: memberGetAccessToken,  // 通过会员登录信息获取
                                        dataType: 'json',
                                        method: 'POST',
                                        data:
                                        {

                                        },
                                        success: function (res) {
                                            if (!res.success) {
                                                $.ajax
                                                    ({
                                                        url: logoutUrl,  // 退出登录
                                                        dataType: 'json',
                                                        method: 'POST',
                                                        data:
                                                        {

                                                        },
                                                        success: function (data) {
                                                            if (data.code == 0) {
                                                                storage.remove("AccessToken");
                                                                storage.remove("RefreshToken");
                                                                storage.remove("AccessTokenUtcExpires");
                                                                storage.remove("RefreshUtcExpires");
                                                                storage.remove("remember");

                                                                window.location.href = loginUrl;
                                                            }
                                                        }
                                                    })
                                                return;
                                            }
                                            else {
                                                var seconds2 = res.data.RefreshUtcExpires - now; // 计算时间戳与当前时间之间的毫秒数

                                                storage.set("AccessToken", res.data.AccessToken, seconds2);
                                                storage.set("RefreshToken", res.data.RefreshToken, seconds2);
                                                storage.set("AccessTokenUtcExpires", res.data.AccessTokenUtcExpires, seconds2);
                                                storage.set("RefreshUtcExpires", res.data.RefreshUtcExpires, seconds2);

                                                storage.set("remember", Remember, seconds2);
                                            }
                                        }
                                    })
                            }
                            else {
                                var seconds2 = res.data.RefreshUtcExpires - now; // 计算时间戳与当前时间之间的毫秒数

                                storage.set("AccessToken", res.data.AccessToken, seconds2);
                                storage.set("RefreshToken", res.data.RefreshToken, seconds2);
                                storage.set("AccessTokenUtcExpires", res.data.AccessTokenUtcExpires, seconds2);
                                storage.set("RefreshUtcExpires", res.data.RefreshUtcExpires, seconds2);

                                storage.set("remember", Remember, seconds2);
                            }
                        }
                    })
            }

            console.error(error.toString());
        });
    }, option.delay);

    return connection;
}
