using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using PitchOnline.Core;

namespace PitchOnline
{
    // The offset mode - how we offset individual cards in a stack.
    public enum OffsetMode
    {
        // Offset every card.
        EveryCard,
        // Offset every Nth card.
        EveryNthCard,
        // Offset only the top N cards.
        TopNCards,
        //Offset only the bottom N cards.
        BottomNCards,
        // Use the offset values specified in the playing card class (see
        // PlayingCard.FaceDownOffset and PlayingCard.FaceUpOffset).
        UseCardValues
    }

    public class CardStackPanel : StackPanel
    {

        private readonly Size infiniteSpace = new Size(Double.MaxValue, Double.MaxValue);


        protected override Size MeasureOverride(Size constraint)
        {
            //  Get the offsets that each element will need.
            List<Size> offsets = CalculateOffsets();

            //  Calculate the total.
            var total_x = (from o in offsets select o.Width).Sum();
            var total_y = (from o in offsets select o.Height).Sum();

            //  Measure each child (always needed, even if we don't use
            //  the measurement!)
            foreach (UIElement child in Children)
            {
                //  Measure the child against infinite space.
                child.Measure(infiniteSpace);
            }

            //  Add the size of the last element.
            if (LastChild != null)
            {
                //  Add the size.
                total_x += LastChild.DesiredSize.Width;
                total_y += LastChild.DesiredSize.Height;
            }

            return new Size(total_x, total_y);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0, y = 0;
            var n = 0;

            //  Get the offsets that each element will need.
            List<Size> offsets = CalculateOffsets();

            //  If we're going to pass the bounds, deal with it.
            if ((ActualWidth > 0 && finalSize.Width > ActualWidth) ||
                (ActualHeight > 0 && finalSize.Height > ActualHeight))
            {
                //  Work out the amount we have to remove from the offsets.
                double overrun_x = finalSize.Width - ActualWidth;
                double overrun_y = finalSize.Height - ActualHeight;

                //  Now as a per-offset.
                double dx = overrun_x / offsets.Count;
                double dy = overrun_y / offsets.Count;

                //  Now nudge each offset.
                for (int i = 0; i < offsets.Count; i++)
                {
                    offsets[i] = new Size(Math.Max(0, offsets[i].Width - dx), Math.Max(0, offsets[i].Height - dy));
                }

                //  Make sure the final size isn't increased past what we can handle.
                finalSize.Width -= overrun_x;
                finalSize.Height -= overrun_y;
            }

            //  Arrange each child.
            foreach (UIElement child in Children)
            {
                //  Get the card. If we don't have one, skip.
                PlayingCard card = ((FrameworkElement)child).DataContext as PlayingCard;
                if (card == null)
                    continue;

                //  Arrange the child at x,y (the first will be at 0,0)
                child.Arrange(new Rect(x, y, child.DesiredSize.Width, child.DesiredSize.Height));

                //  Update the offset.
                x += offsets[n].Width;
                y += offsets[n].Height;

                //  Increment.
                n++;
            }

            return finalSize;
        }

        private List<Size> CalculateOffsets()
        {
            //  Calculate the offsets on a card by card basis.
            List<Size> offsets = new List<Size>();

            int n = 0;
            int total = Children.Count;

            //  Go through each card.
            foreach (UIElement child in Children)
            {
                //  Get the card. If we don't have one, skip.
                PlayingCard card = ((FrameworkElement)child).DataContext as PlayingCard;
                if (card == null)
                    continue;

                //  The amount we'll offset by.
                double faceDownOffset = 0;
                double faceUpOffset = 0;

                //  We are now going to offset only if the offset mode is appropriate.
                switch (OffsetMode)
                {
                    case OffsetMode.EveryCard:
                        //  Offset every card.
                        faceDownOffset = FacedownOffset;
                        faceUpOffset = FaceupOffset;
                        break;
                    case OffsetMode.EveryNthCard:
                        //  Offset only if n Mod N is zero.
                        if (((n + 1) % NValue) == 0)
                        {
                            faceDownOffset = FacedownOffset;
                            faceUpOffset = FaceupOffset;
                        }
                        break;
                    case OffsetMode.TopNCards:
                        //  Offset only if (Total - N) <= n < Total
                        if ((total - NValue) <= n && n < total)
                        {
                            faceDownOffset = FacedownOffset;
                            faceUpOffset = FaceupOffset;
                        }
                        break;
                    case OffsetMode.BottomNCards:
                        //  Offset only if 0 < n < N
                        if (n < NValue)
                        {
                            faceDownOffset = FacedownOffset;
                            faceUpOffset = FaceupOffset;
                        }
                        break;
                    case OffsetMode.UseCardValues:
                        //  Offset each time by the amount specified in the card object.
                        faceDownOffset = card.FaceDownOffset;
                        faceUpOffset = card.FaceUpOffset;
                        break;
                    default:
                        break;
                }

                n++;

                //  Create the offset as a size.
                Size offset = new Size(0, 0);

                //  Offset.
                switch (Orientation)
                {
                    case Orientation.Horizontal:
                        offset.Width = card.IsFaceDown ? faceDownOffset : faceUpOffset;
                        break;
                    case Orientation.Vertical:
                        offset.Height = card.IsFaceDown ? faceDownOffset : faceUpOffset;
                        break;
                    default:
                        break;
                }

                //  Add to the list.
                offsets.Add(offset);
            }

            return offsets;
        }


        private UIElement LastChild
        {
            get { return Children.Count > 0 ? Children[Children.Count - 1] : null; }
        }


        private static readonly DependencyProperty FacedownOffsetProperty = DependencyProperty.Register("FacedownOffset", typeof(double), typeof(CardStackPanel),
                                                                                new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
        public double FacedownOffset
        {
            get { return (double)GetValue(FacedownOffsetProperty); }
            set { SetValue(FacedownOffsetProperty, value); }
        }

        private static readonly DependencyProperty FaceupOffsetProperty = DependencyProperty.Register("FaceupOffset", typeof(double), typeof(CardStackPanel),
                                                                            new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
        public double FaceupOffset
        {
            get { return (double)GetValue(FaceupOffsetProperty); }
            set { SetValue(FaceupOffsetProperty, value); }
        }


        private static readonly DependencyProperty OffsetModeProperty = DependencyProperty.Register("OffsetMode", typeof(OffsetMode), typeof(CardStackPanel),
                                                                            new PropertyMetadata(OffsetMode.EveryCard));
        public OffsetMode OffsetMode
        {
            get { return (OffsetMode)GetValue(OffsetModeProperty); }
            set { SetValue(OffsetModeProperty, value); }
        }

        private static readonly DependencyProperty NValueProperty = DependencyProperty.Register("NValue", typeof(int), typeof(CardStackPanel),
                                                                        new PropertyMetadata(1));
        public int NValue
        {
            get { return (int)GetValue(NValueProperty); }
            set { SetValue(NValueProperty, value); }
        }
    }
}
