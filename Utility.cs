using CoreGraphics;
using UIKit;
using CoreAnimation;
using System;
using EventKit;

namespace iOSMaterialShowcase.Xamarin
{
    public class Utility
    {
    }

    public static class UIViewExtension
    {
        public static void AsCircle(this UIView _view)
        {
            _view.Layer.CornerRadius = _view.Frame.Width * .5f;
            _view.Layer.MasksToBounds = true;
        }

		public static void AsCircleWithTransparentCenter (this UIView _view, UIColor backgroundColor, float targetRadius)
		{
			var largeRadius = _view.Frame.Width * .5f;
			var path = new UIBezierPath ();
			path.AddArc (new CGPoint(_view.Bounds.GetMidX(), _view.Bounds.GetMidY()), (targetRadius + largeRadius) * .5f, 0f, (float)Math.PI * 2, true);
			var shape = new CAShapeLayer ();
			shape.Frame = _view.Bounds;
			shape.Path = path.CGPath;
			shape.LineWidth = largeRadius - targetRadius;
			shape.StrokeColor = backgroundColor.CGColor;
			shape.FillColor = UIColor.Clear.CGColor;
			_view.Layer.AddSublayer(shape);
			_view.Layer.MasksToBounds = true;
		}

        public static void SetTintColor(this UIView _view, UIColor _color, bool _recursive)
        {
            if (_recursive)
            {
                _view.TintColor = _color;
                foreach (var view in _view.Subviews)
                    view.SetTintColor(_color, true);
            }
            else
                _view.TintColor = _color;
        }
    }

    public static class CGRectExtension
    {
        public static CGPoint Center(this CGRect _rect)
        {
            return new CGPoint(_rect.GetMidX(), _rect.GetMidY());
        }
    }

    public static class UILabelExtension
    {
        public static void SizeToFitHeight(this UILabel _label)
        {
            UILabel tempLabel = new UILabel(new CGRect(0, 0, _label.Frame.Width, float.MaxValue))
            {
                Lines = _label.Lines,
                LineBreakMode = _label.LineBreakMode,
                Font = _label.Font,
                Text = _label.Text
            };
            tempLabel.SizeToFit();
            _label.Frame = new CGRect(_label.Frame.GetMinX(), _label.Frame.GetMinY(), _label.Frame.Width, tempLabel.Frame.Height);
        }
    }
}
