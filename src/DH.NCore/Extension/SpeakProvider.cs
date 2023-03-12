﻿using System.Reflection;
using NewLife.Log;
using NewLife.Reflection;

namespace NewLife.Extension;

class SpeakProvider
{
    private static readonly String typeName = "System.Speech.Synthesis.SpeechSynthesizer";
    private Type _type;

    public SpeakProvider()
    {
        try
        {
            //_type = typeName.GetTypeEx(true);
            _type = Type.GetType(typeName);
            if (_type == null)
            {
                var asm = Assembly.Load("System.Speech");
                _type = asm?.GetType(typeName);
            }
        }
        catch (Exception ex)
        {
            XTrace.WriteException(ex);
        }

        if (_type == null) XTrace.WriteLine("找不到语音库System.Speech，需要从nuget引用");
    }

    private Object synth;
    void EnsureSynth()
    {
        if (synth == null)
        {
            try
            {
                synth = _type.CreateInstance(new Object[0]);
                synth.Invoke("SetOutputToDefaultAudioDevice", new Object[0]);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
                _type = null;
            }
        }
    }

    public void Speak(String value)
    {
        if (_type == null) return;

        EnsureSynth();
        synth?.Invoke("Speak", value);
    }

    public void SpeakAsync(String value)
    {
        if (_type == null) return;

        EnsureSynth();
        synth?.Invoke("SpeakAsync", value);
    }

    /// <summary>
    /// 停止话音播报
    /// </summary>
    public void SpeakAsyncCancelAll()
    {
        if (_type == null) return;

        EnsureSynth();
        synth?.Invoke("SpeakAsyncCancelAll");
    }
}