using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MyiOSApp.Core.ViewModels;
using UIKit;

namespace MyiOSApp.Views;

public class FirstView : MvxViewController<FirstViewModel>
{
    private UILabel _titleLabel;
    private UILabel _resultLabel;
    private UIButton _navigateButton;

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        Title = "First Screen";
        View!.BackgroundColor = UIColor.SystemBackground;

        // Create UI elements
        _titleLabel = new UILabel
        {
            Text = "MvvmCross Navigation Sample",
            Font = UIFont.BoldSystemFontOfSize(20),
            TextAlignment = UITextAlignment.Center,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        _resultLabel = new UILabel
        {
            Text = "Tap the button to navigate",
            Font = UIFont.SystemFontOfSize(16),
            TextAlignment = UITextAlignment.Center,
            Lines = 0,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        _navigateButton = new UIButton(UIButtonType.System)
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };
        _navigateButton.SetTitle("Navigate to Detail", UIControlState.Normal);
        _navigateButton.TitleLabel!.Font = UIFont.SystemFontOfSize(18);

        // Wire up button click manually
        _navigateButton.TouchUpInside += async (sender, e) =>
        {
            if (ViewModel != null)
            {
                await ViewModel.NavigateCommand.ExecuteAsync();
            }
        };

        // Add to view
        View.AddSubviews(_titleLabel, _resultLabel, _navigateButton);

        // Setup constraints
        NSLayoutConstraint.ActivateConstraints(new[]
        {
            _titleLabel.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 40),
            _titleLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
            _titleLabel.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),

            _resultLabel.TopAnchor.ConstraintEqualTo(_titleLabel.BottomAnchor, 40),
            _resultLabel.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
            _resultLabel.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),

            _navigateButton.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
            _navigateButton.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor),
            _navigateButton.WidthAnchor.ConstraintEqualTo(200),
            _navigateButton.HeightAnchor.ConstraintEqualTo(44)
        });

        // Update label when ViewModel property changes
        if (ViewModel != null)
        {
            ViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.ResultMessage))
                {
                    _resultLabel.Text = ViewModel.ResultMessage;
                }
            };
        }
    }
}
