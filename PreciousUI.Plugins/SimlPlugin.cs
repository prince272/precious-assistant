using PreciousUI.Plugins.Adapters;
using PreciousUI.Plugins.Internals;
using Syn.Bot.Siml;
using Syn.Bot.Siml.Events;
using Syn.Bot.Utility;
using Syn.Utility;
using Syn.VA;
using Syn.VA.Interaction;
using Syn.VA.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PreciousUI.Plugins
{
    public class SimlPlugin : Plugin
    {
        private object lockObject = new object();

        public SimlPlugin(string defaultSimlDirectory)
        {
            Bot.Adapters.Add(new AppAdapter());
            Bot.Adapters.Add(new WindowAdapter());
            Bot.Adapters.Add(new ClipboardAdapter());
            Bot.Adapters.Add(new BibleAdapter());
            Bot.Adapters.Add(new DictionaryAdapter());
            Bot.Adapters.Add(new MediaAdapter());
            Bot.Adapters.Add(new WikipediaAdapter());
            Bot.Adapters.Add(new MapAdapter());
            Bot.Configuration.StoreExamples = false;
            Bot.Configuration.StorePatternExamples = false;
            Bot.Configuration.StoreVocabulary = false;
            Bot.MainUser.ResponseReceived += MainUser_ResponseReceived;
            ProjectManager = new ProjectManager();
            if (!string.IsNullOrEmpty(defaultSimlDirectory))
            {
                LoadDefaultSimlProject(defaultSimlDirectory);
            }
        }

        void MainUser_ResponseReceived(object sender, ResponseReceivedEventArgs e)
        {
            BotResponse response = GenerateResponse(new Message(string.Empty, MessageType.EventMessage), e.Result);
            VA.Interaction.Respond(response);
        }

        void LoadDefaultSimlProject(string defaultSimlDirectory)
        {
            try
            {
                LoadFromProject(defaultSimlDirectory);
            }
            catch (Exception ex) { VA.Logger.Error(ex); }
        }

        public ProjectManager ProjectManager { get; private set; }

        public SimlBot Bot { get { return SimlBot.Instance; } }

        public void LoadFromPackage(string simlPackageFile)
        {
            ProjectManager.Reset();
            Bot.Release();
            Bot.PackageManager.LoadFromString(File.ReadAllText(simlPackageFile));
        }

        public void LoadFromProject(string simlDirectory)
        {
            ProjectManager.Reset();
            ProjectManager.LoadFromDirectory(simlDirectory);
            ProjectManager.LoadIntoBot(Bot);
        }

        public void LoadFromFile(string simlFile)
        {
            ProjectManager.Reset();
            ProjectManager.LoadFromFile(simlFile);
            ProjectManager.LoadIntoBot(Bot);
        }

        public void ProcessMessage(Message message)
        {
            Thread thread = new Thread(() =>
            {
                lock (lockObject)
                {
                    VA.Interaction.SendMessage(message);
                    if (message.Type == MessageType.UserMessage)
                    {
                        BotResponse response = GenerateResponse(message, null);
                        VA.Interaction.Respond(response);
                    }
                    else if (message.Type == MessageType.EventMessage)
                    {
                        Bot.Trigger(message.Text);
                    }
                }
            });
            thread.IsBackground = false;
            thread.Start();
        }

        public void ProcessMessage(string text)
        {
            ProcessMessage(new Message(text, MessageType.UserMessage));
        }

        BotResponse GenerateResponse(Syn.VA.Interaction.Message message, ChatResult eventResult)
        {
            int count = 1;
            int[] formation = { 1, 2, 3, 4 };
            BotResponse response = null;
            if (message.Type == Syn.VA.Interaction.MessageType.EventMessage)
            {
                response = new BotResponse();
                response.From = string.Empty;
                response.Hint = string.Empty;
                response.Rank = ResponseRank.Normal;
                response.Text = eventResult.BotMessage;
                object[] parameter = new object[] { response };
                response.RtfText = RtfDocumentHelper.GetRtfText(parameter);
                return response;
            }

            while (response == null || string.IsNullOrEmpty(response.Text))
            {
                int option = (formation.Length >= count) && (count >= 1) ? formation[count - 1] : 100;
                switch (option)
                {
                    case 1: ProcessCommonResponse(message.Text, out response); break;
                    case 2: ProcessQuotationResultResponse(message.Text, out response); break;
                    case 3: ProcessChatResponse(message.Text, out response); break;
                    case 4: ProcessSynSetResultResponse(message.Text, out response); break;
                    default: ProcessDefaultResponse(message.Text, out response); break;
                }
                count++;
            }
            return response;
        }

        void ProcessChatResponse(string messageText, out BotResponse response)
        {
            try
            {
                ChatResult data = SimlBot.Instance.Chat(messageText);
                if (data.Success && !string.IsNullOrEmpty(data.BotMessage))
                {
                    response = new BotResponse();
                    response.From = messageText;
                    response.Text = string.Format("{0}", data.BotMessage);
                    response.Rank = ResponseRank.Normal;
                    object[] parameter = new object[] { response, data.Data.Current };
                    response.RtfText = RtfDocumentHelper.GetRtfText(parameter);
                    return;
                }
            }
            catch (Exception) { }
            response = null;
        }

        void ProcessDefaultResponse(string messageText, out BotResponse response)
        {
            response = new BotResponse();
            response.From = messageText;
            response.Text = string.Format("{0}", BotTextHelper.GetRandomText());
            response.Rank = ResponseRank.Low;
            object[] parameter = new object[] { response };
            response.RtfText = RtfDocumentHelper.GetRtfText(parameter);
        }

        void ProcessSynSetResultResponse(string messageText, out BotResponse response)
        {
            try
            {
                WordNetPlugin wnPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<WordNetPlugin>();
                wnPlugin.QueryName = messageText;
                var synSetResult = wnPlugin.GenerateSynSetResult();
                if (synSetResult != null)
                {
                    response = new BotResponse();
                    response.From = messageText;
                    response.Text = string.Format("{0}", synSetResult.CommonSynSet);
                    response.Rank = ResponseRank.Normal;
                    object[] parameter = new object[] { synSetResult, wnPlugin.GenerateSuggestionResult(), wnPlugin.GenerateThesResult() };
                    response.RtfText = RtfDocumentHelper.GetRtfText(parameter);
                    return;
                }
            }
            catch (Exception) { }
            response = null;
        }

        void ProcessCommonResponse(string message, out BotResponse response)
        {
            response = new BotResponse();
            response.From = message;
            response.Text = string.Format("{0}", BotTextHelper.GetCommonText(message));
            response.Rank = ResponseRank.Normal;
            object[] parameter = new object[] { response };
            response.RtfText = RtfDocumentHelper.GetRtfText(parameter);
        }

        void ProcessQuotationResultResponse(string message, out BotResponse response)
        {
            try
            {
                KJBiblePlugin kjPlugin = VirtualAssistant.Instance.Plugins.GetPluginByType<KJBiblePlugin>();
                kjPlugin.QueryName = message;
                kjPlugin.QueryTestament = BibleNet.KJV.TestamentType.Old | BibleNet.KJV.TestamentType.New;
                kjPlugin.IsQueryLimited = false;
                var quotationResult = kjPlugin.GetQuotationResult();
                if (quotationResult != null)
                {
                    response = new BotResponse();
                    response.From = message;
                    response.Text = string.Format("{0}", quotationResult.CommonQuotation);
                    response.Rank = ResponseRank.Normal;
                    object[] parameter = new object[] { quotationResult };
                    response.RtfText = RtfDocumentHelper.GetRtfText(parameter);
                    return;
                }
            }
            catch (Exception) { }
            response = null;
        }


    }
}