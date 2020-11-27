using DevExpress.Utils;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using PreciousUI.Internals;
using PreciousUI.Internals.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PreciousUI.Controls {

    [ToolboxItem(true)]
    public class VirtualAssistantControl : PictureEdit {
        private IReadOnlyCollection<EmotionData> emotions;
        private int prevFrame = 0;
        private int emotionCount = 0;
        private EmotionState emotionState = EmotionState.Greeting;
        private Random randomEmotion = new Random();
        private bool fVisible;

        public VirtualAssistantControl() {
            SetStyle(ControlStyles.Selectable, false);
            BackColor = Color.Black;
            Size = new System.Drawing.Size(428, 500);
            BorderStyle = BorderStyles.NoBorder;
            Properties.AllowDisposeImage = true;
            Properties.PictureAlignment = ContentAlignment.TopCenter;
            Properties.AllowFocused = false;
            Properties.ReadOnly = true;
            Properties.ShowMenu = false;
            Properties.ShowCameraMenuItem = CameraMenuItemVisibility.Never;
            Properties.Caption.Alignment = ContentAlignment.BottomCenter;
            Properties.Caption.Appearance.BackColor = Color.FromArgb(150, 0, 0, 0);
            Properties.Caption.ContentPadding = new Padding(10, 10, 10, 20);
            Properties.Caption.Visible = true;
            Properties.Caption.Offset = new Point(0, 0);
        }

        #region Overrides

        protected override void UpdateViewInfo(Graphics g) {
            BaseAnimationInfo animationInfo = GetAnimationInfo();
            if (animationInfo != null) {
                if (prevFrame != animationInfo.CurrentFrame) {
                    AnimationFrameChanged(animationInfo);
                    animationInfo.IsFinalFrameDrawn = (animationInfo.CurrentFrame == animationInfo.FrameCount - 2);
                    if (animationInfo.IsFinalFrameDrawn)
                        AnimationFrameCompleted(animationInfo);
                    prevFrame = animationInfo.CurrentFrame;
                }
            }

            base.UpdateViewInfo(g);
        }

        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);
            if (DesignMode) return;
            if (!fVisible) {
                InitEmotion();
                LoadEmotionDataCore(emotionState);
                fVisible = true;
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (emotions != null) {
                    foreach (var data in emotions) {
                        if (data.StreamImage != null) {
                            data.StreamImage.Close();
                            data.StreamImage.Dispose();
                            data.StreamImage = null;
                        }
                    }
                }
                emotions = null;
            }
            base.Dispose(disposing);
        }
        #endregion

        void InitEmotion() {
            emotions = GenerateEmotions();
        }

        IReadOnlyCollection<EmotionData> GenerateEmotions()
        {
            var list = new List<EmotionData>();
            try
            {
                foreach (EmotionState state in Enum.GetValues(typeof(EmotionState)))
                {
                    EmotionData data = new EmotionData();
                    data.State = state;
                    data.StreamImage = CreateImageStream(state);
                    list.Add(data);
                }
            }
            catch (Exception) { }
            return list;
        }

        BaseAnimationInfo GetAnimationInfo() {
            foreach (BaseAnimationInfo animationInfo in XtraAnimator.Current.Animations) {
                if (animationInfo.AnimatedObject.OwnerControl == this) {
                    return animationInfo;
                }
            }
            return null;
        }

        EmotionData FindEmotionData(EmotionState state) {
            return emotions.Where(item => item.State == state).First();
        }

        Stream CreateImageStream(EmotionState state) {
            string name = string.Format("MOCHI_{0}.gif", state.ToString().ToUpper());
            return new FileStream(DataDirectoryHelper.GetRelativePath("Data\\Images\\Emotions\\" + name), FileMode.Open, FileAccess.Read);
        }

        void AnimationFrameCompleted(BaseAnimationInfo animationInfo) {
            if (emotionCount <= 0) {
                int number = randomEmotion.Next(0, 50);
                switch (number) {
                    case 0:
                        LoadEmotionDataCore(EmotionState.Blink);
                        break;

                    case 1:
                        LoadEmotionDataCore(EmotionState.Siri);
                        break;

                    case 2:
                        LoadEmotionDataCore(EmotionState.Reminder);
                        break;

                    default:
                        LoadEmotionDataCore(EmotionState.Calm);
                        break;
                }
                emotionCount = 0;
            } else {
                LoadEmotionDataCore(emotionState);
                emotionCount--;
            }
        }

        void AnimationFrameChanged(BaseAnimationInfo animationInfo) {

        }

        void DestoryImage() {
            if (Image != null) {
                Image.Dispose();
                Image = null;
            }
        }

        void LoadEmotionDataCore(EmotionState state) {
            try {
                DestoryImage();
                var data = FindEmotionData(state);
                if (data == null) return;
                Image = Image.FromStream(data.StreamImage);
            } catch (Exception) {

            }
        }

        public void LoadEmotionState(EmotionState state, int count = 1) {
            if (!Visible) return;
            emotionState = state;
            emotionCount = count;
        }
    }

    public class EmotionData {

        public EmotionState State { get; set; }

        public Stream StreamImage { get; set; }
    }

    public enum EmotionState {
        Abashed,
        Blink,
        BouncyRight,
        BouncyLeft,
        Calm,
        ElatedLeft,
        ElatedRight,
        GollumLeft,
        GollumRight,
        Greeting,
        Greeting2,
        Greeting3,
        HarryPotter,
        Heart,
        Holiday,
        Listening,
        Look,
        Minions,
        NeedMore,
        Oobeintro,
        Optimistic,
        Reminder,
        Satisfied,
        Secrets,
        Sensitive,
        Siri,
        Speaking,
        Starwars,
        Thinking,
        Toystory,
        Wink
    }
}