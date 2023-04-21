using PasswordManager.app.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app.interfaces
{
    internal abstract class IStep
    {
        #region Fields

        private Logger _logger;

        #endregion

        #region Properties

        private List<IStep> _selectOptions;
        public List<IStep> SelectOptions
        {
            get
            {
                if (_selectOptions == null)
                    _selectOptions = new List<IStep>();
                return _selectOptions;
            }
        }

        protected bool _canGoBackTo = true;
        public bool CanGoBackTo
        {
            get => _canGoBackTo;
        }

        #endregion

        #region Ctor

        public IStep()
        {
            _logger = new Logger();
            _selectOptions = new List<IStep>();
        }

        #endregion

        #region Methods

        public void Activate()
        {
            _logger.LogInfo(GetDisplayOnActivate() + "\n");

            HandleInput();

            if (SelectOptions.Count > 0)
            {
                for (int i = 0; i < SelectOptions.Count; i++)
                {
                    _logger.LogInfo((i + 1) + ": " + SelectOptions[i].GetDisplayOnSelectOption());
                }
            }

            _logger.LogInfo((SelectOptions.Count + 1) + ": " + GetBackStep());

        }

        public void Deactivate()
        {
            Console.Clear();
        }

        #endregion

        #region Overrides

        protected abstract String GetDisplayOnActivate();
        public abstract String GetDisplayOnSelectOption();
        protected virtual void HandleInput() { }
        protected virtual string GetBackStep()
        {
            return "Back";
        }

        #endregion
    }
}
