using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

namespace MPChat
{
    /// <summary>
    /// GameObject has to have a networidentity that is player owned. The owner cna then call the COmmand method
    /// Pretty much drop this on the player object
    /// </summary>
    [RequireComponent(typeof(NetworkIdentity))]
    public class MPChat : NetworkBehaviour
    {
        public bool debug = false;
        public Text chatOutPut;
        public InputField chatInput;

        private void Start()
        {
            chatOutPut = GameObject.Find("ChatOutput").GetComponent<Text>();
            chatInput = GameObject.Find("Chat").transform.Find("ChatInput").GetComponent<InputField>();
            chatInput.onEndEdit.AddListener(SubmitChatMessage);
        }
        public void SubmitChatMessage(string message)
        {
            if (hasAuthority)
            {
                if (debug) { Debug.Log($"MPChat::SubmitChatMessage({message})"); }
                CmdChatToServer(message);
                chatInput.text = string.Empty;
            }
        }
        [Command]
        public void CmdChatToServer(string message)
        {
            if (debug) { Debug.Log($"MPChat::CmdSendChatToServer({message})"); }
            RpcRecieveChatMessage(message);
        }
        [ClientRpc]
        private void RpcRecieveChatMessage(string message)
        {
            if (debug) { Debug.Log($"MPChat::RpcRecieveChatMessage({message})"); }
            chatOutPut.text = $"{chatOutPut.text}{Environment.NewLine}{message}";
        }
    }
}