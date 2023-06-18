using System.ComponentModel;

namespace Messageria
{
    public enum ExchangeTypeEnum
    {
        [Description("direct")]
        Direct,

        [Description("fanout")]
        Fanout,

        [Description("headers")]
        Headers,

        [Description("topic")]
        Topic,

        [Description("all")]
        All
    }
}
