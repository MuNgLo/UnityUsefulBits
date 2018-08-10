using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchLight : MonoBehaviour
{
	TwitchCredentials cred = new TwitchCredentials(); // This is just a class with Oauth and Token so it gets hidden from screen. You could just use the strings bellow instead.
    //public string oauth;
    //public string nickName;
    public bool showBuffer = false;
    public string channelName;
    private string server = "irc.twitch.tv";
    private int port = 6667;

    

    //event(buffer).
    public class MsgEvent : UnityEngine.Events.UnityEvent<string> { }
    public MsgEvent messageRecievedEvent = new MsgEvent();

    private string buffer = string.Empty;
    private bool stopThreads = false;
    private Queue<string> commandQueue = new Queue<string>();
    private List<string> recievedMsgs = new List<string>();
    private System.Threading.Thread inProc, outProc;
    private void StartIRC()
    {
        Debug.Log($"[TwitchLight] StartIRC()");
        System.Net.Sockets.TcpClient sock = new System.Net.Sockets.TcpClient();
        sock.Connect(server, port);
        if (!sock.Connected)
        {
            Debug.Log("Failed to connect!");
            return;
        }
        var networkStream = sock.GetStream();
        var input = new System.IO.StreamReader(networkStream);
        var output = new System.IO.StreamWriter(networkStream);



        //Send PASS & NICK.
        //output.WriteLine("PASS " + oauth);
        //oauth: asdasd234asd234ad234asds23
        //output.WriteLine($"PASS oauth:{oauth}");
        output.WriteLine($"PASS oauth:{cred._oauth}");
        output.WriteLine($"NICK {cred._username}");
        output.Flush();

        //output proc
        outProc = new System.Threading.Thread(() => IRCOutputProcedure(output));
        outProc.Start();
        //input proc
        inProc = new System.Threading.Thread(() => IRCInputProcedure(input, networkStream));
        inProc.Start();
    }
    private void IRCInputProcedure(System.IO.TextReader input, System.Net.Sockets.NetworkStream networkStream)
    {
        while (!stopThreads)
        {
            if (!networkStream.DataAvailable)
                continue;

            buffer = input.ReadLine();

            if (showBuffer) { Debug.Log($"[TwitchLight] Buffer->{buffer.Trim()}"); }

            //was message?
            if (buffer.Contains("PRIVMSG #"))
            {
                lock (recievedMsgs)
                {
                    TwitchNames.Instance.AddTwitchName(buffer.Split('!')[0].Replace(':', ' ').Trim());
                    recievedMsgs.Add(buffer);
                }
            }

            //Send pong reply to any ping messages
            if (buffer.StartsWith("PING "))
            {
                SendCommand(buffer.Replace("PING", "PONG"));
            }

            //After server sends 001 command, we can join a channel
            if (buffer.Split(' ')[1] == "001")
            {
                SendCommand("JOIN #" + channelName);
                SendCommand("CAP REQ :twitch.tv/membership");
            }

            if (buffer.Split(' ')[1] == "353")
            {
                for(int i=5;i< buffer.Split(' ').Length; i++)
                {
                    TwitchNames.Instance.AddTwitchName(buffer.Split(' ')[i].Replace(':', ' ').Trim());
                }
            }
            if (buffer.Split(' ')[1] == "421")
            {
                Debug.Log($"[TwitchLight] Unknown Command");
            }
        }
    }
    private void IRCOutputProcedure(System.IO.TextWriter output)
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        while (!stopThreads)
        {
            lock (commandQueue)
            {
                if (commandQueue.Count > 0) //do we have any commands to send?
                {
                    // https://github.com/justintv/Twitch-API/blob/master/IRC.md#command--message-limit 
                    //have enough time passed since we last sent a message/command?
                    if (stopWatch.ElapsedMilliseconds > 1750)
                    {
                        //send msg.
                        output.WriteLine(commandQueue.Peek());
                        output.Flush();
                        //remove msg from queue.
                        commandQueue.Dequeue();
                        //restart stopwatch.
                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                }
            }
        }
    }

    public void SendCommand(string cmd)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue(cmd);
        }
    }
    public void SendMsg(string msg)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue("PRIVMSG #" + channelName + " :" + msg);
        }
    }

    //MonoBehaviour Events.
    void Start()
    {
    }

    void test(string msg)
    {
        Debug.Log($"[TwitchLight] {msg}");
    }



    void OnEnable()
    {
        stopThreads = false;
        StartIRC();
        this.messageRecievedEvent.AddListener(test);
    }
    void OnDisable()
    {
        stopThreads = true;
        //while (inProc.IsAlive || outProc.IsAlive) ;
        //print("inProc:" + inProc.IsAlive.ToString());
        //print("outProc:" + outProc.IsAlive.ToString());
    }
    void OnDestroy()
    {
        stopThreads = true;
        //while (inProc.IsAlive || outProc.IsAlive) ;
        //print("inProc:" + inProc.IsAlive.ToString());
        //print("outProc:" + outProc.IsAlive.ToString());
    }
    void Update()
    {
        lock (recievedMsgs)
        {
            if (recievedMsgs.Count > 0)
            {
                for (int i = 0; i < recievedMsgs.Count; i++)
                {
                    messageRecievedEvent.Invoke(recievedMsgs[i]);
                }
                recievedMsgs.Clear();
            }
        }
    }
}

