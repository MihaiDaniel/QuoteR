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
				case EnumAnimation.FadeInFromTopLeft:
					await FadeInFromTopLeft(form);
					break;
				case EnumAnimation.FadeInFromTopRight:
					await FadeInFromTopRight(form);
					break;
				case EnumAnimation.FadeInFromBottomLeft:
					await FadeInFromBottomLeft(form);
					break;
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

		private async Task FadeInFromTopLeft(Form form)
		{
			try
			{
				// Final expected location on scrren on the bottom right
				int finalLocationX = 40;
				int finalLocationY = 20;

				// Start from off screen to the left
				int currentPositionX = finalLocationX - form.Width;
				double maxOpacity = form.Opacity;

				form.InvokeIfRequired(() =>
				{
					form.Opacity = 0.0;
					form.Location = new Point(currentPositionX, finalLocationY);
				});

				// Move form frame by frame and increase opacity
				int frame = 20;
				int incrementPositionX = Math.Abs(currentPositionX - finalLocationX) / frame;
				double incrementOpacity = (double)1 / frame;
				while (frame > 0)
				{
					await Task.Delay(5);
					currentPositionX += incrementPositionX;
					if (form.IsDisposed)
					{
						break;
					}
					form.InvokeIfRequired(() =>
					{
						if (maxOpacity >= form.Opacity + incrementOpacity)
						{
							form.Opacity += incrementOpacity;
						}
						form.Location = new Point(currentPositionX, finalLocationY);
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private async Task FadeInFromBottomLeft(Form form)
		{
			try
			{
				// Final expected location on scrren on the bottom right
				int finalLocationX = 40;
				int finalLocationY = Screen.PrimaryScreen.Bounds.Height - form.Height - 50;

				// Start from off screen to the left
				int currentPositionX = finalLocationX - form.Width;
				double maxOpacity = form.Opacity;

				form.InvokeIfRequired(() =>
				{
					form.Opacity = 0.0;
					form.Location = new Point(currentPositionX, finalLocationY);
				});

				// Move form frame by frame and increase opacity
				int frame = 20;
				int incrementPositionX = Math.Abs(currentPositionX - finalLocationX) / frame;
				double incrementOpacity = (double)1 / frame;
				while (frame > 0)
				{
					await Task.Delay(5);
					currentPositionX += incrementPositionX;
					if (form.IsDisposed)
					{
						break;
					}
					form.InvokeIfRequired(() =>
					{
						if (maxOpacity >= form.Opacity + incrementOpacity)
						{
							form.Opacity += incrementOpacity;
						}
						form.Location = new Point(currentPositionX, finalLocationY);
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private async Task FadeInFromTopRight(Form form)
		{
			try
			{
				// Final expected location on scrren on the bottom right
				int finalLocationX = Screen.PrimaryScreen.Bounds.Width - form.Width - 40;
				int finalLocationY = 20;

				// Start from off screen to the right
				int currentPositionX = finalLocationX + form.Width;
				double maxOpacity = form.Opacity;

				form.InvokeIfRequired(() =>
				{
					form.Opacity = 0.0;
					form.Location = new Point(currentPositionX, finalLocationY);
				});

				// Move form frame by frame and increase opacity
				int frame = 20;
				int decrementPositionX = Math.Abs(currentPositionX - finalLocationX) / frame;
				double incrementOpacity = (double)1 / frame;
				while (frame > 0)
				{
					await Task.Delay(5);
					currentPositionX -= decrementPositionX;
					if (form.IsDisposed)
					{
						break;
					}
					form.InvokeIfRequired(() =>
					{
						if (maxOpacity >= form.Opacity + incrementOpacity)
						{
							form.Opacity += incrementOpacity;
						}
						form.Location = new Point(currentPositionX, finalLocationY);
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		private async Task FadeInFromBottomRight(Form form)
		{
			try
			{
				// Final expected location on scrren on the bottom right
				int finalLocationX = Screen.PrimaryScreen.Bounds.Width - form.Width - 40;
				int finalLocationY = Screen.PrimaryScreen.Bounds.Height - form.Height - 50;

				// Start from off screen to the right
				int currentPositionX = finalLocationX + form.Width;
				double maxOpacity = form.Opacity;

				form.InvokeIfRequired(() =>
				{
					form.Opacity = 0.0;
					form.Location = new Point(currentPositionX, finalLocationY);
				});

				// Move form frame by frame and increase opacity
				int frame = 20;
				int decrementPositionX = Math.Abs(currentPositionX - finalLocationX) / frame;
				double incrementOpacity = (double)1 / frame;
				while (frame > 0)
				{
					await Task.Delay(5);
					currentPositionX -= decrementPositionX;
					if (form.IsDisposed)
					{
						break;
					}
					form.InvokeIfRequired(() =>
					{
						if (maxOpacity >= form.Opacity + incrementOpacity)
						{
							form.Opacity += incrementOpacity;
						}
						form.Location = new Point(currentPositionX, finalLocationY);
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
				if (form.IsDisposed)
				{
					break;
				}
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
				if (form.IsDisposed)
				{
					break;
				}
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
			if (form.IsDisposed)
			{
				return;
			}
			form.InvokeIfRequired(() =>
			{
				form.Opacity = maxOpacity;
				form.Update();
			});
		}

	}
}
