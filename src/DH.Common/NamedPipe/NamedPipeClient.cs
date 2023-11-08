using System.IO.Pipes;
using System.Text;

namespace DH.NamedPipe;

/// <summary>
/// 客户端管道
/// </summary>
class NamedPipeClient {
    private NamedPipeClientStream Client { get; set; }
    private byte[] Buffer { get; set; }
    private bool IsReadEnd { get; set; }
    private bool IsWriteEnd { get; set; }
    private int BufferSize = 10;
    private StringBuilder InputStr { get; set; }
    private string OutputStr { get; set; }

    public NamedPipeClient(string serverName, string serverHost)
    {
        Client = new NamedPipeClientStream(serverHost, serverName);
        this.Buffer = new byte[BufferSize];
        this.InputStr = new StringBuilder();
    }

    public string Request(string outPutStr)
    {
        this.OutputStr = outPutStr.Trim();
        this.Client.Connect();

        for (int i = 0; i < this.BufferSize; i++)
            this.OutputStr = " " + this.OutputStr;
        using (MemoryStream memoryStream = new MemoryStream())
        using (StreamWriter writer = new StreamWriter(memoryStream))
        {

            writer.Write(OutputStr);
            writer.Flush();
            memoryStream.Flush();
            int length = 0;
            memoryStream.Position = 0;
            while ((length = memoryStream.Read(Buffer, 0, Buffer.Length)) != 0)
            {
                this.Client.Write(Buffer, 0, length);
            }
            this.Client.WaitForPipeDrain();
            this.Client.Flush();
        }

        AsyncState asyncState = new AsyncState()
        {
            Buffer = new byte[BufferSize],
            EvtHandle = new ManualResetEvent(false),
            Stream = this.Client
        };

        IAsyncResult readAsyncResult = this.Client.BeginRead(this.Buffer, 0, this.Buffer.Length, new AsyncCallback(ReadCallback), asyncState);
        asyncState.EvtHandle.WaitOne();

        this.Client.Close();
        this.Client.Dispose();

        return this.InputStr.ToString().Replace("\0", " ").Trim();
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
}