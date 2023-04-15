﻿using PasswordManager.app.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.app
{
    internal class Aggregator
    {
        #region Fields

        private Dictionary<String, Delegate> actions;

        #endregion

        #region Properties

        private static Aggregator? _instance;
        public static Aggregator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Aggregator();
                return _instance;
            }
        }

        #endregion

        #region Ctor

        private Aggregator() { actions = new Dictionary<string, Delegate>();  }

        #endregion

        #region Methods

        public void Subscribe(string methodName, Delegate method)
        {
            actions[methodName] = method;
        }

        public void Raise(string methodName, params object[] parameters)
        {
            if (actions.ContainsKey(methodName) == false)
                throw new NoSubscribedMethodException(methodName);

            var action = actions[methodName];
            action.DynamicInvoke(parameters);
        }

        #endregion
    }
}