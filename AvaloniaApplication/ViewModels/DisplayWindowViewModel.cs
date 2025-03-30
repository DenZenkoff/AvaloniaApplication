using AvaloniaApplication.Export;
using AvaloniaApplication.Models;
using AvaloniaApplication.Queries;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using System.Collections.ObjectModel;

namespace AvaloniaApplication.ViewModels
{
    public partial class DisplayWindowViewModel : ViewModelBase
    {
        private readonly TableModeQueries _modeQueries;
        private readonly TableStepQueries _stepQueries;

        [ObservableProperty]
        private ObservableCollection<ModeModel> modes;
        [ObservableProperty]
        private ModeModel? selectedMode;
        [ObservableProperty]
        private bool isModeEditable = false;

        [ObservableProperty]
        private ObservableCollection<StepModel> steps;
        [ObservableProperty]
        private StepModel? selectedStep;
        [ObservableProperty]
        private bool isStepEditable = false;

        public DisplayWindowViewModel() 
        {
            _modeQueries = new TableModeQueries();
            _stepQueries = new TableStepQueries();

            FillData();
        }

        private void FillData()
        {
            Modes = new ObservableCollection<ModeModel>(_modeQueries.GetModes());
            Steps = new ObservableCollection<StepModel>(_stepQueries.GetSteps());
        }

        partial void OnSelectedModeChanged(ModeModel? value)
        {
            IsModeEditable = false;
        }

        partial void OnSelectedStepChanged(StepModel? value)
        {
            IsStepEditable = false;
        }

        [RelayCommand]
        private void RemoveMode()
        {
            if (SelectedMode != null && SelectedMode.Id != null)
            {
                var deleted = _modeQueries.DeleteMode(SelectedMode.Id ?? 0);
                if (deleted) Modes.Remove(SelectedMode);
            }
        }

        [RelayCommand]
        private void AddMode()
        {
            SelectedMode = new ModeModel();
            SelectedMode = new ModeModel();

            IsModeEditable = true;
        }

        [RelayCommand]
        private void EditMode()
        {
            IsModeEditable = true;
        }

        [RelayCommand]
        private void SaveMode()
        {
            if (SelectedMode != null && SelectedMode.Id == null && IsModeEditable)
            {
                var added = _modeQueries.AddMode(SelectedMode);
                if (added > 0)
                {
                    SelectedMode.Id = added;
                    Modes.Add(SelectedMode);
                }
            }
            else if (SelectedMode != null && SelectedMode.Id != null && IsModeEditable)
            {
                var updated = _modeQueries.UpdateMode(SelectedMode);
                if (updated)
                    Modes = new ObservableCollection<ModeModel>(_modeQueries.GetModes());
            }

            SelectedMode = null;
            IsModeEditable = false;
        }

        [RelayCommand]
        private void ExportModesToExcel()
        {
            ModesExport.ExportModesToExcel(_modeQueries.GetModes());
        }

        [RelayCommand]
        private void RemoveStep()
        {
            if (SelectedStep != null && SelectedStep.Id != null)
            {
                var deleted = _stepQueries.DeleteStep(SelectedStep.Id ?? 0);
                if (deleted) Steps.Remove(SelectedStep);
            }
        }

        [RelayCommand]
        private void AddStep()
        {
            SelectedStep = new StepModel();
            SelectedStep = new StepModel();

            IsStepEditable = true;
        }

        [RelayCommand]
        private void EditStep()
        {
            IsStepEditable = true;
        }

        [RelayCommand]
        private void SaveStep()
        {
            if (SelectedStep != null && SelectedStep.Id == null && IsStepEditable)
            {
                var added = _stepQueries.AddStep(SelectedStep);
                if (added > 0)
                {
                    SelectedStep.Id = added;
                    Steps.Add(SelectedStep);
                }
            }
            else if (SelectedStep != null && SelectedStep.Id != null && IsStepEditable)
            {
                var updated = _stepQueries.UpdateStep(SelectedStep);
                if (updated)
                    Steps = new ObservableCollection<StepModel>(_stepQueries.GetSteps());
            }

            SelectedStep = null;
            IsStepEditable = false;
        }

        [RelayCommand]
        private void ExportStepsToExcel()
        {
            StepsExport.ExportStepsToExcel(_stepQueries.GetSteps());
        }
    }
}
