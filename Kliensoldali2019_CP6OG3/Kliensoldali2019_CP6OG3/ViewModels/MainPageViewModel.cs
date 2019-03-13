using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Kliensoldali2019_CP6OG3.Services;
using Kliensoldali2019_CP6OG3.Models;

namespace Kliensoldali2019_CP6OG3.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private Translator _t;
        public Translator Translator
        {
            get { return _t; }
            set { Set(ref _t, value); }
        }

        private string _toTranslate;
        public string toTranslate
        {
            get { return _toTranslate; }
            set { Set(ref _toTranslate, value); }
        }
        private string[] _sourceLanguages;
        public string[] sourceLanguages
        {
            get { return _sourceLanguages; }
            set { Set(ref _sourceLanguages, value); }
        }
        private string[] _targetLanguages;
        public string[] targetLanguages
        {
            get { return _targetLanguages; }
            set { Set(ref _targetLanguages, value); }
        }
        private string _mainResult;
        public string mainResult
        {
            get { return _mainResult; }
            set { Set(ref _mainResult, value); }
        }
        private string _otherResults;
        public string otherResults
        {
            get { return _otherResults; }
            set { Set(ref _otherResults, value); }
        }
        private string[] _examplesSource;
        public string[] examplesSource
        {
            get { return _examplesSource; }
            set { Set(ref _examplesSource, value); }
        }
        private string[] _examplesDest;
        public string[] examplesDest
        {
            get { return _examplesDest; }
            set { Set(ref _examplesDest, value); }
        }
        private bool _isLoading;
        public bool isLoading
        {
            get { return _isLoading; }
            set { Set(ref _isLoading, value); }
        }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            isLoading = true;
            Translator = new Translator();
            await Translator.init();
            sourceLanguages = Translator.GetSourceLanguages();
            targetLanguages = Translator.GetTargetLanguages(sourceLanguages[0]);
            toTranslate = "";
            Translator.sourceLang = sourceLanguages[0];
            Translator.destLang = targetLanguages[7];
            await base.OnNavigatedToAsync(parameter, mode, state);
            isLoading = false;
        }

        public async Task Translate()
        {
            ResetView();
            Debug.WriteLine("CURRENT LANGUGAES: " + Translator.sourceLang + " -> " + Translator.destLang);
            isLoading = true;
            if(toTranslate == "")
            {
                isLoading = false;
                toTranslate = "Ez a mező nem lehet üres!";
                return;
            }
            try
            {
                JsonResult r = await Translator.translate(_toTranslate);
                List<string> translations = r.GetTranslations();
                otherResults = "";
                foreach (string str in translations)
                {
                    if (str == translations[0])
                        mainResult = str;
                    else
                    {
                        otherResults += str + ", ";
                    }
                }
                if (otherResults.Length > 0)
                {
                    otherResults = otherResults.Remove(otherResults.Length - 2);
                }
                setExamples(r);
            }
            catch (Exception e)
            {
                ManageTranslationException(e);
            }
            finally
            {
                isLoading = false;
            }
            
        }
        private void ManageTranslationException(Exception e)
        {
            mainResult = e.Message;
        }

        private void ResetView()
        {
            mainResult = "";
            otherResults = "";
            examplesSource = Array.Empty<string>();
            examplesDest = Array.Empty<string>();
        }

        private void setExamples(JsonResult r)
        {
            List<string> source = new List<string>();
            List<string> dest = new List<string>();
            if (r.results[0].lexicalEntries[0].entries[0].senses[0].examples != null)
            {

                int i = 0;
                foreach (Example1 ex in r.results[0].lexicalEntries[0].entries[0].senses[0].examples)
                {
                    i++;
                    if (i < 4)
                    {
                        source.Add(ex.text);
                        dest.Add(ex.translations[0].text);
                    }
                }
            }
            examplesSource = source.ToArray();
            examplesDest = dest.ToArray();
        }

        public void UpdateLanguages(string sourceLang)
        {
            targetLanguages = Translator.GetTargetLanguages(sourceLang);

        }
    }
}
