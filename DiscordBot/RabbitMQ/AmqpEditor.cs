using System;

public class AmqpEditor
{
    public string _message;
    public AmqpEditor(string message)
    {
        _message = message;
    }
    public string[] GetArrayMess()
    {
        string[] arrMessage = _message.Split("<split>");
        arrMessage[0] = arrMessage[0].ToLower();
        arrMessage[0] = arrMessage[0].Replace(":","");
        arrMessage[0] = arrMessage[0].Replace(" ","-");
        foreach(var elem in arrMessage)
        {
            Console.WriteLine(elem);
        }
        return arrMessage;
    }
}
