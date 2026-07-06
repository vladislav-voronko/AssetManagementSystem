namespace Assets.Application.Commands.SwitchAssetStatus
{
    public sealed record SwitchAssetStatusCommand
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
