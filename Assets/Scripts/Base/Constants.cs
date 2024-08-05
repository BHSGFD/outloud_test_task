public class Constants
{
    public class Animations
    {
        public const float OPEN_TIME = 0.5f;
        public const float CLOSE_TIME = 0.8f;
    }

    public class StateNames
    {
        public class GameStates
        {
            public const string BOOTSTRAP_STATE = "BootstrapState";
            public const string MENU_STATE = "MenuState";
            public const string CARD_MINI_GAME_STATE = "GameState";
        }

        public class MiniGameStates
        {
            public const string START_MINI_GAME = "StartMiniGame";
        }
    }

    public class SceneName
    {
        public const string MENU = "Menu";
        public const string MINI_GAME = "CardGame";
    }

    public class GameData
    {
        public const int MIN_SIZE = 0;
        public const int MAX_SIZE = 10;
    }
}