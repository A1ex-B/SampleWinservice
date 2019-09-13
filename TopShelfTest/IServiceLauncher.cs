namespace Service
{
    public interface IServiceLauncher
    {
        Topshelf.TopshelfExitCode Launch();
    }
}