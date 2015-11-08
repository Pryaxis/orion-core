namespace Orion.Commands.Commands
{
    public class CommandResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static readonly CommandResult SuccessResult = new CommandResult() {Success = true};
        public static readonly CommandResult FailResult = new CommandResult() { Success = false };
        public static CommandResult Failure(string errorMessage) => new CommandResult() { ErrorMessage = errorMessage, Success = false};
    }
}