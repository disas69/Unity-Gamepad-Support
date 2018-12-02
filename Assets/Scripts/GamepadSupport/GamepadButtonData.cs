namespace GamepadSupport
{
    public class GamepadButtonData
    {
        public GamepadType Type;
        public string Name;

        public GamepadButtonData(GamepadType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}