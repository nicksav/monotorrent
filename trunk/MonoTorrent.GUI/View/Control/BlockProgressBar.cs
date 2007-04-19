using System;
using System.Collections.Generic;
using System.Text;
using MonoTorrent.Client;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MonoTorrent.GUI.View.Control
{
    class BlockProgressBar : IDrawable
    {
        private BlockEventArgs args;

        public BlockProgressBar(BlockEventArgs piece)
        {
            this.args = piece;
        }

        public void Draw(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds)
        {
            float width = (float)bounds.Width / args.Piece.BlockCount;
            RectangleF brushDimensions = new RectangleF(0, 0, width, bounds.Height);
            using (LinearGradientBrush requestedBrush = new LinearGradientBrush(brushDimensions, Color.LightBlue, Color.Blue, LinearGradientMode.Vertical))
            using (LinearGradientBrush receivedBrush = new LinearGradientBrush(brushDimensions, Color.LightGoldenrodYellow, Color.Goldenrod, LinearGradientMode.Vertical))
            using (LinearGradientBrush writtenBrush = new LinearGradientBrush(brushDimensions, Color.LightGreen, Color.LimeGreen, LinearGradientMode.Vertical))
            using (LinearGradientBrush notRequestedBrush = new LinearGradientBrush(brushDimensions, Color.LightSalmon, Color.Red, LinearGradientMode.Vertical))
            {
                requestedBrush.SetSigmaBellShape(0.25f);
                receivedBrush.SetSigmaBellShape(0.25f);
                writtenBrush.SetSigmaBellShape(0.25f);
                notRequestedBrush.SetSigmaBellShape(0.25f);

                Rectangle rect = bounds;
                
                for (int i = 0; i < this.args.Piece.BlockCount; i++)
                {
                    RectangleF newArea = new RectangleF(rect.X + (width * i), rect.Y, width, rect.Height);
                    if (args.Piece[i].Written)
                        graphics.FillRectangle(writtenBrush, newArea);

                    else if (args.Piece[i].Received)
                        graphics.FillRectangle(receivedBrush, newArea);

                    else if (args.Piece[i].Requested)
                        graphics.FillRectangle(requestedBrush, newArea);

                    else
                        graphics.FillRectangle(notRequestedBrush, newArea);
                }
            }
        }
    }
}