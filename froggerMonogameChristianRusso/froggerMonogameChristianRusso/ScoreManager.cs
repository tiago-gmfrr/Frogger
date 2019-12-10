/*
 * Author : Tiago Gama
 * Version: V1.0
 * Date : 08.10.2019
 * Classe : ScoreManager 
 * Source : https://github.com/Oyyou/MonoGame_Tutorials
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace froggerMonogameChristianRusso
{
    public class ScoreManager
    {
        private static string cheminVersExe = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private static string _fileName = cheminVersExe + "\\Data\\scores.xml"; // Since we don't give a path, this'll be saved in the "bin" folder
        public List<Score> Highscores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager()
          : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            if (playerExist(score.PlayerName) == false)
            {
                Scores.Add(score);
            }
            else
            {
                foreach (Score s in Scores)
                {
                    if (score.PlayerName == s.PlayerName)
                    {
                        if (Convert.ToDouble(score.Value) < Convert.ToDouble(s.Value))
                        {
                            s.Value = score.Value;
                        }                    
                    }
                }
            }
            

            Scores = Scores.OrderBy(c => c.Value).ToList(); // Orders the list so that the higher scores are first

            UpdateHighscores();
        }
        //Regarde si le nom entré existe déjà dans le fichier XML, retourne un bool
        public bool playerExist(string pn)
        {
            bool playerExist = false;
            foreach (Score s in Scores)
            {
                string nom = s.PlayerName;
                if (nom == pn)
                {
                    playerExist = true;
                }
            }
            return playerExist;
        }

        public static ScoreManager Load()
        {
            // If there isn't a file to load - create a new instance of "ScoreManager"
            if (!File.Exists(_fileName))
                return new ScoreManager();

            // Otherwise we load the file

            using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serializer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(10).ToList(); // Takes the first 5 elements

            string modifiedScore;

            foreach (Score s in Highscores)
            {
                
                //modifiedScore = s.Value.Replace(",", "  Minutes ");
                //modifiedScore += " Secondes";
                //s.Value = modifiedScore;
            }
        }

        public static void Save(ScoreManager scoreManager)
        {
            // Overrides the file if it alreadt exists
            using (var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
