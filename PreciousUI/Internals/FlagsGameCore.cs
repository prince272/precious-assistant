using DevExpress.Utils;
using DevExpress.XtraMap;
using PreciousUI.Internals.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Internals
{
    public enum GameLevel { Easy = 0, Middle = 1, High = 2, VeryHigh = 3 }
    public enum AnswerStatus { Win, Lose, WrongTry, ShowCountry }

    public enum ClickedAction { Unknown, ChangeLevel, NewGame, StartGame, FinishGame, SkipCountry, ShowCountry }

    public class CountryDataEventArgs : EventArgs
    {
        public Image Flag { get; set; }
        public string Name { get; set; }
    }

    public class ChoiseAnswerEventArgs : EventArgs
    {
        public AnswerStatus Status { get; set; }
        public double ScoreDelta { get; set; }
        public string CountryName { get; set; }
    }

    public class ScoreChangedAnswerEventArgs : EventArgs
    {
        public double Score { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }

    public class GameOverEventArgs : EventArgs
    {
        public string Time { get; set; }
    }

    public class FlagsGameCore : IDisposable
    {
        static readonly string FlagExtension = ".png";
        static readonly Random rand = new Random(DateTime.Now.Millisecond);
        readonly List<string> actualCountries;
        List<string> unusedCountries;
        Dictionary<string, Image> countriesFlags;
        GameLevel gameLevel = GameLevel.Easy;
        DateTime startGameTime;
        DateTime finishGameTime;
        string currentCountry;
        bool isGameActive;
        int wins;
        int losses;
        double score;

        long GamingTicks { get { return isGameActive ? (DateTime.Now - startGameTime).Ticks : (finishGameTime - startGameTime).Ticks; } }
        public bool IsGameActive { get { return isGameActive; } }
        public string CurrentCountryName { get { return currentCountry; } }

        public event EventHandler<GameOverEventArgs> GameOver;
        public event EventHandler<ScoreChangedAnswerEventArgs> ScoreChanged;
        public event EventHandler<CountryDataEventArgs> CountryChanged;
        public event EventHandler<ChoiseAnswerEventArgs> ChoiseAnswer;

        public FlagsGameCore(List<string> countriesList)
        {
            this.actualCountries = countriesList;
            this.countriesFlags = new Dictionary<string, Image>();
        }

        void RaiseEventGameOver()
        {
            if (GameOver != null)
            {
                GameOverEventArgs gameOverData = new GameOverEventArgs() { Time = GetGameTime() };
                GameOver(this, gameOverData);
            }
        }
        void RaiseEventScoreChanged()
        {
            if (ScoreChanged != null)
            {
                ScoreChangedAnswerEventArgs scoreData = new ScoreChangedAnswerEventArgs() { Score = score, Wins = wins, Losses = losses };
                ScoreChanged(this, scoreData);
            }
        }
        void RaiseEventCountryChanged(Image flag, string countryName)
        {
            if (CountryChanged != null)
            {
                CountryDataEventArgs countryData = new CountryDataEventArgs() { Flag = flag, Name = countryName };
                CountryChanged(this, countryData);
            }
        }
        void RaiseEventChoiseAnswer(AnswerStatus status, string countryName, double scoreDelta)
        {
            if (ChoiseAnswer != null)
            {
                ChoiseAnswerEventArgs answerData = new ChoiseAnswerEventArgs() { Status = status, CountryName = countryName, ScoreDelta = scoreDelta };
                ChoiseAnswer(this, answerData);
            }
        }
        Image GetCountryFlag(string countryName)
        {
            string flagPath = countryName.Replace(' ', '_') + FlagExtension;
            if (!this.countriesFlags.ContainsKey(countryName))
            {
                Image flagImage = CommonExtension.GetResourceImage("Flags.Big", flagPath);
                this.countriesFlags.Add(countryName, flagImage);
            }
            return this.countriesFlags[countryName];
        }
        void NextCountry()
        {
            if (unusedCountries.Count > 0)
            {
                int index = rand.Next(unusedCountries.Count);
                string countryName = this.unusedCountries[index];
                this.unusedCountries.Remove(countryName);
                this.currentCountry = countryName;
                RaiseEventCountryChanged(GetCountryFlag(countryName), countryName);
            }
            else
            {
                FinishGame();
            }
        }
        void RightChoice(string countryName)
        {
            int stepScore = GetScoreStep();
            this.wins++;
            this.score += stepScore;
            RaiseEventChoiseAnswer(AnswerStatus.Win, countryName, stepScore);
        }
        void WrongChoice(string countryName)
        {
            int stepScore = GetScoreStep();
            this.losses++;
            this.score -= stepScore;
            RaiseEventChoiseAnswer(AnswerStatus.Lose, countryName, -stepScore);
        }
        void WrongTry(string countryName)
        {
            int stepScore = 1;
            this.score -= stepScore;
            RaiseEventChoiseAnswer(AnswerStatus.WrongTry, countryName, -stepScore);
        }
        int GetScoreStep()
        {
            return 10 + 5 * (int)gameLevel;
        }
        string GetGameTime()
        {
            DateTime time = new DateTime(GamingTicks);
            bool isPrinting = false;
            string timeString = "";
            if (isPrinting || time.Hour > 0)
            {
                timeString += time.ToString("HH", CultureInfo.InvariantCulture) + "H ";
                isPrinting = true;
            }
            if (isPrinting || time.Minute > 0)
            {
                timeString += time.ToString("mm", CultureInfo.InvariantCulture) + "m ";
                isPrinting = true;
            }
            if (isPrinting || time.Second > 0)
            {
                timeString += time.ToString("ss", CultureInfo.InvariantCulture) + "s ";
                isPrinting = true;
            }
            timeString += time.ToString("ff", CultureInfo.InvariantCulture) + "ms ";
            return timeString;
        }

        public void StartGame(GameLevel level)
        {
            this.gameLevel = level;
            this.unusedCountries = new List<string>(actualCountries);
            this.currentCountry = "";
            this.wins = 0;
            this.losses = 0;
            this.score = 0;
            this.isGameActive = true;
            this.startGameTime = DateTime.Now;
            RaiseEventScoreChanged();
            NextCountry();
        }
        public void StartGame()
        {
            StartGame(GameLevel.Easy);
        }
        public void FinishGame()
        {
            this.finishGameTime = DateTime.Now;
            this.isGameActive = false;
            RaiseEventGameOver();
        }
        public bool TrySelectionCountry(string countryName)
        {
            if (!this.isGameActive)
                return false;
            bool isCorrectSelection = countryName == this.currentCountry;
            if (isCorrectSelection)
            {
                RightChoice(this.currentCountry);
                NextCountry();
            }
            else if (gameLevel != GameLevel.Easy)
            {
                WrongChoice(this.currentCountry);
                NextCountry();
            }
            else
            {
                WrongTry(countryName);
            }
            RaiseEventScoreChanged();
            return isCorrectSelection;
        }
        public void SkipCountry()
        {
            if (!isGameActive)
                return;
            WrongChoice(this.currentCountry);
            RaiseEventScoreChanged();
            NextCountry();
        }
        public void ShowCountry()
        {
            if (!isGameActive)
                return;
            double stepScore = 0.5;
            this.score -= stepScore;
            RaiseEventChoiseAnswer(AnswerStatus.ShowCountry, currentCountry, -stepScore);
            RaiseEventScoreChanged();
        }
        #region IDisposable implementation
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IEnumerable<string> keysCollection = new List<string>(this.countriesFlags.Keys);
                foreach (string key in keysCollection)
                {
                    if (countriesFlags[key] != null)
                    {
                        this.countriesFlags[key].Dispose();
                        this.countriesFlags[key] = null;
                    }
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~FlagsGameCore()
        {
            Dispose(false);
        }
        #endregion

        public static void LoadShapefileDataAdapter(ShapefileDataAdapter ShapefileDataAdapter)
        {
            Stream shpStream = CommonExtension.GetResourceStream("Data", "Countries.shp");
            Stream dbfStream = CommonExtension.GetResourceStream("Data", "Countries.dbf");
            ShapefileDataAdapter.LoadFromStream(shpStream, dbfStream);
        }
    }

}