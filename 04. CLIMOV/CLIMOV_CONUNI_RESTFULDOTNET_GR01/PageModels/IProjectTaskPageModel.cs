using CLIMOV_CONUNI_RESTFULDOTNET_GR01.Models;
using CommunityToolkit.Mvvm.Input;

namespace CLIMOV_CONUNI_RESTFULDOTNET_GR01.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}