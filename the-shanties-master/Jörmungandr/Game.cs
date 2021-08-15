using System.Net.Mime;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Jörmungandr.Level;

namespace Jörmungandr
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        // Static Properties to use
        public static Game gameInstance { get; private set; }

        //public static Viewport Viewport { get { return gameInstance.GraphicsDevice.Viewport; } }
        //public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Background _background;
        public PlayerUI playerUI;
        private MainMenu _mainMenuUI;
        private SettingsMenu _settingsMenuUI;
        private PauseMenu _pauseMenuUI;

        private float totalGameTime = 0f;

        //different states indicated by the menu UI
        private bool playState = false;
        private bool pauseState = false;
        private bool settingsState = false;
        private bool exitState = false;

        private int settingsOpened = 0;

        private int backgroundNum;

        // Characters
        private Character playerCharacter;

        public LevelManager levelManager;

        public Game()
        {
            gameInstance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this._background = new Background();
            this.playerUI = new PlayerUI();
            this._mainMenuUI = new MainMenu();
            this._settingsMenuUI = new SettingsMenu();
            this._pauseMenuUI = new PauseMenu();
            this.levelManager = new LevelManager();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            // set window size
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            
            this.playerCharacter = Player.Instance;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Art.Load(Content);
            _background.LoadBackground();
            _background.SwitchBackground(Art.CaveBackground);

            playerUI.LoadContent(Content.Load<SpriteFont>("Score"), Content.Load<SpriteFont>("endGame"), Content.Load<SpriteFont>("EndGameFont"));
            _mainMenuUI.LoadContent(Content.Load<SpriteFont>("menuFont"));
            _settingsMenuUI.LoadContent(Content.Load<SpriteFont>("menuFont"));
            _pauseMenuUI.LoadContent(Content.Load<SpriteFont>("menuFont"));
        }

        protected override void Update(GameTime gameTime)
        {
            //// TODO: Add your update logic here
            base.Update(gameTime);

            if (playState == true && pauseState == false)
            {
                // get current keyboard state
                KeyboardState state = Keyboard.GetState();

                // If they hit esc, and GameOver == false then pause game
                // else if ... GameOver == true then exit game
                if (state.IsKeyDown(Keys.Escape) && Player.Instance.IsExpired == false)
                {
                    this._pauseMenuUI.updatePauseState();
                    pauseState = true;
                }
                else if (state.IsKeyDown(Keys.Escape) && Player.Instance.IsExpired == true)
                {
                    Exit();
                }

                _background.Update(gameTime);

                this.playerCharacter.Update(gameTime);

                //Keep track of the total time the game has ran
                totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // update the spawnchance in the levelmanager (this spaces out the spawning more nicely)
                //levelManager.spawnChance += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // update the level
                // call the update function on each active enemy
                levelManager.LoadLevel(gameTime);
                EntityManager.Update(gameTime);

                if ((totalGameTime > 5) && (backgroundNum == 0))         // Start transition from Cave to Ocean
                {
                    backgroundNum = 1;          // Background tracker
                    _background.SwitchTo(1);    // Switch Background
                }
                else if ((totalGameTime > 19) && (backgroundNum == 1))  // Switch Background handled in Background.cs
                {
                    backgroundNum = 2;          // Background tracker
                }
                else if ((totalGameTime > 25) && (backgroundNum == 2))  // Start transition from Ocean to Sky
                {
                    backgroundNum = 3;          // Background tracker
                    _background.SwitchTo(2);    // Switch Background
                }
                else if ((totalGameTime > 40) && (backgroundNum == 3))  // Switch Background handled in Background.cs
                {
                    backgroundNum = 4;          // Background tracker
                }

            }
            else if (playState == true && pauseState == true)
            {
                this._pauseMenuUI.Update();

                pauseState = this._pauseMenuUI.checkPausedGame();
                exitState = this._pauseMenuUI.closeGame();
                if (exitState == true)
                    playState = false;

            }
            else if (playState == false && settingsState == true)
            {
                this._settingsMenuUI.Update();
                settingsOpened = 1;
                settingsState = this._settingsMenuUI.closeSettings();
            }
            else if (playState == false && exitState == true)
            {
                Exit();
            }
            else
            {
                this._mainMenuUI.Update();
                playState = this._mainMenuUI.playGame();
                settingsState = this._mainMenuUI.openSettings();
                exitState = this._mainMenuUI.closeGame();

            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // draw background
            _spriteBatch.Begin();
            _background.DrawBackground(_spriteBatch);
            _spriteBatch.End();

            if (playState == true)
            {

                // draw player
                _spriteBatch.Begin();
                this.playerCharacter.Draw(_spriteBatch);
                _spriteBatch.End();

                // draw enemies
                _spriteBatch.Begin();
                EntityManager.Draw(_spriteBatch);
                _spriteBatch.End();

                // draw player UI
                _spriteBatch.Begin();
                playerUI.Draw(_spriteBatch, totalGameTime);
                _spriteBatch.End();

                if (pauseState == true)
                {
                    _spriteBatch.Begin();
                    _pauseMenuUI.Draw(_spriteBatch);
                    _spriteBatch.End();
                }
            }
            else if (playState == false && settingsState == true)
            {
                _spriteBatch.Begin();
                _settingsMenuUI.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else
            {
                _spriteBatch.Begin();
                _mainMenuUI.Draw(_spriteBatch);
                _spriteBatch.End();

            }

            base.Draw(gameTime);
        }

    }
}