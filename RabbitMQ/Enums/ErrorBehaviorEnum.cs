namespace Messageria
{
    /// <summary>
    /// The error behavior when a consumer throw an exception when processing a messsage
    /// </summary>
    public enum ErrorBehaviourEnum
    {
        NackWithRequeue,
        Nack,
        Ack
    }
}
