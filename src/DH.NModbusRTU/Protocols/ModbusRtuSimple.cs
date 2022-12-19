﻿using System.IO.Ports;
using NewLife.Log;

namespace NewLife.Serial.Protocols;

/// <summary>简易版ModbusRTU</summary>
public class ModbusRtuSimple
{
    #region 属性
    /// <summary>端口</summary>
    public String PortName { get; set; }

    /// <summary>波特率</summary>
    public Int32 Baudrate { get; set; } = 9600;

    /// <summary>字节超时。数据包间隔，默认20ms</summary>
    public Int32 ByteTimeout { get; set; } = 20;

    /// <summary>性能追踪器</summary>
    public ITracer Tracer { get; set; }

    private SerialPort _port;
    #endregion

    #region 方法
    /// <summary>初始化。传入配置字符串</summary>
    /// <param name="config"></param>
    public virtual void Init(String config)
    {
        var ss = config.SplitAsDictionary("=", ";", true);
        if (ss.TryGetValue("PortName", out var str))
            PortName = str;
        else if (ss.TryGetValue("Address", out str))
            PortName = str;

        if (ss.TryGetValue("Baudrate", out str)) Baudrate = str.ToInt();
    }

    /// <summary>打开</summary>
    public virtual void Open()
    {
        if (_port == null)
        {
            var p = new SerialPort(PortName, Baudrate)
            {
                ReadTimeout = 3_000,
                WriteTimeout = 3_000
            };
            p.Open();
            _port = p;

            WriteLog("Open {0} Baudrate={1}", PortName, Baudrate);
        }
    }

    /// <summary>发送两字节命令，并接收返回</summary>
    /// <param name="host"></param>
    /// <param name="code"></param>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual Byte[] SendCommand(Byte host, Byte code, UInt16 address, UInt16 value)
    {
        Open();

        var cmd = new Byte[8];
        cmd[0] = host;
        cmd[1] = code;
        cmd[2] = (Byte)(address >> 8);
        cmd[3] = (Byte)(address & 0xFF);
        cmd[4] = (Byte)(value >> 8);
        cmd[5] = (Byte)(value & 0xFF);

        var crc = Crc(cmd, 0, cmd.Length - 2);
        cmd[6] = (Byte)(crc & 0xFF);
        cmd[7] = (Byte)(crc >> 8);

        using var span = Tracer?.NewSpan("modbus:SendCommand", cmd.ToHex());

        WriteLog("{0}=> {1}", PortName, cmd.ToHex("-"));
        _port.Write(cmd, 0, cmd.Length);
        Thread.Sleep(10);

        // 串口速度较慢，等待收完数据
        WaitMore(_port);

        try
        {
            //using var span = Tracer?.NewSpan("modbus:ReceiveCommand");

            var rs = new Byte[32];
            var c = _port.Read(rs, 0, rs.Length);
            rs = rs.ReadBytes(0, c);
            WriteLog("{0}<= {1}", PortName, rs.ToHex("-"));

            if (span != null) span.Tag += Environment.NewLine + rs.ToHex("-");

            if (rs.Length < 2 + 2) return null;

            // 校验Crc
            crc = Crc(rs, 0, rs.Length - 2);
            var crc2 = rs.ToUInt16(rs.Length - 2);
            if (crc != crc2) WriteLog("Crc Error {0:X4}!={1:X4} !", crc, crc2);

            return rs.ReadBytes(2, rs.Length - 2 - 2);
        }
        catch (Exception ex)
        {
            span?.SetError(ex, null);
            if (ex is TimeoutException) return null;
            throw;
        }
    }

    void WaitMore(SerialPort sp)
    {
        var ms = ByteTimeout;
        var end = DateTime.Now.AddMilliseconds(ms);
        var count = sp.BytesToRead;
        while (sp.IsOpen && end > DateTime.Now)
        {
            //Thread.SpinWait(1);
            Thread.Sleep(ms);
            if (count != sp.BytesToRead)
            {
                end = DateTime.Now.AddMilliseconds(ms);
                count = sp.BytesToRead;
            }
        }
    }

    /// <summary>获取串口列表</summary>
    /// <returns></returns>
    public static String[] GetPortNames() => SerialPort.GetPortNames();
    #endregion

    #region 核心业务
    /// <summary>写入单线圈，0x05</summary>
    /// <param name="host">主机。一般是1</param>
    /// <param name="address">地址。例如0x0002</param>
    /// <param name="value">值。一般是 0xFF00/0x0000</param>
    /// <returns></returns>
    public Byte[] WriteSingleCoil(Byte host, UInt16 address, UInt16 value)
    {
        using var span = Tracer?.NewSpan("modbus:WriteSingleCoil", $"{host} {address:X4} {value:X4}");

        var rs = SendCommand(host, 0x05, address, value);
        if (rs == null || rs.Length <= 0) return null;

        return rs.ReadBytes(1, rs[0]);
    }

    /// <summary>写入单寄存器，0x06</summary>
    /// <param name="host">主机。一般是1</param>
    /// <param name="address">地址。例如0x0002</param>
    /// <param name="value">数值</param>
    /// <returns></returns>
    public Byte[] WriteRegister(Byte host, UInt16 address, UInt16 value)
    {
        using var span = Tracer?.NewSpan("modbus:WriteRegister", $"{host} {address:X4} {value:X4}");

        var rs = SendCommand(host, 0x06, address, value);
        if (rs == null || rs.Length <= 0) return null;

        return rs;
    }

    /// <summary>读取线圈，0x01</summary>
    /// <param name="host">主机。一般是1</param>
    /// <param name="address">地址。例如0x0002</param>
    /// <param name="count">线圈个数</param>
    /// <returns></returns>
    public Byte[] ReadCoil(Byte host, UInt16 address, UInt16 count)
    {
        using var span = Tracer?.NewSpan("modbus:ReadCoil", $"{host} {address:X4} {count:X4}");

        var rs = SendCommand(host, 0x01, address, count);
        if (rs == null || rs.Length <= 0) return null;

        return rs.ReadBytes(1, rs[0]);
    }

    /// <summary>读取保持寄存器，0x03</summary>
    /// <param name="host">主机。一般是1</param>
    /// <param name="address">地址。例如0x0002</param>
    /// <param name="count">寄存器个数。每个寄存器2个字节</param>
    /// <returns></returns>
    public Byte[] ReadHoldingRegister(Byte host, UInt16 address, UInt16 count)
    {
        using var span = Tracer?.NewSpan("modbus:ReadHoldingRegister", $"{host} {address:X4} {count:X4}");

        var rs = SendCommand(host, 0x03, address, count);
        if (rs == null || rs.Length <= 0) return null;

        return rs.ReadBytes(1, rs[0]);
    }
    #endregion

    #region 日志
    /// <summary>日志</summary>
    public ILog Log { get; set; }

    /// <summary>写日志</summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    public void WriteLog(String format, params Object[] args) => Log?.Info(format, args);
    #endregion

    #region CRC
    private static readonly UInt16[] crc_ta = new UInt16[16] { 0x0000, 0xCC01, 0xD801, 0x1400, 0xF001, 0x3C00, 0x2800, 0xE401, 0xA001, 0x6C00, 0x7800, 0xB401, 0x5000, 0x9C01, 0x8801, 0x4400, };

    /// <summary>Crc校验</summary>
    /// <param name="data"></param>
    /// <param name="offset">偏移</param>
    /// <param name="count">数量</param>
    /// <returns></returns>
    public static UInt16 Crc(Byte[] data, Int32 offset, Int32 count = -1)
    {
        if (data == null || data.Length < 1) return 0;

        UInt16 u = 0xFFFF;
        Byte b;

        if (count == 0) count = data.Length - offset;

        for (var i = offset; i < count; i++)
        {
            b = data[i];
            u = (UInt16)(crc_ta[(b ^ u) & 15] ^ (u >> 4));
            u = (UInt16)(crc_ta[((b >> 4) ^ u) & 15] ^ (u >> 4));
        }

        return u;
    }
    #endregion
}