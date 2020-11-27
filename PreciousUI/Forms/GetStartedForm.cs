using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PreciousUI.Internals;

namespace PreciousUI.Forms {
    public partial class GetStartedForm : FormBase {
        public GetStartedForm() {
            InitializeComponent();
        }

        string[] GetAboutInfo() {
            return new string[]
            {
            "<color=0,0,0><size=+5>What is Precious?</size></color>\nPrecious is digital life assistant that " +
             "allows you to interact with your computer 24 hours a day, 7 days in a week. Just tell it to find " +
             "content, get answers, play music, and connect with friends and friends. It works seamlessly with " + 
             "many popular desktops applications and has very friendly disposition.",

             "<color=0,0,0><size=+5>Use online speech recognition</size></color>\nTo use this speech recognition option, " + 
             "you will need Internet connection, Google Chrome browser and a good quality microphone on your computer. To make " + 
             "Precious start listening to you, click on the microphone button. This will open a speech recognition window. " + 
             "Follow further instructions on that window.",

             "<color=0,0,0><size=+5>Access Songs and Video Voice Control</size></color>\nWhen it comes to music, Precious " + 
             "is very useful. Assume that you have a huge song collection at many different places on your hard drives. " + 
             "It would be pain to search a song that you want to play browsing through a hierarchy of folders. Precious is a " + 
             "speech recognition enabled application that allows you to play and search songs by voice commands and saves you lot of time.",

             "<color=0,0,0><size=+5>Solve Mathematical Problems</size></color>\nPrecious can apply artificial intelligence for " + 
             "solving a wide range of mathematical problems. It is also a speaking calculator software. " + 
             "Just give voice commands to solve your math problem and Precious will speak the answer back to you. " + 
             "We are making it a brilliant mathematician and in near future it will be also able to solve complex mathematical " + 
             "problems of calculus and algebra in human readable notation.",

             "<color=0,0,0><size=+5>Search Information from the Internet</size></color>\nPrecious can create a report on any " + 
             "subject by retrieving information from the internet. It can even search for you on search engines like Google and " + 
             "other online resources like Wikipedia.",

             "<color=0,0,0><size=+5>Use Dictionary and Thesaurus</size></color>\nPrecious can show you definition of any word. " + 
             "It is a free reference tool for your computer " + 
             "desktop that will improve both your writing skills and your vocabulary. This means it can help you in creative writing, " + 
             "academic papers, marketing, business relations or even being a good orator.",

             "<color=0,0,0><size=+5>Open any Program, Files, Folder or Webpage</size></color>\nYou can open any program, files, folder or " + 
             "a webpage quickly using Precious. You can also teach Precious and create your custom command to open a file, application or a URL.",

             "<color=0,0,0><size=+5>Locate Countries on the Map</size></color>\nIt’s a game which allows you to locate countries on the map. " + 
             "If your answer is correct, you will win points; otherwise, you will lose them. Right answers are the key to success. " + 
             "To finish the game, either find all 177 countries or click the Finish Game button.",

             "<color=0,0,0><size=+5>Use the Artificial Brain</size></color>\nPrecious works like an artificial brain. " + 
             "The aim of Precious (Brain Artificial) project is to develop a software with cognitive abilities similar to " + 
             "those of the human brain so that it can understand human language, think, infer, reason and learn.",

             "<color=0,0,0><size=+15>We love you because we care!!!</size></color>"
            };
        }

        int index = 0;
        private void nextSimpleButton_Click(object sender, EventArgs e) {
            transitionManager.StartTransition(labelControl1);
            string[] about = GetAboutInfo();
            index = index >= about.Length - 1 ? 0 : index < 0 ? 0 : index + 1;
            labelControl1.Text = GetAboutInfo()[index];
            transitionManager.EndTransition();
        }

        public static void OpenGetStartedWindow() {
            string filename = DataDirectoryHelper.GetRelativePath("Data\\License\\Getting Started.pdf");
            ProcessHelper.Start(filename, string.Empty);
        }

        private void readMoreSimpleButton_Click(object sender, EventArgs e) {
            OpenGetStartedWindow();
        }
    }
}