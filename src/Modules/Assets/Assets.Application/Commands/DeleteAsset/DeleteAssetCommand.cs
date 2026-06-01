namespace Assets.Application.Commands.DeleteAsset
{
    public sealed record DeleteAssetCommand
    {
        public Guid Id { get; set; }
    }
}