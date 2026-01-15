using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MyiOSApp.Core.ViewModels;
using UIKit;

namespace MyiOSApp.Views;

public class DetailView : MvxViewController<DetailViewModel>
{
    private UILabel _itemIdLabel;
    private UITextField _inputTextField;
    private UIButton _saveButton;
    private UIButton _cancelButton;

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        View!.BackgroundColor = UIColor.SystemBackground;

        // Create UI elements
        _itemIdLabel = new UILabel
        {
            Text = "Item ID: ",
            Font = UIFont.SystemFontOfSize(16),
            TextAlignment = UITextAlignment.Center,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        _inputTextField = new UITextField
        {
            Placeholder = "Enter value",
            BorderStyle = UITextBorderStyle.RoundedRect,
            Font = UIFont.SystemFontOfSize(16),
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        _saveButton = new UIButton(UIButtonType.System)
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        _saveButton.SetTitle("Save", UIControlState.Normal);
        _saveButton.TitleLabel!.Font = UIFont.BoldSystemFontOfSize(18);
        _saveButton.BackgroundColor = UIColor.SystemBlue;
        _saveButton.SetTitleColor(UIColor.White, UIControlState.Normal);
        _saveButton.Layer.CornerRadius = 8;

        _cancelButton = new UIButton(UIButtonType.System)
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        _cancelButton.SetTitle("Cancel", UIControlState.Normal);
        _cancelButton.TitleLabel!.Font = UIFont.SystemFontOfSize(18);
        _cancelButton.BackgroundColor = UIColor.SystemGray3;
        _cancelButton.SetTitleColor(UIColor.Label, UIControlState.Normal);
        _cancelButton.Layer.CornerRadius = 8;

        // Add to view
        View.AddSubviews(_itemIdLabel, _inputTextField, _saveButton, _cancelButton);

        // Setup constraints
        NSLayoutConstraint.ActivateConstraints(new[]
        {
            _itemIdLabel.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 40),
            _itemIdLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
            _itemIdLabel.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),

            _inputTextField.TopAnchor.ConstraintEqualTo(_itemIdLabel.BottomAnchor, 20),
            _inputTextField.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
            _inputTextField.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),
            _inputTextField.HeightAnchor.ConstraintEqualTo(44),

            _saveButton.TopAnchor.ConstraintEqualTo(_inputTextField.BottomAnchor, 40),
            _saveButton.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
            _saveButton.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),
            _saveButton.HeightAnchor.ConstraintEqualTo(50),

            _cancelButton.TopAnchor.ConstraintEqualTo(_saveButton.BottomAnchor, 20),
            _cancelButton.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
            _cancelButton.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),
            _cancelButton.HeightAnchor.ConstraintEqualTo(50)
        });

        // Wire up buttons manually
        _saveButton.TouchUpInside += async (sender, e) =>
        {
            if (ViewModel != null)
            {
                await ViewModel.SaveCommand.ExecuteAsync();
            }
        };

        _cancelButton.TouchUpInside += async (sender, e) =>
        {
            if (ViewModel != null)
            {
                await ViewModel.CancelCommand.ExecuteAsync();
            }
        };

        // Wire up text field
        _inputTextField.EditingChanged += (sender, e) =>
        {
            if (ViewModel != null)
            {
                ViewModel.InputValue = _inputTextField.Text ?? string.Empty;
            }
        };

        // Update UI when ViewModel changes
        if (ViewModel != null)
        {
            Title = ViewModel.Title;
            _itemIdLabel.Text = $"Item ID: {ViewModel.ItemId}";
            _inputTextField.Text = ViewModel.InputValue;

            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.Title))
                {
                    Title = ViewModel.Title;
                }
                else if (e.PropertyName == nameof(ViewModel.ItemId))
                {
                    _itemIdLabel.Text = $"Item ID: {ViewModel.ItemId}";
                }
                else if (e.PropertyName == nameof(ViewModel.InputValue))
                {
                    _inputTextField.Text = ViewModel.InputValue;
                }
            };
        }
    }
}
