using DevExpress.Utils;
using DevExpress.XtraMap;
using PreciousUI.Internals.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciousUI.Internals
{
    public abstract class OverlayManagerBase : IDisposable
    {
        Dictionary<string, Font> fontsCollection;

        protected Dictionary<string, Font> FontsCollection { get { return fontsCollection; } }

        protected OverlayManagerBase()
        {
            this.fontsCollection = CreateFonts();
        }

        protected abstract Dictionary<string, Font> CreateFonts();

        #region IDisposable implementation
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IEnumerable<string> keysCollection = new List<string>(this.fontsCollection.Keys);
                foreach (string key in keysCollection)
                {
                    if (fontsCollection[key] != null)
                    {
                        this.fontsCollection[key].Dispose();
                        this.fontsCollection[key] = null;
                    }
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~OverlayManagerBase()
        {
            Dispose(false);
        }
        #endregion
    }
    public class FlagsGameOverlayManager : OverlayManagerBase
    {
        static readonly Color HotTrackedColor = Color.FromArgb(128, 135, 135, 135);
        static readonly Color ChooseLevelBackgroundColor = Color.FromArgb(32, Color.Gray);

        readonly string[] levelNames;
        readonly Dictionary<string, string> levelDescriptions;
        Dictionary<MapOverlayImageItem, MapOverlayTextItem> levelItems;
        Dictionary<MapOverlayTextItem, MapOverlayTextItem> levelSelectorItems;
        MapOverlayTextItem selectedLevelItem;

        MapOverlay newGameOverlay, chooseLevelOverlay;
        MapOverlayTextItem startGameItem;
        MapOverlayTextItem levelDescItem;

        MapOverlay infoOverlay;
        MapOverlayTextItem infoTextItem;

        MapOverlay countryFlagOverlay;
        MapOverlayImageItem flagItem;
        MapOverlayTextItem countryName;

        MapOverlay skipCountryOverlay, showCountryOverlay;
        MapOverlay restartGameOverlay, finishGameOverlay;
        MapOverlayTextItem skipCountryItem, showCountryItem;
        MapOverlayTextItem restartGameItem, finishGameItem;

        MapOverlay statisticGameOverlay;
        MapOverlayImageItem scoreImage, timeImage;
        MapOverlayTextItem scoreLabel, timeLabel;
        MapOverlayTextItem scoreItem, winsItem, lossesItem, timeItem;

        MapOverlay gameOverOverlay, scoreOverOverlay;
        MapOverlayTextItem newGameItem, scoreOverItem;

        public FlagsGameOverlayManager(string[] levelNames, Dictionary<string, string> levelDescriptions)
        {
            this.levelNames = levelNames;
            this.levelDescriptions = levelDescriptions;
            CreateOverlays();
        }

        void CreateOverlays()
        {
            CreateNewGameOverlay();
            CreateChooseLevelOverlay();
            CreateCountryGameOverlay();
            CreateStatisticGameOverlay();
            CreateOperationGameOverlay();
            CreateScoreOverOverlay();
            CreateGameOverOverlay();
            CreateGameInfoOverlay();
        }
        void CreateNewGameOverlay()
        {
            newGameOverlay = new MapOverlay() { Alignment = ContentAlignment.MiddleCenter, Padding = new Padding(30) };
            MapOverlayTextItem titleText = new MapOverlayTextItem() { Text = "New Game", Alignment = ContentAlignment.TopCenter, JoiningOrientation = Orientation.Vertical, TextAlignment = ContentAlignment.MiddleCenter, Padding = new Padding(35, 0, 35, 12) };
            titleText.TextStyle.Font = FontsCollection["title"];
            MapOverlayTextItem infoText = new MapOverlayTextItem() { Text = "Choose your level", Alignment = ContentAlignment.TopCenter, JoiningOrientation = Orientation.Vertical, TextAlignment = ContentAlignment.MiddleCenter, Padding = new Padding(35, 0, 35, 130), Size = new Size(470, 0) };
            infoText.TextStyle.Font = FontsCollection["note"];
            levelDescItem = new MapOverlayTextItem() { Alignment = ContentAlignment.BottomCenter, JoiningOrientation = Orientation.Vertical, TextAlignment = ContentAlignment.MiddleCenter, Margin = new Padding(0, 30, 0, 30), Size = new Size(500, 0) };
            levelDescItem.TextStyle.Font = FontsCollection["default"];
            startGameItem = new MapOverlayTextItem() { Text = "START GAME", Alignment = ContentAlignment.BottomCenter, JoiningOrientation = Orientation.Vertical, TextAlignment = ContentAlignment.MiddleCenter, Padding = new Padding(20, 10, 20, 10) };
            startGameItem.TextStyle.Font = FontsCollection["button"];
            startGameItem.TextStyle.TextColor = Color.White;
            startGameItem.BackgroundStyle.Fill = Color.FromArgb(0x74, 0x9F, 0xDF);
            startGameItem.HotTrackedStyle.Fill = Color.FromArgb(0xAF, 0x74, 0x9F, 0xDF);
            newGameOverlay.Items.AddRange(new MapOverlayItemBase[] { titleText, infoText, startGameItem, levelDescItem });
        }
        void CreateChooseLevelOverlay()
        {
            chooseLevelOverlay = new MapOverlay();
            levelItems = new Dictionary<MapOverlayImageItem, MapOverlayTextItem>();
            levelSelectorItems = new Dictionary<MapOverlayTextItem, MapOverlayTextItem>();
            foreach (string levelName in this.levelNames)
            {
                string levelIconName = levelName.ToLower().Replace(' ', '_');
                MapOverlayImageItem iconItem = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", levelIconName + ".png"), JoiningOrientation = Orientation.Horizontal, Padding = new Padding(37, 20, 37, 20) };
                MapOverlayTextItem nameItem = new MapOverlayTextItem() { Text = levelName.ToUpper(), Size = new Size(80, 0), Padding = new Padding(0, 20, 0, 20), TextAlignment = ContentAlignment.BottomCenter };
                nameItem.TextStyle.Font = FontsCollection["level"];
                nameItem.HotTrackedStyle.Fill = ChooseLevelBackgroundColor;
                MapOverlayTextItem selectorItem = new MapOverlayTextItem();
                selectorItem.HotTrackedStyle.Fill = ChooseLevelBackgroundColor;
                this.levelItems.Add(iconItem, nameItem);
                this.levelSelectorItems.Add(nameItem, selectorItem);
            }
            chooseLevelOverlay.Items.AddRange(this.levelItems.Keys);
            chooseLevelOverlay.Items.AddRange(this.levelItems.Values);
            chooseLevelOverlay.Items.AddRange(this.levelSelectorItems.Values);
        }
        void CreateGameOverOverlay()
        {
            gameOverOverlay = new MapOverlay() { Alignment = ContentAlignment.MiddleCenter, JoiningOrientation = Orientation.Vertical, Padding = new Padding(20) };
            MapOverlayTextItem titleText = new MapOverlayTextItem() { Text = "GAME OVER", Alignment = ContentAlignment.BottomCenter, JoiningOrientation = Orientation.Vertical, TextAlignment = ContentAlignment.MiddleCenter, Padding = new Padding(0, 0, 0, 15) };
            titleText.TextStyle.Font = FontsCollection["title"];
            newGameItem = new MapOverlayTextItem() { Text = "NEW GAME", Alignment = ContentAlignment.BottomCenter, JoiningOrientation = Orientation.Vertical, TextAlignment = ContentAlignment.MiddleCenter, Padding = new Padding(20, 10, 20, 10) };
            newGameItem.TextStyle.Font = FontsCollection["button"];
            newGameItem.TextStyle.TextColor = Color.White;
            newGameItem.BackgroundStyle.Fill = Color.FromArgb(0x40, 0xAB, 0x5B);
            newGameItem.HotTrackedStyle.Fill = Color.FromArgb(0xAF, 0x40, 0xAB, 0x5B);
            gameOverOverlay.Items.AddRange(new MapOverlayItemBase[] { newGameItem, titleText });
        }
        void CreateScoreOverOverlay()
        {
            scoreOverOverlay = new MapOverlay() { Alignment = ContentAlignment.TopCenter, Margin = new Padding(0, 12, 0, 0), Padding = new Padding(0, 15, 0, 5) };
            MapOverlayTextItem scoreOverLabel = new MapOverlayTextItem() { Text = "SCORE:" };
            scoreOverLabel.TextStyle.Font = FontsCollection["score_over"];
            scoreOverItem = new MapOverlayTextItem() { Padding = new Padding(10, 0, 0, 0) };
            scoreOverItem.TextStyle.Font = FontsCollection["score_over"];
            scoreOverOverlay.Items.AddRange(new MapOverlayItemBase[] { scoreOverLabel, scoreOverItem });
        }
        void CreateCountryGameOverlay()
        {
            countryFlagOverlay = new MapOverlay() { Alignment = ContentAlignment.TopLeft, Margin = new Padding(12, 12, 0, 0) };
            countryName = new MapOverlayTextItem() { Alignment = ContentAlignment.BottomCenter, TextAlignment = ContentAlignment.MiddleCenter, Padding = new Padding(12, 0, 12, 15), Size = new Size(150, 0) };
            countryName.TextStyle.Font = FontsCollection["bold"];
            flagItem = new MapOverlayImageItem() { Alignment = ContentAlignment.TopCenter, Padding = new Padding(20) };
            countryFlagOverlay.Items.AddRange(new MapOverlayItemBase[] { countryName, flagItem });
        }
        void CreateStatisticGameOverlay()
        {
            statisticGameOverlay = new MapOverlay() { JoiningOrientation = Orientation.Vertical, Padding = new Padding(15, 3, 10, 3), Margin = new Padding(0, 0, 0, 7) };
            scoreImage = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "score.png"), Margin = new Padding(8, 12, 10, 12) };
            scoreLabel = new MapOverlayTextItem() { Text = "Score:", Padding = new Padding(0, 10, 5, 10) };
            scoreLabel.TextStyle.Font = FontsCollection["default"];
            scoreItem = new MapOverlayTextItem() { Padding = new Padding(0, 10, 10, 10) };
            scoreItem.TextStyle.Font = FontsCollection["default"];

            MapOverlayImageItem winsImage = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "wins.png"), Margin = new Padding(8, 12, 10, 12) };
            MapOverlayTextItem winsLabel = new MapOverlayTextItem() { Text = "Wins:", Padding = new Padding(0, 10, 5, 10) };
            winsLabel.TextStyle.Font = FontsCollection["default"];
            winsItem = new MapOverlayTextItem() { Padding = new Padding(0, 10, 10, 10) };
            winsItem.TextStyle.Font = FontsCollection["default"];

            MapOverlayImageItem lossesImage = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "loses.png"), Margin = new Padding(8, 12, 10, 12) };
            MapOverlayTextItem lossesLabel = new MapOverlayTextItem() { Text = "Losses:", Padding = new Padding(0, 10, 5, 10) };
            lossesLabel.TextStyle.Font = FontsCollection["default"];
            lossesItem = new MapOverlayTextItem() { Padding = new Padding(0, 10, 10, 10) };
            lossesItem.TextStyle.Font = FontsCollection["default"];

            timeImage = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "time.png"), Margin = new Padding(8, 12, 10, 12) };
            timeLabel = new MapOverlayTextItem() { Text = "Time:", Padding = new Padding(0, 10, 5, 10) };
            timeLabel.TextStyle.Font = FontsCollection["default"];
            timeItem = new MapOverlayTextItem() { Padding = new Padding(0, 10, 10, 10) };
            timeItem.TextStyle.Font = FontsCollection["default"];
            statisticGameOverlay.Items.AddRange(new MapOverlayItemBase[] { scoreImage, scoreLabel, scoreItem, winsImage, winsLabel, winsItem, lossesImage, lossesLabel, lossesItem, timeImage, timeLabel, timeItem });
        }
        void CreateOperationGameOverlay()
        {
            restartGameOverlay = new MapOverlay() { Alignment = ContentAlignment.TopRight, JoiningOrientation = Orientation.Vertical, Margin = new Padding(0, 12, 12, 0) };
            MapOverlayImageItem restartIcon = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "restart.png"), Margin = new Padding(10, 6, 7, 6) };
            restartGameItem = new MapOverlayTextItem() { Text = "RESTART GAME", Alignment = ContentAlignment.TopLeft, Margin = new Padding(1), Padding = new Padding(6), Size = new Size(110, 0) };
            restartGameItem.TextStyle.Font = FontsCollection["nav_button"];
            restartGameItem.HotTrackedStyle.Fill = HotTrackedColor;
            restartGameOverlay.Items.AddRange(new MapOverlayItemBase[] { restartIcon, restartGameItem });

            finishGameOverlay = new MapOverlay() { Alignment = ContentAlignment.TopRight, JoiningOrientation = Orientation.Vertical, Margin = new Padding(0, 0, 12, 0) };
            MapOverlayImageItem finishIcon = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "finish.png"), Margin = new Padding(10, 6, 7, 6) };
            finishGameItem = new MapOverlayTextItem() { Text = "FINISH GAME", Alignment = ContentAlignment.TopLeft, Margin = new Padding(1), Padding = new Padding(6), Size = new Size(110, 0) };
            finishGameItem.TextStyle.Font = FontsCollection["nav_button"];
            finishGameItem.HotTrackedStyle.Fill = HotTrackedColor;
            finishGameOverlay.Items.AddRange(new MapOverlayItemBase[] { finishIcon, finishGameItem });

            skipCountryOverlay = new MapOverlay() { Alignment = ContentAlignment.TopRight, JoiningOrientation = Orientation.Vertical, Margin = new Padding(0, 12, 12, 0) };
            MapOverlayImageItem skipIcon = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "skip.png"), Margin = new Padding(10, 6, 7, 6) };
            skipCountryItem = new MapOverlayTextItem() { Text = "SKIP COUNTRY", Alignment = ContentAlignment.TopLeft, Margin = new Padding(1), Padding = new Padding(6), Size = new Size(110, 0) };
            skipCountryItem.TextStyle.Font = FontsCollection["nav_button"];
            skipCountryItem.HotTrackedStyle.Fill = HotTrackedColor;
            skipCountryOverlay.Items.AddRange(new MapOverlayItemBase[] { skipIcon, skipCountryItem });

            showCountryOverlay = new MapOverlay() { Alignment = ContentAlignment.TopRight, JoiningOrientation = Orientation.Vertical, Margin = new Padding(0, 0, 12, 0) };
            MapOverlayImageItem showIcon = new MapOverlayImageItem() { Image = CommonExtension.GetResourceImage("FlagsGame", "show.png"), Margin = new Padding(10, 6, 7, 6) };
            showCountryItem = new MapOverlayTextItem() { Text = "SHOW COUNTRY", Alignment = ContentAlignment.TopLeft, Margin = new Padding(1), Padding = new Padding(6), Size = new Size(110, 0) };
            showCountryItem.TextStyle.Font = FontsCollection["nav_button"];
            showCountryItem.HotTrackedStyle.Fill = HotTrackedColor;
            showCountryOverlay.Items.AddRange(new MapOverlayItemBase[] { showIcon, showCountryItem });
        }
        void CreateGameInfoOverlay()
        {
            infoOverlay = new MapOverlay() { Margin = new Padding(0, 12, 0, 0) };
            infoTextItem = new MapOverlayTextItem() { TextAlignment = ContentAlignment.MiddleLeft, Padding = new Padding(20), Size = new Size(250, 0) };
            infoTextItem.TextStyle.Font = FontsCollection["info"];
            infoOverlay.Items.Add(infoTextItem);
        }
        void ScoreOverMode()
        {
            statisticGameOverlay.Alignment = ContentAlignment.TopCenter;
            timeImage.Visible = true;
            timeLabel.Visible = true;
            timeItem.Visible = true;
            scoreImage.Visible = false;
            scoreLabel.Visible = false;
            scoreItem.Visible = false;
        }
        void ScoreGamingMode()
        {
            statisticGameOverlay.Alignment = ContentAlignment.BottomCenter;
            timeImage.Visible = false;
            timeLabel.Visible = false;
            timeItem.Visible = false;
            scoreImage.Visible = true;
            scoreLabel.Visible = true;
            scoreItem.Visible = true;
        }
        void SetDefaultStyleLevelItems()
        {
            foreach (KeyValuePair<MapOverlayImageItem, MapOverlayTextItem> levelPair in this.levelItems)
                ChangeSelectionOverlayItem(levelPair.Value, false);
        }
        void ChangeSelectionOverlayItem(MapOverlayTextItem levelItem, bool isSelect)
        {
            if (isSelect)
            {
                string key = levelItem.Text.ToLower();
                this.levelDescItem.Text = levelDescriptions[key];
                this.selectedLevelItem = levelItem;
            }
            levelSelectorItems[levelItem].BackgroundStyle.Fill = isSelect ? GetLevelColor() : ChooseLevelBackgroundColor;
        }
        void RecalculateChooseLevelArrangement(OverlayArrangement[] overlayArrangement)
        {
            const int itemsCount = 4;
            Rectangle newGameRect = overlayArrangement[0].OverlayLayout;
            overlayArrangement[1].OverlayLayout = Rectangle.FromLTRB(newGameRect.Left, newGameRect.Top + 120, newGameRect.Right, newGameRect.Top + 120 + 110);
            double itemsWidth = overlayArrangement[1].ItemLayouts[itemsCount - 1].Right - overlayArrangement[1].ItemLayouts[0].Left;
            int itemsOffset = (int)((newGameRect.Width - itemsWidth) / 2.0);
            for (int i = 0; i < itemsCount; i++)
            {
                Rectangle iconRect = overlayArrangement[1].ItemLayouts[i];
                Rectangle textRect = overlayArrangement[1].ItemLayouts[i + itemsCount];
                iconRect = Rectangle.FromLTRB(iconRect.Left + itemsOffset, iconRect.Top, iconRect.Right + itemsOffset, iconRect.Bottom);
                textRect = Rectangle.FromLTRB(iconRect.Left, iconRect.Top, iconRect.Right, iconRect.Bottom + 25);
                overlayArrangement[1].ItemLayouts[i] = iconRect;
                overlayArrangement[1].ItemLayouts[i + itemsCount] = textRect;
                overlayArrangement[1].ItemLayouts[i + itemsCount * 2] = Rectangle.FromLTRB(textRect.Left, textRect.Bottom, textRect.Right, textRect.Bottom + 5);
            }
        }
        void RecalculatScoreOverArrangement(OverlayArrangement[] overlayArrangement)
        {
            Rectangle scoreRect = overlayArrangement[0].OverlayLayout;
            Rectangle statisticRect = overlayArrangement[2].OverlayLayout;
            overlayArrangement[0].OverlayLayout = Rectangle.FromLTRB(statisticRect.Left, scoreRect.Top, statisticRect.Right, scoreRect.Bottom);
            int itemsCount = overlayArrangement[0].ItemLayouts.Length;
            double itemsWidth = overlayArrangement[0].ItemLayouts[itemsCount - 1].Right - overlayArrangement[0].ItemLayouts[0].Left;
            int itemsOffset = (int)((statisticRect.Width - itemsWidth) / 2.0);
            for (int i = 0; i < itemsCount; i++)
            {
                Rectangle itemRect = overlayArrangement[0].ItemLayouts[i];
                overlayArrangement[0].ItemLayouts[i] = Rectangle.FromLTRB(itemRect.Left + itemsOffset, itemRect.Top, itemRect.Left + itemsOffset + itemRect.Width, itemRect.Bottom);
            }
        }
        void RecalculateInfoArrangement(OverlayArrangement[] overlayArrangement)
        {
            Rectangle countryRect = overlayArrangement[0].OverlayLayout;
            Rectangle infoRect = overlayArrangement[1].OverlayLayout;
            overlayArrangement[1].OverlayLayout = Rectangle.FromLTRB(infoRect.Left, countryRect.Top, infoRect.Right, countryRect.Bottom);
            double itemsHeight = overlayArrangement[1].ItemLayouts[0].Height;
            int itemsOffset = (int)((overlayArrangement[0].ItemLayouts[1].Height - itemsHeight) / 2.0);
            Rectangle itemRect = overlayArrangement[1].ItemLayouts[0];
            overlayArrangement[1].ItemLayouts[0] = Rectangle.FromLTRB(itemRect.Left, itemRect.Top + itemsOffset, itemRect.Right, itemRect.Top + itemsOffset + itemRect.Height);
        }
        Color GetLevelColor()
        {
            string levelName = selectedLevelItem != null ? selectedLevelItem.Text.ToLower() : String.Empty;
            if (levelName == this.levelNames[1])
                return Color.FromArgb(0x40, 0xAB, 0x5B);
            if (levelName == this.levelNames[2])
                return Color.FromArgb(0xFF, 0xAE, 0x00);
            if (levelName == this.levelNames[3])
                return Color.FromArgb(0xFF, 0x00, 0x00);
            return Color.FromArgb(0x74, 0x9F, 0xDF);
        }

        protected override Dictionary<string, Font> CreateFonts()
        {
            Dictionary<string, Font> collection = new Dictionary<string, Font>();
            collection.Add("title", new Font(AppearanceObject.DefaultFont.FontFamily, 23, FontStyle.Bold));
            collection.Add("note", new Font(AppearanceObject.DefaultFont.FontFamily, 11, FontStyle.Regular));
            collection.Add("button", new Font(AppearanceObject.DefaultFont.FontFamily, 10, FontStyle.Bold));
            collection.Add("nav_button", new Font(AppearanceObject.DefaultFont.FontFamily, 9, FontStyle.Bold));
            collection.Add("score_over", new Font(AppearanceObject.DefaultFont.FontFamily, 17, FontStyle.Regular));
            collection.Add("default", new Font(AppearanceObject.DefaultFont.FontFamily, 11, FontStyle.Regular));
            collection.Add("bold", new Font(AppearanceObject.DefaultFont.FontFamily, 11, FontStyle.Bold));
            collection.Add("level", new Font(AppearanceObject.DefaultFont.FontFamily, 8, FontStyle.Bold));
            collection.Add("info", new Font(AppearanceObject.DefaultFont.FontFamily, 12, FontStyle.Regular));
            return collection;
        }

        public void OverlaysArranged(OverlayArrangement[] overlayArrangement)
        {
            if (newGameOverlay.Visible && chooseLevelOverlay.Visible && overlayArrangement.Length >= 2)
                RecalculateChooseLevelArrangement(overlayArrangement);
            if (statisticGameOverlay.Visible && scoreOverOverlay.Visible && overlayArrangement.Length >= 3)
                RecalculatScoreOverArrangement(overlayArrangement);
            if (countryFlagOverlay.Visible && infoOverlay.Visible && overlayArrangement.Length >= 3)
                RecalculateInfoArrangement(overlayArrangement);
        }
        public MapOverlay[] GetOverlays()
        {
            return new MapOverlay[] { newGameOverlay, chooseLevelOverlay, scoreOverOverlay, gameOverOverlay, countryFlagOverlay, infoOverlay, statisticGameOverlay, skipCountryOverlay, showCountryOverlay, restartGameOverlay, finishGameOverlay };
        }
        public void HidePanels()
        {
            newGameOverlay.Visible = false;
            chooseLevelOverlay.Visible = false;
            countryFlagOverlay.Visible = false;
            statisticGameOverlay.Visible = false;
            scoreOverOverlay.Visible = false;
            gameOverOverlay.Visible = false;
            skipCountryOverlay.Visible = false;
            showCountryOverlay.Visible = false;
            restartGameOverlay.Visible = false;
            finishGameOverlay.Visible = false;
            infoOverlay.Visible = false;
        }
        public void ShowGameOverOverlay(string time)
        {
            HidePanels();
            ScoreOverMode();
            timeItem.Text = time;
            scoreOverItem.Text = scoreItem.Text;
            statisticGameOverlay.Visible = true;
            scoreOverOverlay.Visible = true;
            gameOverOverlay.Visible = true;
        }
        public void ShowNewGameOverlay()
        {
            HidePanels();
            newGameOverlay.Visible = true;
            chooseLevelOverlay.Visible = true;
        }
        public void ShowGameInfoOverlay()
        {
            HidePanels();
            ScoreGamingMode();
            countryFlagOverlay.Visible = true;
            statisticGameOverlay.Visible = true;
            skipCountryOverlay.Visible = true;
            showCountryOverlay.Visible = true;
            restartGameOverlay.Visible = true;
            finishGameOverlay.Visible = true;
            infoOverlay.Visible = true;
        }


        public void SetInfoMessage(string text)
        {
            infoTextItem.Text = text;
        }
        public void SetScore(double score, int wins, int losses)
        {
            this.scoreItem.Text = score.ToString();
            this.winsItem.Text = wins.ToString();
            this.lossesItem.Text = losses.ToString();
        }
        public void SetCountry(Image flag, string name)
        {
            this.flagItem.Image = flag;
            this.countryName.Text = name;
        }
        public void SetCountryNameVisibility(bool isVisible)
        {
            this.countryName.Visible = isVisible;
        }
        public void SetCountryOperationsVisibility(bool isVisible)
        {
            this.skipCountryOverlay.Visible = isVisible;
            this.showCountryOverlay.Visible = isVisible;
        }
        public void SelectDefaultLevel()
        {
            SetDefaultStyleLevelItems();
            if (this.levelItems.Count > 0)
                ChangeSelectionOverlayItem(this.levelItems.First().Value, true);
        }
        public GameLevel GetSelectedLevel()
        {
            string levelName = selectedLevelItem != null ? selectedLevelItem.Text.ToLower() : String.Empty;
            if (levelName == this.levelNames[1])
                return GameLevel.Middle;
            if (levelName == this.levelNames[2])
                return GameLevel.High;
            if (levelName == this.levelNames[3])
                return GameLevel.VeryHigh;
            return GameLevel.Easy;
        }

        public ClickedAction GetClickedAction(MapOverlayItemBase clickedItem)
        {
            if (clickedItem == this.skipCountryItem)
                return ClickedAction.SkipCountry;
            if (clickedItem == this.showCountryItem)
                return ClickedAction.ShowCountry;
            if (clickedItem == this.finishGameItem)
                return ClickedAction.FinishGame;
            if (clickedItem == this.startGameItem)
                return ClickedAction.StartGame;
            if (clickedItem == this.restartGameItem || clickedItem == this.newGameItem)
                return ClickedAction.NewGame;
            KeyValuePair<MapOverlayImageItem, MapOverlayTextItem> levelPair = this.levelItems.FirstOrDefault(x => x.Value.Equals(clickedItem) || x.Key.Equals(clickedItem));
            if (levelPair.Value != null)
            {
                SetDefaultStyleLevelItems();
                ChangeSelectionOverlayItem(levelPair.Value, true);
                return ClickedAction.ChangeLevel;
            }
            return ClickedAction.Unknown;
        }
    }
    public static class MapOverlayUtils
    {
        static MapOverlay bingLogoOverlay;
        static MapOverlay bingCopyrightOverlay;

        public static MapOverlay BingLogoOverlay
        {
            get
            {
                if (bingLogoOverlay == null)
                    bingLogoOverlay = CreateBingLogoOverlay();
                return bingLogoOverlay;
            }
        }

        public static MapOverlay BingCopyrightOverlay
        {
            get
            {
                if (bingCopyrightOverlay == null)
                    bingCopyrightOverlay = CreateBingCopyrightOverlay();
                return bingCopyrightOverlay;
            }
        }

        public static MapOverlay[] GetBingOverlays()
        {
            return new MapOverlay[] { BingLogoOverlay, BingCopyrightOverlay };
        }

        static MapOverlay CreateBingLogoOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.BottomLeft, Margin = new Padding(10, 0, 0, 10) };
            MapOverlayImageItem logoItem = new MapOverlayImageItem() { Padding = new Padding() };
            overlay.Items.Add(logoItem);
            return overlay;
        }

        static MapOverlay CreateBingCopyrightOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.BottomRight, Margin = new Padding(0, 0, 10, 10) };
            MapOverlayTextItem copyrightItem = new MapOverlayTextItem() { Padding = new Padding(5), Text = "Copyright © 2017 Microsoft and its suppliers. All rights reserved." };
            overlay.Items.Add(copyrightItem);
            return overlay;
        }

        static MapOverlay CreateOSMCopyrightOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.BottomRight, Margin = new Padding(0, 0, 10, 10) };
            MapOverlayTextItem copyrightItem = new MapOverlayTextItem() { Padding = new Padding(5), Text = "© OpenStreetMap contributors" };
            overlay.Items.Add(copyrightItem);
            return overlay;
        }

        static MapOverlay CreateMedalsOverlay()
        {
            MapOverlay overlay = new MapOverlay() { Alignment = ContentAlignment.TopCenter, Margin = new Padding(10, 10, 10, 10) };
            MapOverlayTextItem medalsItem = new MapOverlayTextItem() { Padding = new Padding(5), Text = "2016 Summer Olympics Medal Result" };
            medalsItem.TextStyle.Font = new Font(AppearanceObject.DefaultFont.FontFamily, 16, FontStyle.Regular);
            overlay.Items.Add(medalsItem);
            return overlay;
        }

        public static MapOverlayItemBase GetClickedOverlayItem(MapHitInfo hitInfo)
        {
            if (hitInfo.InUIElement)
            {
                MapOverlayHitInfo overlayHitInfo = hitInfo.UiHitInfo as MapOverlayHitInfo;
                if (overlayHitInfo != null)
                    return overlayHitInfo.OverlayItem;
            }
            return null;
        }
    }
}
