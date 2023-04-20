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

        private Stack<IStep> _previousSteps;

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
                _previousStep = _currentStep;

                if (_previousStep.CanGoBackTo)
                    _previousSteps.Push(_previousStep);

                _currentStep = value;
                _previousStep.Deactivate();
                _currentStep.Activate();
            }
        }

        #endregion

        #region Ctor

        private StepManager()
        {
            if (_previousSteps == null) _previousSteps = new Stack<IStep>();
            Aggregator.Instance.Subscribe(AggregatorMethodNames.NAVIGATE_TO_OUTCOME, NavigateToOutcome);
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            initialStep = new StartupStep();

            IStep loginStep = new LoginStep();
            IStep registerStep = new RegisterStep();
            IStep viewPasswordsStep = new ViewPasswordsStep();

            IStep createPasswordsStep = new CreatePasswordStep();
            IStep updatePasswordsStep = new UpdatePasswordStep();
            IStep deletePasswordsStep = new DeletePasswordStep();

            initialStep.SelectOptions.Add(loginStep);
            initialStep.SelectOptions.Add(registerStep);

            loginStep.SelectOptions.Add(viewPasswordsStep);

            viewPasswordsStep.SelectOptions.Add(createPasswordsStep);
            viewPasswordsStep.SelectOptions.Add(updatePasswordsStep);
            viewPasswordsStep.SelectOptions.Add(deletePasswordsStep);

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

            CurrentStep = _previousSteps.Pop();
        }

        private void NavigateToOutcome(string message, bool success)
        {
            _currentStep = (success) ? new CompleteStep(message) : new FailStep(message);
        }
        #endregion
    }

    #region Common

    public partial class AggregatorMethodNames
    {
        public const string QUIT_APP = "QuitApp";
        public const string NAVIGATE_TO_OUTCOME = "NavigateToOutcome";
    }

    #endregion
}
