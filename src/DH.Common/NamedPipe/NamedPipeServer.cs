using System.IO.Pipes;
using System.Text;

namespace DH.NamedPipe;

public class AsyncState {
    public byte[] Buffer { get; set; }
    public PipeStream Stream { get; set; }
    public MemoryStream MemoryStream { get; set; }
    public ManualResetEvent EvtHandle { get; set; }
}

/// <summary>
/// 服务端管道
/// </summary>
class NamedPipeServer {
    private NamedPipeServerStream Server { get; set; }
    private byte[] Buffer;
    private int BufferSize = 10;
    private StringBuilder InputStr { get; set; }

    public NamedPipeServer(string name)
    {
        this.Server = new NamedPipeServerStream(name,
            PipeDirection.InOut,
            1,
            PipeTransmissionMode.Message,
            PipeOptions.None);
        this.InputStr = new StringBuilder();
        Buffer = new byte[BufferSize];
    }

    public void Start()
    {
        while (true)
        {
            this.Server.WaitForConnection();

            AsyncState asyncState = new AsyncState()
            {
                Buffer = new byte[BufferSize],
                EvtHandle = new ManualResetEvent(false),
                Stream = this.Server
            };

            //异步读取，并阻塞线程，读取结束取消阻塞
            this.Server.BeginRead(this.Buffer, 0, this.Buffer.Length, new AsyncCallback(ReadCallback), asyncState);
            asyncState.EvtHandle.WaitOne();

            //获取输出字符串
            string outStr = "";
            if (Readed != null) outStr = this.Readed.Invoke(this.InputStr.ToString().Replace("\0", " ").Trim()).Trim();
            this.InputStr.Clear();
            for (int i = 0; i < this.BufferSize; i++)
                outStr = " " + outStr;

            //输出到内存流，然后内存流转写字节码到服务流中
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter write = new StreamWriter(memoryStream))
            {
                write.Write(outStr);
                write.Flush();
                memoryStream.Flush();
                int length = 0;
                memoryStream.Position = 0;
                byte[] tmp = new byte[BufferSize];
                while (((length = memoryStream.Read(tmp, 0, this.Buffer.Length)) != 0))
                {
                    Server.Write(tmp, 0, length);
                }
            }

            Server.WaitForPipeDrain();
            Server.Flush();
            Server.Disconnect();
        }
    }

    private void ReadCallback(IAsyncResult arg)
    {
        AsyncState state = arg.AsyncState as AsyncState;
        int length = state.Stream.EndRead(arg);

        if (length > 0)
        {
            byte[] buffer;
            if (length == BufferSize) buffer = state.Buffer;
            else
            {
                buffer = new byte[length];
                Array.Copy(state.Buffer, 0, buffer, 0, length);
            }

            if (state.MemoryStream == null) state.MemoryStream = new MemoryStream();
            state.MemoryStream.Write(buffer, 0, buffer.Length);
            state.MemoryStream.Flush();
        }
        if (length < BufferSize)
        {
            state.MemoryStream.Position = 0;
            using (StreamReader reader = new StreamReader(state.MemoryStream))
            {
                this.InputStr.Append(reader.ReadToEnd());
            }
            state.MemoryStream.Close();
            state.MemoryStream.Dispose();
            state.MemoryStream = null;

            state.EvtHandle.Set();
        }
        else
        {
            Array.Clear(state.Buffer, 0, BufferSize);
            //再次执行异步读取操作
            state.Stream.BeginRead(state.Buffer, 0, BufferSize, new AsyncCallback(ReadCallback), state);
        }
    }

    public event Func<string, string> Readed;
}