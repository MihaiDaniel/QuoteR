using Quoter.App.Helpers.Extensions;
using Quoter.Framework.Enums;
using System.Diagnostics;

namespace Quoter.App.Services.FormAnimation
{
	public class FormAnimationsService : IFormAnimationService
	{

		public async Task AnimateAsync(Form form, EnumAnimation enumAnimation)
		{
			switch (enumAnimation)
			{
				case EnumAnimation.FadeInFromBottomRight:
					await FadeInFromBottomRight(form);
					break;
				case EnumAnimation.FadeOut:
					await FadeOut(form);
					break;
				case EnumAnimation.FadeIn:
					await FadeIn(form);
					break;
			}
		}

		private async Task FadeInFromBottomRight(Form form)
		{
			try
			{
				// Final expected location on scrren on the bottom right
				int locationX = Screen.PrimaryScreen.Bounds.Width - form.Width - 40;
				int locationY = Screen.PrimaryScreen.Bounds.Height - form.Height - 50;

				// Start from off screen to the right
				int currentPositionX = locationX + form.Width;
				double maxOpacity = form.Opacity;

				form.InvokeIfRequired(() =>
				{
					form.Opacity = 0.0;
					form.Location = new Point(currentPositionX, locationY);
				});

				// Move form frame by frame and increase opacity
				int frame = 20;
				int decrementPositionX = Math.Abs(currentPositionX - locationX) / frame;
				double incrementOpacity = (double)1 / frame;
				while (frame > 0)
				{
					await Task.Delay(5);
					currentPositionX -= decrementPositionX;
					form.InvokeIfRequired(() =>
					{
						if (maxOpacity >= form.Opacity + incrementOpacity)
						{
							form.Opacity += incrementOpacity;
						}
						form.Location = new Point(currentPositionX, locationY);
						form.Update();
					});
					frame--;
				}
				// Set max opacity just in case
				form.InvokeIfRequired(() =>
				{
					form.Opacity = maxOpacity;
					form.Update();
				});
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
			
		}

		private async Task FadeOut(Form form)
		{
			// Decrease opacity frame by frame
			int frame = 20;
			double decrementOpacity = (double)1 / frame;
			while (frame > 0)
			{
				await Task.Delay(5);
				form.InvokeIfRequired(() =>
				{
					form.Opacity -= decrementOpacity;
					form.Update();
				});
				frame--;
			}
		}

		private async Task FadeIn(Form form)
		{
			// Decrease opacity frame by frame
			int frame = 20;
			double maxOpacity = form.Opacity;

			form.InvokeIfRequired(() =>
			{
				form.Opacity = 0.0;
			});
			double incrementOpacity = (double)1 / frame;
			while (frame > 0)
			{
				await Task.Delay(5);
				form.InvokeIfRequired(() =>
				{
					if(maxOpacity >= form.Opacity + incrementOpacity)
					{
						form.Opacity += incrementOpacity;
					}
					form.Update();
				});
				frame--;
			}
			// Set max opacity just in case something iffy happens
			form.InvokeIfRequired(() =>
			{
				form.Opacity = maxOpacity;
				form.Update();
			});
		}

	}
}
