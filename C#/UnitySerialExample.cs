/*
UnitySerialExample.cs
tested in Unity 2021.2.16f1

by Beau V. at SCPS Unlimited
created 8/23/2023
*/

using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnitySerialExample : MonoBehaviour
{
    private SerialPort port;
    private float lastSerialSend = 0.0f;
    public static string GuiConsoleLine = "";

    IEnumerator Start()
    {
        try
        {
            string lastPort = "";
            foreach(string str in SerialPort.GetPortNames())
            {
                WriteLineToGuiConsole(string.Format("Existing COM port: {0}", str));
                lastPort = str;
            }
            port = new SerialPort("\\\\.\\" + lastPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            WriteLineToGuiConsole("SerialPort connected to " + lastPort);
            //LightUp();
        }
        catch(Exception e)
        {
            WriteLineToGuiConsole("SerialPort fail " + e.ToString());
        }
    }
    void Update()
    {
        PollSerial();
    }

    private void PollSerial()
    {
        if(port.IsOpen)
        {
            string message = "";
            try
            {
                message = port.ReadLine();
                //message += (char)port.ReadByte();
                if(message.Length > 0)
                {
                    WriteLineToGuiConsole(message);
                    GuiConsoleLine += "\n" + message;
                }
            }
            catch(Exception e)
            {
                GuiConsoleLine += "\n" + e.ToString();
                WriteLineToGuiConsole(e.ToString());
            }
        }
    }

    public static void WriteLineToGuiConsole(string s)
    {
        if(GuiConsoleLine.Length > 1024)
            GuiConsoleLine = "";
        GuiConsoleLine += "\n " + s;
        print("wrote to gui console: " + s);
        /*
        string path = "Assets/Resources/debug.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
        */
    }

    void OnApplicationQuit()
    {
        if(port != null)
            port.Close();
    }
    ~UnitySerialExample()
    {
        if(port != null)
            port.Close();
    }
}
