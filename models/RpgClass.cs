using System.Text.Json.Serialization;

namespace rpg_game.models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric,
        Rogue
    }
}
