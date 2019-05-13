using System;
using System.Windows.Input;

namespace OnMenu
{
    /// <summary>
    /// Encapsulates an action
    /// </summary>
    /// <typeparam name="T">Type of the parameter of the command</typeparam>
    public sealed class Command<T> : Command
    {
        /// <summary>
        /// Initializes a command with the given action
        /// </summary>
        /// <param name="execute">The Action to execute</param>
        public Command(Action<T> execute) : base(o => execute((T)o))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a command with the given action and restriction
        /// </summary>
        /// <param name="execute">The action to execute</param>
        /// <param name="canExecute">Flag restricting the execution</param>
        public Command(Action<T> execute, Func<T, bool> canExecute) : base(o => execute((T)o), o => canExecute((T)o))
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }
    }

    /// <summary>
    /// Represents a command
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Map with the given parameters and execute flags
        /// </summary>
        readonly Func<object, bool> canExecute;
        /// <summary>
        /// The action to execute
        /// </summary>
        readonly Action<object> execute;

        /// <summary>
        /// Initializes a command with the given action
        /// </summary>
        /// <param name="execute">the action to execute</param>
        public Command(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            this.execute = execute;
        }

        /// <summary>
        /// Initializes a command with the given action using the sealed class
        /// </summary>
        /// <param name="execute">the action to execute</param>
        public Command(Action execute) : this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a command with the given flags
        /// </summary>
        /// <param name="execute">the action to execute</param>
        /// <param name="canExecute">the flags</param>
        public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            this.canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a command with the given flags using the sealed class
        /// </summary>
        /// <param name="execute">the action to execute</param>
        /// <param name="canExecute">the flags</param>
        public Command(Action execute, Func<bool> canExecute) : this(o => execute(), o => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// Determines if a parameter can be executed
        /// </summary>
        /// <param name="parameter">the parameter</param>
        /// <returns>A boolean indicating if it can be executed</returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);

            return true;
        }

        /// <summary>
        /// Handles the event thrown then CanExecute changes
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Executes the actions encapsulated on this command
        /// </summary>
        /// <param name="parameter">The parameter for the command</param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }

        /// <summary>
        /// Changes the CanExecute flag
        /// </summary>
        public void ChangeCanExecute()
        {
            EventHandler changed = CanExecuteChanged;
            if (changed != null)
                changed(this, EventArgs.Empty);
        }
    }
}
