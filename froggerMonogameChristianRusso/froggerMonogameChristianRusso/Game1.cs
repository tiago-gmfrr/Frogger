/*******************************************************
Nom ......... : FroggerMonogame
Role ........ : Frogger réalisé avec MonoGame
Inspiré du premier Frogger, avec des éléments de Frogger 2
Auteur ...... : Christian Russo
Version ..... : V1.0 du 25/10/2019
Classe ...... : I.FA-P3A
********************************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System;
using froggerMonogameChristianRusso;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;

namespace froggerMonogameChristianRusso
{
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Déclaration du backGround et du player

        Player player;

        //Déclaration des objets et les listes les contenant
        PickUp pickUp;
        List<PickUp> ListPickUp;

        Voiture voiture;
        List<Voiture> ListVoiture;

        Bus bus;
        List<Bus> ListBus;

        GrosseVoiture grosseVoiture;
        List<GrosseVoiture> ListGrosseVoiture;

        Buche bucheGauche;
        Buche bucheDroite;
        List<Buche> ListBuche;

        Crocodile croco;
        List<Crocodile> ListCroco;

        Tortue tortue;
        List<Tortue> ListTortue;


        List<BabyFrog> ListBabyFrog;

        Serpent serpent;
        List<Serpent> ListSerpent;
        //Initialisation des vies
        Vie vie;
        List<Vie> ListVie;
        //Liste des rochers de bordure de terrain
        List<Rectangle> ListRocher;
        //Initialisation du compte à rebours et du timer du score
        Decompte decompte;
        Compteur compteur;
        //Initialisation des soundEffects et soundEffectInstances
        private SoundEffect gameMusic;
        private SoundEffect victoryMusic;
        private SoundEffect gameOverMusic;
        private SoundEffect menuTheme;
        private SoundEffect mortEcraser;
        private SoundEffect mortNoyer;
        private SoundEffect babyFrogRecup;
        private SoundEffectInstance gameMusicInstance;
        private SoundEffectInstance victoryMusicInstance;
        private SoundEffectInstance gameOverMusicInstance;
        private SoundEffectInstance menuThemeInstance;
        private SoundEffectInstance mortEcraserInstance;
        private SoundEffectInstance mortNoyerInstance;
        private SoundEffectInstance babyFrogRecupInstance;
        //Initialisation des menus
        Texture2D gameOverScreen;
        Texture2D victoryScreen;
        Texture2D mainBackGround;
        Texture2D mainMenu;
        Texture2D singlePlayer;
        Texture2D multiPlayer;
        Texture2D highScore;
        Texture2D blackScreen;
        Texture2D newHighScore;
        string menu = "mainMenu";
        ScoreManager scoreManager;
       
        int indexDic = 0;
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
        int indexAlphabet = 0;
        int indexNom = 0;
        bool scoreAjoute = false;
        
        string stringNom = "AAA";



        //Déclaration des timer de respawn de chaque entité
        float voitureRespawn = 0f;
        float pickUpRespawn = 0f;
        float busRespawn = 0f;
        float grosseVoitureRespawn = 0f;
        float bucheRespawn = 0f;
        float crocoRespawn = 0f;
        float tortueRespawn = 0f;
        float serpentRespawn = 0f;
        float tempsJoue = 0f;
        float chooseTimer = 0f;

        //Variable qui nous dit si on est sur une plateforme mouvante
        bool safePlace = false;
        //Initialisation des variables de taille de l'affichage et rocher
        int safeVieWidth = 10;
        float safeRocherWidth = 0.850f;

        bool firstSpawn = true;

        public Game1()
        {
            //ScoreManager
            scoreManager = ScoreManager.Load();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            //Paramètres pour le plein écran
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {



            //Affectation des compteurs et du joueur
            player = new Player(this);
            decompte = new Decompte(this);
            compteur = new Compteur(this);

            //Initialisation des différentes listes
            ListVoiture = new List<Voiture>();
            ListPickUp = new List<PickUp>();
            ListBus = new List<Bus>();
            ListGrosseVoiture = new List<GrosseVoiture>();
            ListBuche = new List<Buche>();
            ListCroco = new List<Crocodile>();
            ListTortue = new List<Tortue>();
            ListBabyFrog = new List<BabyFrog>();
            ListSerpent = new List<Serpent>();
            ListVie = new List<Vie>();
            ListRocher = new List<Rectangle>();



            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Affectation de la texture au background
            mainBackGround = Content.Load<Texture2D>("sprite/plateau2");
            //Affectation de la texture et la position au player
            Texture2D playerTexture = Content.Load<Texture2D>("sprite/frog1");
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - playerTexture.Width, 990);
            //Texture du gameOverScreen
            gameOverScreen = Content.Load<Texture2D>("menu/gameOver");
            //Texture du victoryScreen
            victoryScreen = Content.Load<Texture2D>("menu/intro");
            //Texture du menu principal
            mainMenu = Content.Load<Texture2D>("menu/mainMenu");
            //Texture de la selection des joueurs
            singlePlayer = Content.Load<Texture2D>("menu/onePlayer");
            multiPlayer = Content.Load<Texture2D>("menu/twoPlayer");
            //Texture pour les HighScore
            highScore = Content.Load<Texture2D>("menu/background");
            blackScreen = Content.Load<Texture2D>("menu/highScoreScreen");
            newHighScore = Content.Load<Texture2D>("menu/newHighScore");
            //Charge la musique et la joue en boucle
            gameMusic = Content.Load<SoundEffect>("musicSound/mainTheme");
            victoryMusic = Content.Load<SoundEffect>("musicSound/stageWin");
            gameOverMusic = Content.Load<SoundEffect>("musicSound/gameOver");
            menuTheme = Content.Load<SoundEffect>("musicSound/menuTheme");
            mortEcraser = Content.Load<SoundEffect>("musicSound/frogEcraser");
            mortNoyer = Content.Load<SoundEffect>("musicSound/frogNoyer");
            babyFrogRecup = Content.Load<SoundEffect>("musicSound/babyfrogRecup");
            //Création des musicInstance
            gameMusicInstance = gameMusic.CreateInstance();
            victoryMusicInstance = victoryMusic.CreateInstance();
            gameOverMusicInstance = gameOverMusic.CreateInstance();
            menuThemeInstance = menuTheme.CreateInstance();
            mortEcraserInstance = mortEcraser.CreateInstance();
            mortNoyerInstance = mortNoyer.CreateInstance();
            babyFrogRecupInstance = babyFrogRecup.CreateInstance();
            //Joue l'instance du menu principal
            menuThemeInstance.Play();
            //Load le content pour les decomptes
            decompte.LoadContent("decompte", new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 100, (GraphicsDevice.DisplayMode.Height / 2) - 150));
            compteur.LoadContent("compteur", new Vector2(10, 955));
            
            //Fait spawn la première entité de chaque objet
            spawnVoiture();
            spawnPickUp();
            spawnBus();
            spawnGrosseVoiture();
            spawnBuche();
            spawnCroco();
            spawnTortue();
            spawnBabyFrog();
            spawnSerpent();
            spawnRocher();
            //Initialize le joueur
            player.Initialize(playerTexture, playerPosition);
        }

        //Déclaration, initialisation avec texture et position et ajout dans la liste de chaque objet
        public void spawnGrosseVoiture()
        {
            grosseVoiture = new GrosseVoiture(this);
            grosseVoiture.Initialize(Content.Load<Texture2D>("sprite/car1"), new Vector2(1800, 590));
            ListGrosseVoiture.Add(grosseVoiture);
        }
        public void spawnPickUp()
        {
            pickUp = new PickUp(this);
            pickUp.Initialize(Content.Load<Texture2D>("sprite/car4"), new Vector2(1800, 770));
            ListPickUp.Add(pickUp);
        }
        public void spawnVoiture()
        {
            voiture = new Voiture(this);

            voiture.Initialize(Content.Load<Texture2D>("sprite/car5"), new Vector2(-100, 850));
            ListVoiture.Add(voiture);
        }
        public void spawnBus()
        {
            bus = new Bus(this);

            bus.Initialize(Content.Load<Texture2D>("sprite/car3"), new Vector2(-200, 680));
            ListBus.Add(bus);
        }
        public void spawnBuche()
        {
            bucheGauche = new Buche(this, 5);
            bucheDroite = new Buche(this, 3);

            bucheDroite.Initialize(Content.Load<Texture2D>("sprite/wood1"), new Vector2(-300, 280));
            bucheGauche.Initialize(Content.Load<Texture2D>("sprite/wood2"), new Vector2(-300, 460));

            ListBuche.Add(bucheDroite);
            ListBuche.Add(bucheGauche);
        }
        public void spawnCroco()
        {
            croco = new Crocodile(this);

            croco.Initialize(Content.Load<Texture2D>("sprite/croco1"), new Vector2(1800, 370));
            ListCroco.Add(croco);
        }
        public void spawnTortue()
        {
            tortue = new Tortue(this);

            tortue.Initialize(Content.Load<Texture2D>("sprite/turtle2"), new Vector2(1800, 200));
            ListTortue.Add(tortue);
        }
        public void spawnSerpent()
        {
            serpent = new Serpent(this);

            serpent.Initialize(Content.Load<Texture2D>("sprite/snake1"), new Vector2(1800, 530));
            ListSerpent.Add(serpent);
        }
        public void spawnBabyFrog()
        {
            BabyFrog greenBabyFrog = new BabyFrog(this);
            BabyFrog orangeBabyFrog = new BabyFrog(this);
            BabyFrog blueBabyFrog = new BabyFrog(this);
            BabyFrog violetBabyFrog = new BabyFrog(this);
            BabyFrog yellowBabyFrog = new BabyFrog(this);

            greenBabyFrog.Initialize(Content.Load<Texture2D>("sprite/babyFrog1"), new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width * 0.915f, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height * 0.9f));
            yellowBabyFrog.Initialize(Content.Load<Texture2D>("sprite/babyFrog5"), new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width * 0.715f, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height * 0.9f));
            blueBabyFrog.Initialize(Content.Load<Texture2D>("sprite/babyFrog3"), new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width * 0.515f, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height * 0.9f));
            violetBabyFrog.Initialize(Content.Load<Texture2D>("sprite/babyFrog4"), new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width * 0.315f, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height * 0.9f));
            orangeBabyFrog.Initialize(Content.Load<Texture2D>("sprite/babyFrog2"), new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width * 0.115f, GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height * 0.9f));

            ListBabyFrog.Add(greenBabyFrog);
            ListBabyFrog.Add(yellowBabyFrog);
            ListBabyFrog.Add(violetBabyFrog);
            ListBabyFrog.Add(blueBabyFrog);
            ListBabyFrog.Add(orangeBabyFrog);

        }
        public void spawnVie()
        {
            vie = new Vie(this);
            Vector2 viePosition = new Vector2(safeVieWidth, 10);
            Texture2D vieTexture = Content.Load<Texture2D>("sprite/frog3");
            vie.Initialize(vieTexture, viePosition);
            ListVie.Add(vie);

            safeVieWidth += 70;
        }
        public void spawnRocher()
        {
            Rectangle rocher = new Rectangle((int)(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width * safeRocherWidth), 30, 150, 125);
            ListRocher.Add(rocher);
            safeRocherWidth -= 0.200f;


        }
        //Méthode permettant de tout remettre à zéro pour recommencer une partie
        public void restartGame()
        {
            firstSpawn = true;
            indexAlphabet = 0;
            indexNom = 0;
            stringNom = "AAA";
            scoreAjoute = false;
            compteur.actif = true;
            compteur.Timer = 0;
            compteur.Minute = 0;
            decompte.Finish = false;
            decompte.Timer = 3f;
            decompte.visible = true;
            ListVie.Clear();
            gameOverMusicInstance.Stop();
            victoryMusicInstance.Stop();
            player.visible = true;
            ListBabyFrog.Clear();
            spawnBabyFrog();
            safeVieWidth = 10;
            player.Vie = 5;
            player.Respawn();
            gameMusicInstance.Play();
        }
        //Méthode qui fait les actions nécessaire quand le joueur meurt
        public void playerMort()
        {

            player.Respawn();
            player.Vie -= 1;
            ListVie.RemoveAt(player.Vie);
            player.EstMort = true;
            firstSpawn = false;

        }
        

        //Gestion de toutes les collisions
        public void entitiesCollisions(GameTime gameTime)
        {
            //Rectangle de l'eau et du bord de map
            Rectangle eauRectangleSprite = new Rectangle(0, 200, GraphicsDevice.Viewport.Width, 330);
            Rectangle bordDeMap = new Rectangle(0, (int)(GraphicsDevice.Viewport.Height - GraphicsDevice.Viewport.Height * 0.98f), GraphicsDevice.Viewport.Width, 35);

            ListRocher.ForEach(e =>
            {
                Rectangle coteGauche = new Rectangle();
                Rectangle coteDroit = new Rectangle();
                Rectangle coteBas = new Rectangle();
                coteGauche = new Rectangle((int)e.Left, (int)e.Y, 10, e.Height);
                coteDroit = new Rectangle((int)e.Right, (int)e.Y, 10, e.Height);
                coteBas = new Rectangle((int)e.Left, (int)e.Bottom, e.Width, 10);

                if (player.rectangleSprite.Intersects(coteGauche))
                {
                    player.positionSprite.X = player.positionSprite.X - 10;
                }
                if (player.rectangleSprite.Intersects(coteDroit))
                {
                    player.positionSprite.X = player.positionSprite.X + 10;
                }
                if (player.rectangleSprite.Intersects(coteBas))
                {

                    player.positionSprite.Y = player.positionSprite.Y + 10;
                }
            });

            if (player.rectangleSprite.Intersects(bordDeMap))
            {
                player.positionSprite.Y = player.positionSprite.Y + 10;
            }
            //Gestion des collisions avec les voitures (première ligne de voiture)
            ListVoiture.ForEach(e =>
            {
                if (player.rectangleSprite.Intersects(e.rectangleSprite))
                {
                    playerMort();
                    mortEcraserInstance.Play();
                }
            });
            //Gestion des collisions avec les serpents (à partir de 2 grenouilles récupérées)
            ListSerpent.ForEach(e =>
            {
                if (player.rectangleSprite.Intersects(e.rectangleSprite))
                {
                    playerMort();
                    mortEcraserInstance.Play();
                }
            });
            //Gestion des collisions avec les pickups (deuxième ligne de voiture)
            ListPickUp.ForEach(e =>
            {
                if (player.rectangleSprite.Intersects(e.rectangleSprite))
                {
                    playerMort();
                    mortEcraserInstance.Play();
                }
            });
            //Gestion des collisions avec les bus (troisième ligne de voiture)
            ListBus.ForEach(e =>
            {
                if (player.rectangleSprite.Intersects(e.rectangleSprite))
                {
                    playerMort();
                    mortEcraserInstance.Play();
                }
            });
            //Gestion des collisions avec les babyfrogs
            ListBabyFrog.ForEach(e =>
            {
                if (player.rectangleSprite.Intersects(e.rectangleSprite) && e.visible == true)
                {
                    player.Respawn();
                    e.visible = false;
                    firstSpawn = false;
                    babyFrogRecupInstance.Play();
                }
            });
            //Gestion des collisions avec les grosses voitures (dernière ligne de voiture)
            ListGrosseVoiture.ForEach(e =>
            {
                if (player.rectangleSprite.Intersects(e.rectangleSprite))
                {
                    playerMort();
                    mortEcraserInstance.Play();


                }
            });
            //Gestion des collisions avec l'eau

            if (player.rectangleSprite.Intersects(eauRectangleSprite))
            {
                safePlace = false;

                //Si contact avec une buche, player prend la vitesse x et n'est pas affecté par la mort dans l'eau
                ListBuche.ForEach(e =>
                {

                    if (player.rectangleSprite.Intersects(e.rectangleSprite))
                    {
                        safePlace = true;
                        player.positionSprite.X += e.Vitesse;
                    }

                });
                //Si contact avec un croco, player prend la vitesse x et n'est pas affecté par la mort dans l'eau
                ListCroco.ForEach(e =>
                {

                    if (player.rectangleSprite.Intersects(e.rectangleSprite))
                    {
                        safePlace = true;
                        player.positionSprite.X -= e.Vitesse;
                    }

                });
                //Si contact avec une tortue, player prend la vitesse x et n'est pas affecté par la mort dans l'eau
                ListTortue.ForEach(e =>
                {

                    if (player.rectangleSprite.Intersects(e.rectangleSprite))
                    {
                        safePlace = true;
                        player.positionSprite.X -= e.Vitesse;
                    }

                });
                //Si la variable reste false, le player meurt
                if (safePlace == false)
                {
                    playerMort();
                    mortNoyerInstance.Play();

                }

            }

        }
        public void updateEntities(GameTime gameTime)
        {
            //Si l'entité n'est plus visible, la supprime, ce qui économise de la mémoire
            ListVoiture.RemoveAll(v => v.visible == false);
            ListGrosseVoiture.RemoveAll(g => g.visible == false);
            ListPickUp.RemoveAll(p => p.visible == false);
            ListBus.RemoveAll(b => b.visible == false);
            ListBuche.RemoveAll(b => b.visible == false);
            ListCroco.RemoveAll(c => c.visible == false);
            ListTortue.RemoveAll(t => t.visible == false);

            //Appel à la méthode update pour chaque objet dans chaque liste
            for (var i = 0; i < ListVoiture.Count; i++)
            {
                ListVoiture[i].Update(gameTime);
            }
            for (var i = 0; i < ListGrosseVoiture.Count; i++)
            {
                ListGrosseVoiture[i].Update(gameTime);
            }
            for (var i = 0; i < ListPickUp.Count; i++)
            {
                ListPickUp[i].Update(gameTime);
            }
            for (var i = 0; i < ListBus.Count; i++)
            {
                ListBus[i].Update(gameTime);
            }
            for (var i = 0; i < ListBuche.Count; i++)
            {
                ListBuche[i].Update(gameTime);
            }
            for (var i = 0; i < ListCroco.Count; i++)
            {
                ListCroco[i].Update(gameTime);
            }
            for (var i = 0; i < ListTortue.Count; i++)
            {
                ListTortue[i].Update(gameTime);
            }
            for (var i = 0; i < ListBabyFrog.Count; i++)
            {
                ListBabyFrog[i].Update(gameTime);
                if (ListBabyFrog[i].visible == false)
                {
                    ListBabyFrog.RemoveAt(i);
                }


            }
            for (var i = 0; i < ListSerpent.Count; i++)
            {
                ListSerpent[i].Update(gameTime);
            }

        }
        public void respawnEnnemi(GameTime gameTime)
        {
            //Mise à jour des timer à chaque passage dans l'update
            voitureRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            pickUpRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            busRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            grosseVoitureRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            bucheRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            crocoRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            tortueRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            serpentRespawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Si le timer à atteint la valeur indiquée dans chaque objet, fait respawn un objet en question 
            if (voitureRespawn >= voiture.RespawnTime)
            {
                spawnVoiture();
                voitureRespawn = 0;
            }
            if (grosseVoitureRespawn >= grosseVoiture.RespawnTime)
            {
                spawnGrosseVoiture();
                grosseVoitureRespawn = 0;
            }
            if (pickUpRespawn >= pickUp.RespawnTime)
            {
                spawnPickUp();
                pickUpRespawn = 0;
            }
            if (busRespawn >= bus.RespawnTime)
            {
                spawnBus();
                busRespawn = 0;
            }
            if (bucheRespawn >= bucheGauche.RespawnTime)
            {
                spawnBuche();
                bucheRespawn = 0;
            }
            if (crocoRespawn >= croco.RespawnTime)
            {
                spawnCroco();
                crocoRespawn = 0;
            }
            if (tortueRespawn >= tortue.RespawnTime)
            {
                spawnTortue();
                tortueRespawn = 0;
            }
            //Uniquement si on a récupéré 3 babyFrog ou plus
            if (serpentRespawn >= serpent.RespawnTime && ListBabyFrog.Count < 3)
            {
                spawnSerpent();
                serpentRespawn = 0;
            }
            if (ListVie.Count < player.Vie)
            {
                spawnVie();
            }
            if (ListRocher.Count != 4)
            {

                spawnRocher();

            }


        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            
            tempsJoue += (float)gameTime.ElapsedGameTime.TotalSeconds;

            respawnEnnemi(gameTime);
            updateEntities(gameTime);
            entitiesCollisions(gameTime);
            //Si le decompte est fini, lance le timer de score
            if (decompte.Finish == true)
            {
                compteur.Update(gameTime);
            }
            //Indique que le timer est fini
            if (decompte.Timer < 1f)
            {
                decompte.Finish = true;
            }
            //Lance le decompte uniquement si on se trouve dans le menu jeu
            if (menu == "jeu")
            {
                decompte.Update(gameTime);
            }
            //Le joueur peut se déplacer uniquement si le décompte est fini
            if (decompte.Finish == true)
            {
                player.Update(gameTime);
            }
            //Si le player est mort lance un délai
            if (player.EstMort == true && firstSpawn == false)
            {
                decompte.Finish = false;
                decompte.Timer = 1.5f;
                player.EstMort = false;
                firstSpawn = true;

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //Dessine le menu principal et joue la musique du menu principal
            spriteBatch.Draw(mainMenu, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            menuThemeInstance.Play();
            menuThemeInstance.Volume = 0.3f;
            
            //Si on appuye sur 0 et que l'on se trouve pas dans le menu des scores, quitte
            if (Keyboard.GetState().IsKeyDown(Keys.D0) && menu != "highScore")
            {
                if (menu != "newHighScore")
                {
                    Exit();
                }

            }
            //Si on appuye sur 7 et que l'on se trouve pas dans le jeu, affiche le menu des scores
            if (Keyboard.GetState().IsKeyDown(Keys.D7) && menu != "jeu")
            {
                if (menu != "newHighScore")
                {
                    menu = "highScore";
                }

            }
            //Si on appuye sur 8 et que l'on se trouve dans le menu principal, lance le jeu
            if (Keyboard.GetState().IsKeyDown(Keys.D8) && menu == "mainMenu")
            {
                menu = "jeu";
            }
            if (menu == "highScore")
            {
                //Perme de revenir au menu principal
                if (Keyboard.GetState().IsKeyDown(Keys.D6))
                {
                    menu = "mainMenu";
                }

                //string scores = string.Join("\n", scoreManager.Highscores.Select(c => c.PlayerName + " | " + c.Value));
                string[] scores = scoreManager.Highscores.Select(c => c.PlayerName + " | " + c.Value).ToArray();
                for (int i = 0; i < scores.Length; i++)
                {
                    scores[i] = Regex.Replace(scores[i], ",", " Minutes ");
                    
                    scores[i] += " Secondes";
                }
                
                //Dessine le background, le rectangle des scores, les scores et l'indication pour revenir au menu
                spriteBatch.Draw(highScore, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.DrawString(Content.Load<SpriteFont>("compteur"), "Tableau des HighScores : \n\n" + string.Join("\n",scores), new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, GraphicsDevice.Viewport.Height / 2 - 500), Color.White);

            }
            if (menu == "newHighScore")
            {
                //Dessine le background et le rectangle du nom
                spriteBatch.Draw(newHighScore, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.Draw(blackScreen, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 500, GraphicsDevice.Viewport.Height / 2 - 300, 1000, 600), Color.White);
                //Timer pour pouvoir selectionner correctement
                chooseTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //Permet de remonter dans l'alphabet
                if (Keyboard.GetState().IsKeyDown(Keys.W) && chooseTimer >= 0.2)
                {
                    //Si l'index de l'alphabet est plus grand que 0 on récule normalement
                    if (indexAlphabet > 0)
                    {
                        indexAlphabet--;
                    }
                    //Sinon on se place à la fin
                    else
                    {
                        indexAlphabet = 24;
                    }
                    //Crée un tableau de char avec la valeur stringNom et la même chose avec l'alphabet
                    char[] nomAsChar = stringNom.ToCharArray();
                    char[] alphabetAsChar = alphabet.ToCharArray();
                    //Modifie le tableau nomAsChar à l'index séléctionné, avec notre index dans le tableau alphabet
                    nomAsChar[indexNom] = alphabetAsChar[indexAlphabet];
                    //Modifie le string avec la valeur du tableau
                    stringNom = new string(nomAsChar);
                    chooseTimer = 0;
                }
                //Permet de descendre dans l'alphabet
                if (Keyboard.GetState().IsKeyDown(Keys.S) && chooseTimer >= 0.2)
                {
                    //Si l'index est plut petit que 24 on avance normalement
                    if (indexAlphabet < 24)
                    {
                        indexAlphabet++;
                    }
                    //Sinon on revient au début
                    else
                    {
                        indexAlphabet = 0;
                    }
                    //Crée un tableau de char avec la valeur stringNom et la même chose avec l'alphabet
                    char[] nomAsChar = stringNom.ToCharArray();
                    char[] alphabetAsChar = alphabet.ToCharArray();
                    //Modifie le tableau nomAsChar à l'index séléctionné, avec notre index dans le tableau alphabet
                    nomAsChar[indexNom] = alphabetAsChar[indexAlphabet];
                    //Modifie le string avec la valeur du tableau
                    stringNom = new string(nomAsChar);
                    chooseTimer = 0;
                }
                //Si on appuye sur la touche 8, on valide notre choix pour la lettre en question
                if (Keyboard.GetState().IsKeyDown(Keys.D8) && chooseTimer >= 0.2)
                {
                    //Nous replace sur la lettre A pour la prochaine lettre
                    indexAlphabet = 0;
                    //Si l'index du nom est plus petit que 2 on continue
                    if (indexNom < 2)
                    {
                        indexNom++;

                    }
                    //Sinon si le score n'a pas encore été ajouté
                    else
                    {
                        if (scoreAjoute == false)
                        {
                                scoreManager.Add(new Score()
                                {
                                    PlayerName = stringNom,
                                    Value = compteur.Score,
                                });

                                ScoreManager.Save(scoreManager);
                                indexDic++;
                                scoreAjoute = true;
                               
                                
                               
                           // }

                        }
                        menu = "highScore";
                    }

                    chooseTimer = 0;
                }
                //Affiche la chaîne de nom et l'indication
                spriteBatch.DrawString(Content.Load<SpriteFont>("decompte"), stringNom, new Vector2((GraphicsDevice.DisplayMode.Width / 2) - 250, (GraphicsDevice.DisplayMode.Height / 2) - 150), Color.White);
                spriteBatch.DrawString(Content.Load<SpriteFont>("compteur"), "Utilisez le joystick pour changer la lettre" + Environment.NewLine + "puis appuyez sur $ pour la valider", new Vector2((GraphicsDevice.DisplayMode.Width / 2 - 300), (GraphicsDevice.DisplayMode.Height / 2) + 150), Color.White);

            }
            if (menu == "jeu")
            {
                //Si on appuye sur 0 quitte
                if (Keyboard.GetState().IsKeyDown(Keys.D0))
                {
                    Exit();
                }
                //Arrete la musique du menu et joue la musique du jeu
                menuThemeInstance.Stop();
                gameMusicInstance.Play();
                gameMusicInstance.Volume = 0.3f;
                //Dessine le background
                spriteBatch.Draw(mainBackGround, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                //Dessine tous les objets présents dans une liste pour chaque objets
                foreach (var l in ListVoiture)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListGrosseVoiture)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListBus)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListPickUp)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListBuche)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListCroco)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListTortue)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListBabyFrog)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListSerpent)
                {
                    l.Draw(spriteBatch);
                }
                foreach (var l in ListVie)
                {

                    l.Draw(spriteBatch);
                }
                //Dessine les decomptes
                decompte.Draw(spriteBatch);
                compteur.Draw(spriteBatch);
                //Dessine le player
                player.Draw(spriteBatch);

                //Si on a récupéré toutes les grenouilles
                if (ListBabyFrog.Count == 0)
                {
                    //Affiche l'écran de victoire, arrête le temps et désactive le joueur
                    spriteBatch.Draw(victoryScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    compteur.actif = false;
                    compteur.Draw(spriteBatch);
                    player.visible = false;
                    //Arrête la musique du jeu et lance la musique de victoire
                    gameMusicInstance.Stop();
                    victoryMusicInstance.Play();
                    victoryMusicInstance.Volume = 0.3f;
                    //Si on appuye sur 6, on va dans le menu pour enregistrer son score
                    if (Keyboard.GetState().IsKeyDown(Keys.D6))
                    {
                        menu = "newHighScore";
                        victoryMusicInstance.Stop();
                    }


                    //Si on appuye sur 8 on relance une partie
                    if (Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        restartGame();
                    }
                    //Et si on appuye sur 0 on quitte
                    if (Keyboard.GetState().IsKeyDown(Keys.D0))
                    {
                        Exit();
                    }

                }
                //Si le player n'a plus de vie
                if (player.Vie == 0)
                {
                    //Déssine le gameOverScreen, désactive le timer et le player
                    spriteBatch.Draw(gameOverScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    compteur.actif = false;
                    compteur.Draw(spriteBatch);
                    player.visible = false;
                    //Arrête la musique du jeu et lance la musique de gameOver
                    gameMusicInstance.Stop();
                    gameOverMusicInstance.Play();
                    gameOverMusicInstance.Volume = 0.3f;

                    //Si on appuye sur 8 on relance une partie
                    if (Keyboard.GetState().IsKeyDown(Keys.D8))
                    {
                        restartGame();
                    }
                    //Et si on appuye sur 0 on quitte
                    if (Keyboard.GetState().IsKeyDown(Keys.D0))
                    {
                        Exit();
                    }

                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
