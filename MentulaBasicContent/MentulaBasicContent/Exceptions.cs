namespace Mentula.BasicContent
{
    using System;

    /// <summary>
    /// The exception that is thrown when a parameter is missing in a MentulaContent file.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class ParameterNullException : Exception
    {
        /// <summary> The parameter expected. </summary>
        public readonly string Parameter;
        /// <summary> Gets a string representation of the immediate frames on the call stack. </summary>
        public override string StackTrace { get { return null; } }

        /// <summary> 
        /// Initializes a new instance of the <see cref="ParameterNullException"/> class 
        /// with default arguments. 
        /// </summary>
        public ParameterNullException()
        {
            Parameter = "NULL";
        }

        /// <summary> 
        /// Initializes a new instance of the <see cref="ParameterNullException"/> class 
        /// with a specified parameter. 
        /// </summary>
        /// <param name="parameter"> The parameter expected. </param>
        public ParameterNullException(string parameter)
            : base(SetMessage(parameter))
        {
            Parameter = parameter;
        }

        /// <summary> 
        /// Initializes a new instance of the <see cref="ParameterNullException"/> class 
        /// with a specified parameter and a inner exception. 
        /// </summary>
        /// <param name="parameter"> The parameter expected. </param>
        /// <param name="inner"> The inner exception causing this exception. </param>
        public ParameterNullException(string parameter, Exception inner)
            : base(SetMessage(parameter), inner)
        {
            Parameter = parameter;
        }

        private static string SetMessage(string arg)
        {
            return $"Could not find parameter: '{arg}'!";
        }
    }

    /// <summary>
    /// The exception that is thrown when a parameter is invalid in a MentulaContent file.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class ParameterException : Exception
    {
        /// <summary> The type of the specified parameter. </summary>
        public readonly Type Type;
        /// <summary> The parameter expected. </summary>
        public readonly string Parameter;
        /// <summary> The current value of the parameter. </summary>
        public readonly string Value;
        /// <summary> Gets a string representation of the immediate frames on the call stack. </summary>
        public override string StackTrace { get { return null; } }

        /// <summary> 
        /// Initializes a new instance of the <see cref="ParameterException"/> class 
        /// with default arguments. 
        /// </summary>
        public ParameterException()
        {
            Type = typeof(void);
            Parameter = Value = "NULL";
        }

        /// <summary> 
        /// Initializes a new instance of the <see cref="ParameterException"/> class 
        /// with a specified parameter, the value of that parameter and the expected type. 
        /// </summary>
        /// <param name="parameter"> The parameter expected. </param>
        /// <param name="value"> The current value of the parameter. </param>
        /// <param name="type"> The type of parameter expected. </param>
        public ParameterException(string parameter, string value, Type type)
            : base(SetMessage(parameter, value, type))
        {
            Type = type;
            Parameter = parameter;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterException"/> class
        /// with a specified parameter, the value of that parameter, the expected type and a inner exception.
        /// </summary>
        /// <param name="parameter"> The parameter expected. </param>
        /// <param name="value"> The current value of the parameter. </param>
        /// <param name="type"> The type of parameter expected. </param>
        /// <param name="inner"> The inner exception causing this exception. </param>
        public ParameterException(string parameter, string value, Type type, Exception inner)
            : base(SetMessage(parameter, value, type), inner)
        {
            Type = type;
            Parameter = parameter;
            Value = value;
        }

        private static string SetMessage(string parameter, string value, Type type)
        {
            return $"Could not process parameter '{parameter}' to {type}. Value='{value}'!";
        }
    }

    /// <summary>
    /// The exception that is thrown when processing a container fails in a MentulaContent file.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class ContainerException : Exception
    {
        /// <summary> The container that caused the exception </summary>
        public readonly string Container;
        /// <summary> Gets a string representation of the immediate frames on the call stack. </summary>
        public override string StackTrace { get { return null; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerException"/> class 
        /// with default arguments. 
        /// </summary>
        public ContainerException()
        {
            Container = "NULL";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerException"/> class 
        /// with a specified container. 
        /// </summary>
        /// <param name="container"> The specified container. </param>
        public ContainerException(string container)
            : base(SetMessage(container))
        {
            Container = container;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerException"/> class 
        /// with a specified container and a inner exception. 
        /// </summary>
        /// <param name="container"> The specified container. </param>
        /// <param name="inner"> The inner exception causing this exception. </param>
        public ContainerException(string container, Exception inner)
            : base(SetMessage(container), inner)
        {
            Container = container;
        }

        private static string SetMessage(string container)
        {
            return $"An error occured while processing container: {container}!";
        }
    }

    /// <summary>
    /// The exception that is thrown when processing a MentulaContent file fails.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class BuildException : Exception
    {
        /// <summary> Gets a string representation of the immediate frames on the call stack. </summary>
        public override string StackTrace { get { return null; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildException"/> class 
        /// with default arguments. 
        /// </summary>
        public BuildException()
            : base("An exception occured while builing content!")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerException"/> class 
        /// with a specified message.
        /// </summary>
        /// <param name="message"> The message that describes the error. </param>
        public BuildException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerException"/> class 
        /// with a specified message and a inner exception.
        /// </summary>
        /// <param name="message"> The message that describes the error. </param>
        /// <param name="inner"> The inner exception causing this exception. </param>
        public BuildException(string message, Exception inner)
            :base(message, Retro(inner))
        { }

        private static BuildException Retro(Exception e)
        {
            if (e != null) return new BuildException(e.Message, Retro(e.InnerException));
            return null;
        }
    }
}