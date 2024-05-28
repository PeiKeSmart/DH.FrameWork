﻿namespace Stardust.Models;

/// <summary>系统种类。主流操作系统类型，不考虑子版本</summary>
public enum OSKinds
{
    /// <summary>未知</summary>
    Unknown = 0,

    /// <summary>SmartOS by NewLife</summary>
    SmartOS = 40,

    #region Windows
    /// <summary>WinXP, 5.1.2600</summary>
    WinXP = 51,

    /// <summary>WinXP SP3, 5.1.2600</summary>
    WinXP3 = 53,

    /// <summary>WinXP, 5.2.3790</summary>
    Win2003 = 52,

    /// <summary>Vista, 6.0.6000</summary>
    WinVista = 60,

    /// <summary>Win2008, 6.0.6001</summary>
    Win2008 = 68,

    /// <summary>Win7, 6.1.7600</summary>
    Win7 = 61,

    /// <summary>Win7 SP1, 6.1.7601</summary>
    Win71 = 67,

    /// <summary>Win8, 6.2.9200</summary>
    Win8 = 62,

    /// <summary>Win8.1, 6.3.9200</summary>
    Win81 = 63,

    /// <summary>Win2012, 6.3.9600</summary>
    Win2012 = 64,

    /// <summary>Win2016</summary>
    Win2016 = 66,

    /// <summary>Win2019</summary>
    Win2019 = 69,

    /// <summary>Win2022</summary>
    Win2022 = 72,

    /// <summary>Windows服务器</summary>
    WinServer = 70,

    /// <summary>Win10, 10.0.10240</summary>
    Win10 = 10,

    /// <summary>Win11, 10.0.22000</summary>
    Win11 = 11,
    #endregion

    /// <summary>Alpine</summary>
    Alpine = 90,

    /// <summary>Linux</summary>
    Linux = 100,

    /// <summary>ArchLinux</summary>
    ArchLinux = 101,

    /// <summary>OpenWrt</summary>
    OpenWrt = 102,

    /// <summary>Buildroot</summary>
    Buildroot = 103,

    #region Debian系
    /// <summary>Ubuntu</summary>
    Ubuntu = 110,

    /// <summary>Debian</summary>
    Debian = 111,

    /// <summary>Armbian</summary>
    Armbian = 112,

    /// <summary>树莓派</summary>
    Raspbian = 113,
    #endregion

    #region RedHat系
    /// <summary>红帽</summary>
    RedHat = 120,

    /// <summary>Centos</summary>
    CentOS = 121,

    /// <summary>Fedora</summary>
    Fedora = 122,
    #endregion

    #region 国产DEB系
    /// <summary>深度</summary>
    Deepin = 130,

    /// <summary>统信UOS</summary>
    UOS = 131,

    /// <summary>银河麒麟</summary>
    Kylin = 132,

    /// <summary>优麒麟</summary>
    OpenKylin = 133,

    /// <summary>龙芯操作系统</summary>
    Loongnix = 134,

    /// <summary>红旗Linux</summary>
    RedFlag = 135,

    /// <summary>起点操作系统</summary>
    StartOS = 136,
    #endregion

    #region 国产RPM系
    /// <summary>Alibaba Cloud Linux</summary>
    AlibabaLinux = 140,

    /// <summary>中标麒麟</summary>
    NeoKylin = 141,

    /// <summary>龙蜥</summary>
    Anolis = 142,

    /// <summary>凝思</summary>
    Linx = 143,

    /// <summary>开源欧拉</summary>
    OpenEuler = 144,

    /// <summary>欧拉</summary>
    EulerOS = 145,

    /// <summary>麒麟信安</summary>
    KylinSec = 146,

    /// <summary>普华操作系统</summary>
    PuhuaOS = 147,

    /// <summary>方德操作系统</summary>
    FangdeOS = 148,

    /// <summary>新支点操作系统</summary>
    NewStartOS = 149,
    #endregion

    #region 国产独立
    /// <summary>龙芯嵌入式OS。具备精简、高效、实时特征的工控类操作系统</summary>
    /// <remarks>基于通用Linux内核，利用RT-Linux技术实现实时性</remarks>
    LoongOS = 160,

    #endregion

    /// <summary>MacOS</summary>
    MacOSX = 400,

    /// <summary>Android</summary>
    Android = 500,
}