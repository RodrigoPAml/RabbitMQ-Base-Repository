namespace Messageria
{
    /// <summary>
    /// Callback when consumer recieves messages
    /// </summary>
    public delegate void OnRecieveMessage(int id, string queue, string message, ulong tag);
}
