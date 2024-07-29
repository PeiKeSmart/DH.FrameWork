using System.ComponentModel;
using NewLife.IoT;
using NewLife.IoT.Drivers;
using NewLife.Log;

namespace NewLife.Schneider.Drivers;

/// <summary>
/// 施耐德PLC驱动
/// </summary>
[Driver("SchneiderPLC")]
[DisplayName("施耐德PLC")]
public class SchneiderDriver : ModbusTcpDriver, ILogFeature, ITracerFeature
{
    /// <summary>建立连接，打开驱动</summary>
    /// <param name="device"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public override INode Open(IDevice device, IDriverParameter parameter)
    {
        var modbusNode = base.Open(device, parameter);
        if (modbusNode is ModbusNode node && Modbus != null)
        {
            Modbus.Open();
        }

        return modbusNode;
    }
}