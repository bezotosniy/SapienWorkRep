using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public void OnChatClickOpener()
    {
        GameObject.Find("Messages").GetComponent<MessagesManager>().OnClickChatOpener(this.gameObject);
    }
}
