using PasswordManager.app.Common;
using PasswordManager.app.Exceptions;
using PasswordManager.app.interfaces;
using PasswordManager.app.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app
{
    internal class StepManager
    {
        #region Fields

        private IStep _currentStep;
        private IStep _previousStep;
        private IStep initialStep;

        #endregion

        #region Properties

        private static StepManager? _instance;
        public static StepManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StepManager();
                return _instance;
            }
        }

        private IStep CurrentStep
        {
            get { return _currentStep; }
            set
            {
                _previousStep = (_currentStep.CanGoBackTo)? _currentStep : _previousStep;
                _currentStep = value;
                _previousStep.Deactivate();
                _currentStep.Activate();
            }
        }

        #endregion

        #region Ctor

        private StepManager()
        {
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            initialStep = new StartupStep();
            IStep loginStep = new LoginStep();
            IStep registerStep = new RegisterStep();
            IStep viewPasswordsStep = new ViewPasswordsStep();

            initialStep.SelectOptions.Add(loginStep);
            initialStep.SelectOptions.Add(registerStep);

            loginStep.SelectOptions.Add(viewPasswordsStep);

            _currentStep = initialStep;
        }

        public void Start()
        {
            if (_currentStep == null)
                throw new StepManagerNotInitializedException();
            CurrentStep.Activate();
        }

        public void Select(int selectOptionIndex)
        {
            if (selectOptionIndex == CurrentStep.SelectOptions.Count + 1)
            {
                GoBack();
                return;
            }

            CurrentStep = CurrentStep.SelectOptions[--selectOptionIndex];
        }

        private void GoBack()
        {
            if (CurrentStep == initialStep)
            {
                Aggregator.Instance.Raise(AggregatorMethodNames.QUIT_APP);
                return;
            }

            CurrentStep = _previousStep;
        }

        #endregion
    }

    #region Common

    public partial class AggregatorMethodNames
    {
        public const string QUIT_APP = "QuitApp";
    }

    #endregion
}
