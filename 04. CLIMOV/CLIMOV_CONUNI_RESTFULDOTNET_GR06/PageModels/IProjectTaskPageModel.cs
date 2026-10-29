using CLIMOV_CONUNI_RESTFULDOTNET_GR06.Models;
using CommunityToolkit.Mvvm.Input;

namespace CLIMOV_CONUNI_RESTFULDOTNET_GR06.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}