using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OTTER
{
    /// <summary>
    /// -
    /// </summary>
    public partial class BGL : Form
    {
        /* ------------------- */
        #region Environment Variables

        List<Func<int>> GreenFlagScripts = new List<Func<int>>();

        /// <summary>
        /// Uvjet izvršavanja igre. Ako je <c>START == true</c> igra će se izvršavati.
        /// </summary>
        /// <example><c>START</c> se često koristi za beskonačnu petlju. Primjer metode/skripte:
        /// <code>
        /// private int MojaMetoda()
        /// {
        ///     while(START)
        ///     {
        ///       //ovdje ide kod
        ///     }
        ///     return 0;
        /// }</code>
        /// </example>
        public static bool START = true;

        //sprites
        /// <summary>
        /// Broj likova.
        /// </summary>
        public static int spriteCount = 0, soundCount = 0;

        /// <summary>
        /// Lista svih likova.
        /// </summary>
        //public static List<Sprite> allSprites = new List<Sprite>();
        public static SpriteList<Sprite> allSprites = new SpriteList<Sprite>();

        //sensing
        int mouseX, mouseY;
        Sensing sensing = new Sensing();

        //background
        List<string> backgroundImages = new List<string>();
        int backgroundImageIndex = 0;
        string ISPIS = "";

        SoundPlayer[] sounds = new SoundPlayer[1000];
        TextReader[] readFiles = new StreamReader[1000];
        TextWriter[] writeFiles = new StreamWriter[1000];
        bool showSync = false;
        int loopcount;
        DateTime dt = new DateTime();
        String time;
        double lastTime, thisTime, diff;

        #endregion
        /* ------------------- */
        #region Events

        private void Draw(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            try
            {                
                foreach (Sprite sprite in allSprites)
                {                    
                    if (sprite != null)
                        if (sprite.Show == true)
                        {
                            g.DrawImage(sprite.CurrentCostume, new Rectangle(sprite.X, sprite.Y, sprite.Width, sprite.Height));
                        }
                    if (allSprites.Change)
                        break;
                }
                if (allSprites.Change)
                    allSprites.Change = false;
            }
            catch
            {
                //ako se doda sprite dok crta onda se mijenja allSprites
                MessageBox.Show("Greška!");
            }
        }

        private void startTimer(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            Init();
        }

        private void updateFrameRate(object sender, EventArgs e)
        {
            updateSyncRate();
        }

        /// <summary>
        /// Crta tekst po pozornici.
        /// </summary>
        /// <param name="sender">-</param>
        /// <param name="e">-</param>
        public void DrawTextOnScreen(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var brush = new SolidBrush(Color.WhiteSmoke);
            string text = ISPIS;

            SizeF stringSize = new SizeF();
            Font stringFont = new Font("Arial", 14);
            stringSize = e.Graphics.MeasureString(text, stringFont);

            using (Font font1 = stringFont)
            {
                RectangleF rectF1 = new RectangleF(0, 0, stringSize.Width, stringSize.Height);
                e.Graphics.FillRectangle(brush, Rectangle.Round(rectF1));
                e.Graphics.DrawString(text, font1, Brushes.Black, rectF1);
            }
        }

        private void mouseClicked(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = true;
            sensing.MouseDown = true;            
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            //sensing.MouseDown = false;
            sensing.MouseDown = false;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;

            //sensing.MouseX = e.X;
            //sensing.MouseY = e.Y;
            //Sensing.Mouse.x = e.X;
            //Sensing.Mouse.y = e.Y;
            sensing.Mouse.X = e.X;
            sensing.Mouse.Y = e.Y;

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            sensing.Key = e.KeyCode.ToString();
            sensing.KeyPressedTest = true;
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            sensing.Key = "";
            sensing.KeyPressedTest = false;
        }

        private void Update(object sender, EventArgs e)
        {
            if (sensing.KeyPressed(Keys.Escape))
            {
                START = false;
            }

            if (START)
            {
                this.Refresh();
            }
        }

        #endregion
        /* ------------------- */
        #region Start of Game Methods

        //my
        #region my

        //private void StartScriptAndWait(Func<int> scriptName)
        //{
        //    Task t = Task.Factory.StartNew(scriptName);
        //    t.Wait();
        //}

        //private void StartScript(Func<int> scriptName)
        //{
        //    Task t;
        //    t = Task.Factory.StartNew(scriptName);
        //}

        private int AnimateBackground(int intervalMS)
        {
            while (START)
            {
                setBackgroundPicture(backgroundImages[backgroundImageIndex]);
                Game.WaitMS(intervalMS);
                backgroundImageIndex++;
                if (backgroundImageIndex == 3)
                    backgroundImageIndex = 0;
            }
            return 0;
        }

        private void KlikNaZastavicu()
        {
            foreach (Func<int> f in GreenFlagScripts)
            {
                Task.Factory.StartNew(f);
            }
        }

        #endregion

        /// <summary>
        /// BGL
        /// </summary>
        public BGL(string _type, string _player)
        {
            InitializeComponent();
            this.type = _type;
            this.player = _player;
        }

        /// <summary>
        /// Pričekaj (pauza) u sekundama.
        /// </summary>
        /// <example>Pričekaj pola sekunde: <code>Wait(0.5);</code></example>
        /// <param name="sekunde">Realan broj.</param>
        public void Wait(double sekunde)
        {
            int ms = (int)(sekunde * 1000);
            Thread.Sleep(ms);
        }

        //private int SlucajanBroj(int min, int max)
        //{
        //    Random r = new Random();
        //    int br = r.Next(min, max + 1);
        //    return br;
        //}

        /// <summary>
        /// -
        /// </summary>
        public void Init()
        {
            if (dt == null) time = dt.TimeOfDay.ToString();
            loopcount++;
            //Load resources and level here
            this.Paint += new PaintEventHandler(DrawTextOnScreen);
            SetupGame();
        }

        /// <summary>
        /// -
        /// </summary>
        /// <param name="val">-</param>
        public void showSyncRate(bool val)
        {
            showSync = val;
            if (val == true) syncRate.Show();
            if (val == false) syncRate.Hide();
        }

        /// <summary>
        /// -
        /// </summary>
        public void updateSyncRate()
        {
            if (showSync == true)
            {
                thisTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                diff = thisTime - lastTime;
                lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

                double fr = (1000 / diff) / 1000;

                int fr2 = Convert.ToInt32(fr);

                syncRate.Text = fr2.ToString();
            }

        }

        //stage
        #region Stage

        /// <summary>
        /// Postavi naslov pozornice.
        /// </summary>
        /// <param name="title">tekst koji će se ispisati na vrhu (naslovnoj traci).</param>
        public void SetStageTitle(string title)
        {
            this.Text = title;
        }

        /// <summary>
        /// Postavi boju pozadine.
        /// </summary>
        /// <param name="r">r</param>
        /// <param name="g">g</param>
        /// <param name="b">b</param>
        public void setBackgroundColor(int r, int g, int b)
        {
            this.BackColor = Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Postavi boju pozornice. <c>Color</c> je ugrađeni tip.
        /// </summary>
        /// <param name="color"></param>
        public void setBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        /// <summary>
        /// Postavi sliku pozornice.
        /// </summary>
        /// <param name="backgroundImage">Naziv (putanja) slike.</param>
        public void setBackgroundPicture(string backgroundImage)
        {
            this.BackgroundImage = new Bitmap(backgroundImage);
        }

        /// <summary>
        /// Izgled slike.
        /// </summary>
        /// <param name="layout">none, tile, stretch, center, zoom</param>
        public void setPictureLayout(string layout)
        {
            if (layout.ToLower() == "none") this.BackgroundImageLayout = ImageLayout.None;
            if (layout.ToLower() == "tile") this.BackgroundImageLayout = ImageLayout.Tile;
            if (layout.ToLower() == "stretch") this.BackgroundImageLayout = ImageLayout.Stretch;
            if (layout.ToLower() == "center") this.BackgroundImageLayout = ImageLayout.Center;
            if (layout.ToLower() == "zoom") this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        #endregion

        //sound
        #region sound methods

        /// <summary>
        /// Učitaj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        /// <param name="file">-</param>
        public void loadSound(int soundNum, string file)
        {
            soundCount++;
            sounds[soundNum] = new SoundPlayer(file);
        }

        /// <summary>
        /// Sviraj zvuk.
        /// </summary>
        /// <param name="soundNum">-</param>
        public void playSound(int soundNum)
        {
            sounds[soundNum].Play();
        }

        /// <summary>
        /// loopSound
        /// </summary>
        /// <param name="soundNum">-</param>
        public void loopSound(int soundNum)
        {
            sounds[soundNum].PlayLooping();
        }

        /// <summary>
        /// Zaustavi zvuk.
        /// </summary>
        /// <param name="soundNum">broj</param>
        public void stopSound(int soundNum)
        {
            sounds[soundNum].Stop();
        }

        #endregion

        //file
        #region file methods

        /// <summary>
        /// Otvori datoteku za čitanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToRead(string fileName, int fileNum)
        {
            readFiles[fileNum] = new StreamReader(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToRead(int fileNum)
        {
            readFiles[fileNum].Close();
        }

        /// <summary>
        /// Otvori datoteku za pisanje.
        /// </summary>
        /// <param name="fileName">naziv datoteke</param>
        /// <param name="fileNum">broj</param>
        public void openFileToWrite(string fileName, int fileNum)
        {
            writeFiles[fileNum] = new StreamWriter(fileName);
        }

        /// <summary>
        /// Zatvori datoteku.
        /// </summary>
        /// <param name="fileNum">broj</param>
        public void closeFileToWrite(int fileNum)
        {
            writeFiles[fileNum].Close();
        }

        /// <summary>
        /// Zapiši liniju u datoteku.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <param name="line">linija</param>
        public void writeLine(int fileNum, string line)
        {
            writeFiles[fileNum].WriteLine(line);
        }

        /// <summary>
        /// Pročitaj liniju iz datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća pročitanu liniju</returns>
        public string readLine(int fileNum)
        {
            return readFiles[fileNum].ReadLine();
        }

        /// <summary>
        /// Čita sadržaj datoteke.
        /// </summary>
        /// <param name="fileNum">broj datoteke</param>
        /// <returns>vraća sadržaj</returns>
        public string readFile(int fileNum)
        {
            return readFiles[fileNum].ReadToEnd();
        }

        #endregion

        //mouse & keys
        #region mouse methods

        /// <summary>
        /// Sakrij strelicu miša.
        /// </summary>
        public void hideMouse()
        {
            Cursor.Hide();
        }

        /// <summary>
        /// Pokaži strelicu miša.
        /// </summary>
        public void showMouse()
        {
            Cursor.Show();
        }

        /// <summary>
        /// Provjerava je li miš pritisnut.
        /// </summary>
        /// <returns>true/false</returns>
        public bool isMousePressed()
        {
            //return sensing.MouseDown;
            return sensing.MouseDown;
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">naziv tipke</param>
        /// <returns></returns>
        public bool isKeyPressed(string key)
        {
            if (sensing.Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Provjerava je li tipka pritisnuta.
        /// </summary>
        /// <param name="key">tipka</param>
        /// <returns>true/false</returns>
        public bool isKeyPressed(Keys key)
        {
            if (sensing.Key == key.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #endregion
        /* ------------------- */

        /* ------------ GAME CODE START ------------ */

        /* Game variables */

        string type, player;
        

        public delegate void TouchHandler();
        public static event TouchHandler _touch;


        /* Initialization */
        MainCharacter MC;
        Caster caster;
        Archer archer;
        Assassin assassin;
        
        Dagger dagger;
        MagicBall magicball;
        Arrow arrow;
        Weapon wep;

        Treasure treasure;

        Skeleton skeleton;
        Demon demon;

        Random r = new Random();


        private void SetupGame()
        {
            //1. setup stage
            SetStageTitle("TH9");
            //setBackgroundColor(Color.WhiteSmoke);            
            setBackgroundPicture("backgrounds\\dungeon.png");
            //none, tile, stretch, center, zoom
            setPictureLayout("stretch");

            //2. add sprites
            if (type=="caster")
            {
                caster = new Caster("sprites\\CasterD.png", GameOptions.DownEdge/2, GameOptions.RightEdge/2);
                MC = caster;
                magicball = new MagicBall("sprites\\ball.png", GameOptions.DownEdge / 2, GameOptions.RightEdge / 2);
                magicball.SetSize(40);
                magicball.SetVisible(false);
                wep = magicball;
            }
            else if (type == "archer")
            {
                archer = new Archer("sprites\\ArcherD.png", GameOptions.DownEdge / 2, GameOptions.RightEdge / 2);
                MC = archer;
                arrow = new Arrow("sprites\\Arrow.png", GameOptions.DownEdge / 2, GameOptions.RightEdge / 2);
                arrow.SetSize(20);
                arrow.SetVisible(false);
                wep = arrow;  
            }
            else if (type == "assassin")
            {
                assassin = new Assassin("sprites\\AssassinD.png", GameOptions.DownEdge / 2, GameOptions.RightEdge / 2);
                MC = assassin;
                dagger = new Dagger("sprites\\dagger.png", GameOptions.DownEdge / 2, GameOptions.RightEdge / 2);
                dagger.SetSize(20);
                dagger.SetVisible(false);
                wep = dagger;
            }
            //MC.RotationStyle = "AllAround";
            MC.SetSize(60);
            Game.AddSprite(MC);
            Game.AddSprite(wep);

            treasure = new Treasure("sprites\\Treasure.png", 640, 100);
            treasure.SetSize(70);
            treasure.SetVisible(true);
            Game.AddSprite(treasure);

            skeleton = new Skeleton("sprites\\SkeletonS.png", 400, 0);
            skeleton.SetSize(100);
            Game.AddSprite(skeleton);


            _touch += Touch;

            //3. scripts that start
            Game.StartScript(MCMove);
            Game.StartScript(MCAttack);
            Game.StartScript(EnemyMove);
            Game.StartScript(Ispis);
            Wait(1);

        }

        /* Scripts */

        private int Metoda()
        {
            while (START) //ili neki drugi uvjet
            {

                Wait(0.1);
            }
            return 0;
        }
 
        private void Touch()
        {
            if (MC.Treasure < 9)
            {
                treasure.SetVisible(false);
                treasure.Y = r.Next(0, GameOptions.DownEdge - treasure.Height);
                treasure.X = r.Next(0, GameOptions.RightEdge - treasure.Width);
                treasure.SetVisible(true);
            }
        }

        public int MCMove()
        {

            while (START)
            {
                if (MC.Health <= 0)
                {
                    Wait(0.1);
                    DeleteSprites();
                    START = false;
                    using (StreamWriter sw = File.AppendText(GameOptions.dat))
                    {
                        sw.WriteLine(player + " " + MC.Kills * MC.Health * MC.Treasure + " " + type);
                    }
                    GameOver GO = new GameOver("Game Over! You lost. Your score was:" + MC.Kills * MC.Health * MC.Treasure);
                    GO.ShowDialog();
                }
                if (MC.Treasure == 9)
                {
                    if (demon.Health <= 0)
                    {
                        demon.SetVisible(false);
                        skeleton.SetVisible(false);
                        Wait(0.1);
                        DeleteSprites();
                        START = false;
                        using (StreamWriter sw = File.AppendText(GameOptions.dat))
                        {
                            sw.WriteLine(player + " " + MC.Kills * MC.Health * MC.Treasure + " " + type);
                        }
                        GameOver GO = new GameOver("Congratulations!!! You beat the game.Your score was:" + MC.Kills * MC.Health * MC.Treasure);
                        GO.ShowDialog();
                    }
                }
                if (sensing.KeyPressed(Keys.Up)||sensing.KeyPressed(Keys.W))
                {
                    MC.SetDirection(0);
                    MC.MoveSteps(MC.Speed);
                }
                if (sensing.KeyPressed(Keys.Down) || sensing.KeyPressed(Keys.S))
                {
                    MC.SetDirection(180);
                    MC.MoveSteps(MC.Speed);
                }
                if (sensing.KeyPressed(Keys.Left) || sensing.KeyPressed(Keys.A))
                {
                    MC.SetDirection(270);
                    MC.MoveSteps(MC.Speed);
                }
                if (sensing.KeyPressed(Keys.Right) || sensing.KeyPressed(Keys.D))
                {
                    MC.SetDirection(90);
                    MC.MoveSteps(MC.Speed);
                }
                
                if (sensing.MouseDown|| sensing.KeyPressed(Keys.Space))
                {
                    if (MC.WeaponR)
                    {
                        
                        Game.StartScript(MCAttack);
                        MC.WeaponR = false;
                        wep.SetDirection(MC.GetDirection());

                    }
                }
                Wait(0.01);
                if (MC.TouchingSprite(treasure))
                {
                    treasure.X = -50;
                    MC.Treasure += 1;

                    if (MC.Treasure == 9)
                    {
                        demon = new Demon("sprites\\Demon.png", 800, 0);
                        demon.SetSize(120);
                        Game.AddSprite(demon);

                        Game.StartScript(DemonMove);
                    }
                    _touch.Invoke();

                }
            }
            return 0;
        }

        public int MCAttack()
        {
            wep.Goto_Sprite(MC);
            wep.SetVisible(true);
            while (START)
            {
                wep.MoveSteps(wep.Speed);
                Wait(0.01);
                if (wep.TouchingEdge())
                {
                    wep.SetVisible(false);
                    MC.WeaponR= true;
                    break;
                }
                if (wep.TouchingSprite(skeleton))
                {
                    skeleton.Health -= wep.Damage;
                    wep.SetVisible(false);
                    MC.WeaponR = true;
                    Wait(0.5);
                    if (skeleton.Health<=0)
                    {
                        skeleton.SetVisible(false);
                        MC.Kills += 1;
                        skeleton.MonsterSpawn(MC);
                        skeleton.MonsterMove(MC);
                    }
                    break;
                }
                if (MC.Treasure==9)
                {
                    if (wep.TouchingSprite(demon))
                    {
                        demon.Health -= wep.Damage;
                        wep.SetVisible(false);
                        MC.WeaponR = true;
                        Wait(0.5);
                        break;

                    }
                }
            }
            return 0;
        }

        public int EnemyMove()
        {
            while (START)
            {
                skeleton.MonsterMove(MC);
                Wait(0.1);
                if (skeleton.TouchingSprite(MC))
                {
                    MC.Health -= skeleton.Health;
                    skeleton.SetVisible(false);
                    skeleton.MonsterSpawn(MC);
                }
            }
            return 0;
        }

        public int DemonMove()
        {
            while (START)
            {
                demon.MonsterMove(MC);
                Wait(0.2);
                if (demon.TouchingSprite(MC))
                {
                    MC.Health -= demon.Health;
                    demon.SetVisible(false);
                }
            }
            return 0;
        }

        private int Ispis()
        {
            while (START)
            {
                ISPIS = String.Format("HP: {0} Treasure: {1} Kills: {2})", MC.Health, MC.Treasure, MC.Kills);
            }
            ISPIS = String.Format("HP: {0} Treasure: {1} Kills: {2})", MC.Health, MC.Treasure, MC.Kills);
            return 0;
        }

        private void DeleteSprites()
        {
            allSprites.Remove(MC);
            allSprites.Remove(wep);
            allSprites.Remove(skeleton);
            allSprites.Remove(demon);         
        }

        /* ------------ GAME CODE END ------------ */


    }
}
