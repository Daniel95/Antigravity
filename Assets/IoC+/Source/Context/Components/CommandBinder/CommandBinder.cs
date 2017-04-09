/// <copyright file="CommandBinder.cs">Copyright (c) 2017 All Rights Reserved</copyright>
/// <author>Joris van Leeuwen</author>

using System;
using System.Collections.Generic;
using UnityEngine;

namespace IoCPlus.Internal {

    public class CommandBinder : ContextComponent, ICommandBinder {

        public IEnumerable<KeyValuePair<AbstractSignal, List<SignalResponse>>> CommandBindings { get { return commandBindings; } }

        protected Dictionary<AbstractSignal, List<SignalResponse>> commandBindings = new Dictionary<AbstractSignal, List<SignalResponse>>();
        protected AbstractSignal currentCommandTarget;

        /// <summary>
        /// Iterates through old commandBindings and replaces each signal with the 
        /// up-to-date signal bound in this context or in parent contexts. If a 
        /// signal is not bound, a new binding is made in this context.
        /// </summary>
        public virtual void UpdateBindings() {
            Dictionary<Type, object> allInjectionBindings = injectionBinder.GetCulumativeInjectionBindings();

            RemoveListeners();

            Dictionary<AbstractSignal, List<SignalResponse>> oldCommandBindings = commandBindings;
            commandBindings = new Dictionary<AbstractSignal, List<SignalResponse>>();

            foreach (KeyValuePair<AbstractSignal, List<SignalResponse>> oldSignalBinding in oldCommandBindings) {
                Type signalType = oldSignalBinding.Key.GetType();

                AbstractSignal signal;
                if (allInjectionBindings.ContainsKey(signalType)) {
                    signal = allInjectionBindings[signalType] as AbstractSignal;
                } else {
                    signal = Activator.CreateInstance(signalType) as AbstractSignal;
                    injectionBinder.Bind(signal.GetType(), signal);
                }

                commandBindings.Add(signal, oldSignalBinding.Value);
            }

            AddListeners();
        }

        public void AddListeners() {
            foreach (KeyValuePair<AbstractSignal, List<SignalResponse>> pair in commandBindings) {
                pair.Key.AddReferencedListener(OnSignal);
            }
        }

        public void RemoveListeners() {
            foreach (KeyValuePair<AbstractSignal, List<SignalResponse>> pair in commandBindings) {
                pair.Key.RemoveReferencedListener(OnSignal);
            }
        }

        public ICommandBinder On<T>() where T : AbstractSignal, new() {
            Dictionary<Type, object> allInjectionBindings = injectionBinder.GetCulumativeInjectionBindings();

            if (!allInjectionBindings.ContainsKey(typeof(T))) {
                currentCommandTarget = injectionBinder.Bind<T>();
            } else {
                currentCommandTarget = allInjectionBindings[typeof(T)] as AbstractSignal;
            }

            if (currentCommandTarget.HasDuplicateParameterTypes()) {
                Debug.LogWarning("Setting a command bindinging with signal '" + currentCommandTarget.GetType() + "'. This will result in undifined behaviour as it has more than one parameter of the same type.");
            }

            if (!commandBindings.ContainsKey(currentCommandTarget)) {
                commandBindings.Add(currentCommandTarget, new List<SignalResponse>());
            }

            int signalResponseIndex = commandBindings[currentCommandTarget].Count;
            commandBindings[currentCommandTarget].Add(new SignalResponse(signalResponseIndex));

            return this;
        }

        public ICommandBinder Do<T>(params object[] parameters) where T : AbstractCommand, new() {
            SignalResponse signalResponse = GetCurrentCommandTargetSignalResponse();
            if (signalResponse != null) {
                T command = new T();
                command.SetParameters(parameters);
                signalResponse.Commands.Add(command);
            }
            return this;
        }

        public ICommandBinder GotoState<T>() where T : Context, new() {
            Do<GotoStateCommand<T>>();
            return this;
        }

        public ICommandBinder SwitchState<T>() where T : Context, new() {
            Do<SwitchStateCommand<T>>();
            return this;
        }

        public ICommandBinder InstantiateView<T>() where T : View {
            Do<InstantiateViewCommand<T>>();
            return this;
        }

        public ICommandBinder Dispatch<T>() where T : Signal {
            Do<DispatchSignalCommand<T>>();
            return this;
        }

        public ICommandBinder SwitchContext<T>() where T : Context, new() {
            Do<SwitchContextCommand<T>>();
            return this;
        }

        public ICommandBinder AddContext<T>() where T : Context, new() {
            Do<AddContextCommand<T>>();
            return this;
        }

        public ICommandBinder Remove() {
            Do<RemoveContextCommand>();
            return this;
        }

        public ICommandBinder OnAbort<T>() where T : Command, new() {
            SignalResponse signalResponse = GetCurrentCommandTargetSignalResponse();
            if (signalResponse != null) {
                signalResponse.AbortCommand = new T();
            }
            return this;
        }

        public ICommandBinder OnFinish<T>() where T : Command, new() {
            SignalResponse signalResponse = GetCurrentCommandTargetSignalResponse();
            if (signalResponse != null) {
                signalResponse.FinishCommand = new T();
            }
            return this;
        }

        public ICommandBinder RevertOnAbort() {
            SignalResponse signalResponse = GetCurrentCommandTargetSignalResponse();
            if (signalResponse != null) {
                signalResponse.RevertOnAbort = true;
            }
            return this;
        }

        public ICommandBinder ExecuteParallel() {
            SignalResponse signalResponse = GetCurrentCommandTargetSignalResponse();
            if (signalResponse != null) {
                signalResponse.ExecuteParallel = true;
            }
            return this;
        }

        protected virtual Dictionary<Type, object> GetCulumativeInjectionBindingsForCommands() {
            return injectionBinder.GetCulumativeInjectionBindings();
        }

        protected virtual Dictionary<string, Dictionary<Type, object>> GetCulumativeLabeledInjectionBindingsForCommands() {
            return injectionBinder.GetCumulativeLabeledInjectionBindings();
        }

        private SignalResponse GetCurrentCommandTargetSignalResponse() {
            if (currentCommandTarget == null || 
                !commandBindings.ContainsKey(currentCommandTarget) ||
                commandBindings[currentCommandTarget].Count == 0) {
                Debug.Log("No signal selected. Call 'On' before adding responses.");
                return null;
            }

            List<SignalResponse> responses = commandBindings[currentCommandTarget];
            return responses[responses.Count - 1];
        }

        private void OnSignal(AbstractSignal signal) {
            if (signal == null) { return; }

            List<SignalResponse> signalResponses;
            commandBindings.TryGetValue(signal, out signalResponses);

            for (int i = 0; i < signalResponses.Count; i++) {
                if (signalResponses[i].Commands.Count == 0) { continue; }
                UpdateSignalResponseInjections(signal, signalResponses[i]);
                signalResponses[i].Respond(context, signal);
            }
        }

        private void UpdateSignalResponseInjections(AbstractSignal signal, SignalResponse signalResponse) {
            Dictionary<Type, object> signalInjectionBindings = signal.InjectionBindings;
            Dictionary<Type, object> allInjectionBindings = GetCulumativeInjectionBindingsForCommands();
            Dictionary<string, Dictionary<Type, object>> allLabeledInjectionBindings = GetCulumativeLabeledInjectionBindingsForCommands();

            for (int i = 0; i < signalResponse.Commands.Count; i++) {
                UpdateCommandInjections(signalResponse.Commands[i], signalInjectionBindings, allInjectionBindings, allLabeledInjectionBindings);
            }

            if (signalResponse.AbortCommand != null) {
                UpdateCommandInjections(signalResponse.AbortCommand, signalInjectionBindings, allInjectionBindings, allLabeledInjectionBindings);
            }

            if (signalResponse.FinishCommand != null) {
                UpdateCommandInjections(signalResponse.FinishCommand, signalInjectionBindings, allInjectionBindings, allLabeledInjectionBindings);
            }
        }

        private static void UpdateCommandInjections(AbstractCommand command,
                                                    Dictionary<Type, object> signalInjectionBindings,
                                                    Dictionary<Type, object> allInjectionBindings,
                                                    Dictionary<string, Dictionary<Type, object>> allLabeledInjectionBindings) {
            Injector.Inject<Inject>(command, allInjectionBindings, Injector.CONTEXT_INJECTION_BINDING_MISSING_MESSAGE);
            Injector.Inject<Inject>(command, allLabeledInjectionBindings, true);
            Injector.Inject<InjectParameter>(command, signalInjectionBindings, Injector.PARAMETER_INJECTION_BINDING_MISSING_MESSAGE);
        }

    }

}